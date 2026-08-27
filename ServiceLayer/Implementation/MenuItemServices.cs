using Microsoft.EntityFrameworkCore;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Implementation
{
    public class MenuItemServices : IMenuItem
    {
        private readonly RentalCarDbContext _dbContext;

        public MenuItemServices(RentalCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MenuItem>> GetMenuTreeAsync()
        {
            var all = await _dbContext.MenuItems
                .AsNoTracking()
                .Where(m => m.IsActive && !m.Is_deleted)
                .OrderBy(m => m.SortOrder)
                .ToListAsync();

            return BuildTree(all);
        }

        public async Task<List<MenuItem>> GetManagementTreeAsync()
        {
            // Include inactive items so an admin can reorder/enable them.
            var all = await _dbContext.MenuItems
                .AsNoTracking()
                .Where(m => !m.Is_deleted)
                .OrderBy(m => m.SortOrder)
                .ToListAsync();

            return BuildTree(all);
        }

        public async Task<DynamicResponse<bool>> UpdateOrderAsync(List<MenuOrderItemDTO> items)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                if (items == null || items.Count == 0)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var ids = items.Select(i => i.Id).ToList();

                // AsTracking because the context's global default is NoTracking.
                var models = await _dbContext.MenuItems
                    .AsTracking()
                    .Where(m => ids.Contains(m.Id) && !m.Is_deleted)
                    .ToListAsync();

                var byId = models.ToDictionary(m => m.Id);

                foreach (var dto in items)
                {
                    if (byId.TryGetValue(dto.Id, out var model))
                    {
                        model.ParentId = dto.ParentId;
                        model.SortOrder = dto.SortOrder;
                        model.Updated_at = DateTime.UtcNow;
                    }
                }

                await _dbContext.SaveChangesAsync();

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }
            return response;
        }

        public async Task<DynamicResponse<MenuItemDTO>> GetByIdAsync(int id)
        {
            var response = new DynamicResponse<MenuItemDTO>();
            try
            {
                var model = await _dbContext.MenuItems
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == id && !m.Is_deleted);

                if (model == null)
                {
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                response.Data = new MenuItemDTO
                {
                    Id = model.Id,
                    Name = model.Name,
                    Name_ar = model.Name_ar,
                    Title = model.Title,
                    Icon = model.Icon,
                    Url = model.Url,
                    IsActive = model.IsActive,
                    ParentId = model.ParentId
                };
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }
            return response;
        }

        public async Task<DynamicResponse<bool>> UpdateDetailsAsync(MenuItemDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                if (dto == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                // AsTracking because the context's global default is NoTracking.
                var model = await _dbContext.MenuItems
                    .AsTracking()
                    .FirstOrDefaultAsync(m => m.Id == dto.Id && !m.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.Name = dto.Name;
                model.Name_ar = dto.Name_ar;
                model.Title = dto.Title;
                model.Icon = dto.Icon;
                model.Url = dto.Url;
                model.IsActive = dto.IsActive;
                // Ignore a parent that points at the item itself (would create a cycle).
                model.ParentId = (dto.ParentId == model.Id) ? model.ParentId : dto.ParentId;
                model.Updated_at = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }
            return response;
        }

        public async Task<DynamicResponse<bool>> CreateAsync(MenuItemDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Title))
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Title is required.";
                    return response;
                }

                // Next sort order within the target group (top level when ParentId is null).
                var maxOrder = await _dbContext.MenuItems
                    .Where(m => !m.Is_deleted && m.ParentId == dto.ParentId)
                    .Select(m => (int?)m.SortOrder)
                    .MaxAsync();

                var model = new MenuItem
                {
                    Name = string.IsNullOrWhiteSpace(dto.Name) ? dto.Title : dto.Name,
                    Name_ar = dto.Name_ar,
                    Title = dto.Title,
                    Icon = dto.Icon,
                    Url = string.IsNullOrWhiteSpace(dto.Url) ? "#" : dto.Url,
                    IsActive = dto.IsActive,
                    ParentId = dto.ParentId,
                    SortOrder = (maxOrder ?? 0) + 1,
                    Is_deleted = false,
                    Created_at = DateTime.UtcNow
                };

                await _dbContext.MenuItems.AddAsync(model);
                await _dbContext.SaveChangesAsync();

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }
            return response;
        }

        // Soft-deletes a menu item together with all of its descendants.
        public async Task<DynamicResponse<bool>> DeleteAsync(int id)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var all = await _dbContext.MenuItems
                    .AsTracking()
                    .Where(m => !m.Is_deleted)
                    .ToListAsync();

                var target = all.FirstOrDefault(m => m.Id == id);
                if (target == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                // Collect the item and every descendant (children, grandchildren, ...).
                var toDelete = new List<MenuItem> { target };
                var queue = new Queue<MenuItem>();
                queue.Enqueue(target);
                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    foreach (var child in all.Where(m => m.ParentId == current.Id))
                    {
                        toDelete.Add(child);
                        queue.Enqueue(child);
                    }
                }

                foreach (var item in toDelete)
                {
                    item.Is_deleted = true;
                    item.Updated_at = DateTime.UtcNow;
                }

                await _dbContext.SaveChangesAsync();

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;
            }
            return response;
        }

        // Assembles a flat, SortOrder-ordered list into a parent/child tree in memory.
        private static List<MenuItem> BuildTree(List<MenuItem> all)
        {
            foreach (var item in all)
                item.Children = new List<MenuItem>();

            var byId = all.ToDictionary(m => m.Id);
            var roots = new List<MenuItem>();

            foreach (var item in all)
            {
                if (item.ParentId.HasValue && byId.TryGetValue(item.ParentId.Value, out var parent))
                    parent.Children.Add(item);
                else if (!item.ParentId.HasValue)
                    roots.Add(item);
            }

            return roots;
        }
    }
}
