using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class CarServices : ICar
    {
        private readonly IRepository<Car> _repository;
        private readonly RentalCarDbContext _dbContext;

        public CarServices(IRepository<Car> rep, RentalCarDbContext dbContext)
        {
            _repository = rep;
            _dbContext = dbContext;
        }

        // Sets a (possibly shadow) FK property; tolerant of trailing-space FK names in the model.
        private static void SetFk(EntityEntry entry, string name, object value)
        {
            if (value == null) return;
            var prop = entry.Metadata.FindProperty(name) ?? entry.Metadata.FindProperty(name.Trim());
            if (prop == null) return;

            // Convert to the FK's actual CLR type (e.g. Branch's key is int while the DTO carries long).
            var targetType = Nullable.GetUnderlyingType(prop.ClrType) ?? prop.ClrType;
            var converted = Convert.ChangeType(value, targetType);
            entry.Property(prop.Name).CurrentValue = converted;
        }

        private void ApplyForeignKeys(Car model, CarDTO dto)
        {
            var entry = _dbContext.Entry(model);
            SetFk(entry, "BranchId", dto.BranchId);
            SetFk(entry, "FuelTypeId", dto.FuelTypeId);
            SetFk(entry, "LicensePlateId", dto.LicensePlateId);
            SetFk(entry, "CarOwnerId", dto.CarOwnerId);
            SetFk(entry, "BrandId", dto.BrandId);
            SetFk(entry, "CarStatusId", dto.CarStatusId);
        }

        #region DTOtoModel / ModeltoDTO
        public Car FromDTOtoModel(CarDTO dto)
        {
            return new Car
            {
                Id = dto.Id,
                VIN = dto.VIN,
                EngineNo = dto.EngineNo,
                Model = dto.Model,
                ChassisNumber = dto.ChassisNumber,
                Year = dto.Year,
                Color = dto.Color,
                Image = dto.Image,
                PurchasePrice = dto.PurchasePrice,
                CurrentKM = dto.CurrentKM,
                Description = dto.Description,
                InvestorId = dto.InvestorId,
                Is_deleted = dto.Is_deleted,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at
            };
        }

        public CarDTO FromModeltoDTO(Car model)
        {
            return new CarDTO
            {
                Id = model.Id,
                VIN = model.VIN,
                EngineNo = model.EngineNo,
                Model = model.Model,
                ChassisNumber = model.ChassisNumber,
                Year = model.Year,
                Color = model.Color,
                Image = model.Image,
                PurchasePrice = model.PurchasePrice,
                CurrentKM = model.CurrentKM,
                Description = model.Description,
                InvestorId = model.InvestorId,
                BranchId = model.Branch?.Id,
                FuelTypeId = model.FuelType?.Id,
                LicensePlateId = model.LicensePlate?.Id,
                CarOwnerId = model.CarOwner?.Id,
                BrandId = model.Brand?.Id,
                CarStatusId = model.CarStatus?.Id,
                Is_deleted = model.Is_deleted,
                Created_by = model.Created_by,
                Updated_by = model.Updated_by,
                Created_at = model.Created_at,
                Updated_at = model.Updated_at
            };
        }
        #endregion

        #region GetAll
        public async Task<DynamicResponse<List<CarDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<CarDTO>>();
            try
            {
                var list = await _dbContext.Cars
                    .AsNoTracking()
                    .Include(c => c.Branch)
                    .Include(c => c.FuelType)
                    .Include(c => c.LicensePlate)
                    .Include(c => c.CarOwner)
                    .Include(c => c.Brand)
                    .Include(c => c.Investor)
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
        public async Task<DynamicResponse<CarDTO>> GetAsync(long id)
        {
            var response = new DynamicResponse<CarDTO>();
            try
            {
                var model = await _dbContext.Cars
                    .AsNoTracking()
                    .Include(c => c.Branch)
                    .Include(c => c.FuelType)
                    .Include(c => c.LicensePlate)
                    .Include(c => c.CarOwner)
                    .Include(c => c.Brand)
                    .Include(c => c.CarStatus)
                    .Include(c => c.Investor)
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
        public async Task<DynamicResponse<bool>> AddAsync(CarDTO dto)
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

                await _dbContext.Cars.AddAsync(model);
                ApplyForeignKeys(model, dto);
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

        #region Update
        public async Task<DynamicResponse<bool>> UpdateAsync(CarDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                // Global QueryTrackingBehavior is NoTracking, so force tracking here
                // otherwise the mutated entity is never detected as modified on SaveChanges.
                var model = await _dbContext.Cars
                    .AsTracking()
                    .FirstOrDefaultAsync(e => e.Id == dto.Id && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.VIN = dto.VIN;
                model.EngineNo = dto.EngineNo;
                model.Model = dto.Model;
                model.ChassisNumber = dto.ChassisNumber;
                model.Year = dto.Year;
                model.Color = dto.Color;
                model.Image = dto.Image;
                model.PurchasePrice = dto.PurchasePrice;
                model.CurrentKM = dto.CurrentKM;
                model.Description = dto.Description;
                model.InvestorId = dto.InvestorId;
                model.Updated_by = dto.Updated_by;
                model.Updated_at = DateTime.UtcNow;

                ApplyForeignKeys(model, dto);
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
        public async Task<DynamicResponse<bool>> DeleteAsync(long id)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                // Force tracking (global default is NoTracking) so the soft-delete flag persists.
                var model = await _dbContext.Cars
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
