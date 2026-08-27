using Microsoft.EntityFrameworkCore;
using RepositoryLayer.RespositoryPattern;
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
    public class RepairServices : IRepair
    {
        private readonly IRepository<Repair> _repository;
        private readonly RentalCarDbContext _dbContext;

        public RepairServices(IRepository<Repair> rep, RentalCarDbContext dbContext)
        {
            _repository = rep;
            _dbContext = dbContext;
        }

        #region DTOtoModel / ModeltoDTO
        public Repair FromDTOtoModel(RepairDTO dto)
        {
            return new Repair
            {
                Id = dto.Id,
                Name = dto.Name,
                Name_ar = dto.Name_ar,
                RepairTypeId = dto.RepairTypeId,
                Details = dto.Details,
                WorkTime = dto.WorkTime,
                LaborCost = dto.LaborCost,
                Is_deleted = dto.Is_deleted,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at
            };
        }

        public RepairDTO FromModeltoDTO(Repair model)
        {
            return new RepairDTO
            {
                Id = model.Id,
                Name = model.Name,
                Name_ar = model.Name_ar,
                RepairTypeId = model.RepairTypeId,
                RepairTypeName = model.RepairType?.Name,
                Details = model.Details,
                WorkTime = model.WorkTime,
                LaborCost = model.LaborCost,
                Is_deleted = model.Is_deleted,
                Created_by = model.Created_by,
                Updated_by = model.Updated_by,
                Created_at = model.Created_at,
                Updated_at = model.Updated_at
            };
        }
        #endregion

        #region GetAll
        public async Task<DynamicResponse<List<RepairDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<RepairDTO>>();
            try
            {
                var list = await _dbContext.Repairs
                    .AsNoTracking()
                    .Include(e => e.RepairType)
                    .Where(e => !e.Is_deleted)
                    .ToListAsync();

                response.Data = list.Select(FromModeltoDTO).ToList();
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
        #endregion

        #region Get
        public async Task<DynamicResponse<RepairDTO>> GetAsync(int id)
        {
            var response = new DynamicResponse<RepairDTO>();
            try
            {
                var model = await _dbContext.Repairs
                    .AsNoTracking()
                    .Include(e => e.RepairType)
                    .FirstOrDefaultAsync(e => e.Id == id && !e.Is_deleted);

                if (model == null)
                {
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                response.Data = FromModeltoDTO(model);
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
        #endregion

        #region Add
        public async Task<DynamicResponse<bool>> AddAsync(RepairDTO dto)
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

                var model = FromDTOtoModel(dto);
                model.Is_deleted = false;
                model.Created_at = DateTime.UtcNow;
                await _repository.InsertAsync(model);

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
        #endregion

        #region Update
        public async Task<DynamicResponse<bool>> UpdateAsync(RepairDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.Repairs
                    .AsTracking()
                    .FirstOrDefaultAsync(e => e.Id == dto.Id && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.Name = dto.Name;
                model.Name_ar = dto.Name_ar;
                model.RepairTypeId = dto.RepairTypeId;
                model.Details = dto.Details;
                model.WorkTime = dto.WorkTime;
                model.LaborCost = dto.LaborCost;
                model.Updated_by = dto.Updated_by;
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
        #endregion

        #region Delete (soft)
        public async Task<DynamicResponse<bool>> DeleteAsync(int id)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.Repairs
                    .AsTracking()
                    .FirstOrDefaultAsync(e => e.Id == id && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.Is_deleted = true;
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
        #endregion
    }
}
