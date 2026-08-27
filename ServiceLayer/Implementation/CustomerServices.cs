using Microsoft.AspNetCore.Identity;
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
    public class CustomerServices : ICustomer
    {
        private readonly IRepository<Customer> _repository;
        private readonly RentalCarDbContext _dbContext;
        private readonly UserManager<EUser> _userManager;

        public CustomerServices(IRepository<Customer> rep, RentalCarDbContext dbContext, UserManager<EUser> userManager)
        {
            _repository = rep;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        // A policy-compliant random password for admin-created party accounts.
        private static string GeneratePassword() => $"Aa1!{Guid.NewGuid():N}";

        // Sets a (possibly shadow) FK property; tolerant of trailing-space FK names in the model.
        private static void SetFk(EntityEntry entry, string name, object value)
        {
            if (value == null) return;
            var prop = entry.Metadata.FindProperty(name) ?? entry.Metadata.FindProperty(name.Trim());
            if (prop != null)
                entry.Property(prop.Name).CurrentValue = value;
        }

        private void ApplyForeignKeys(Customer model, CustomerDTO dto)
        {
            var entry = _dbContext.Entry(model);
            SetFk(entry, "NationalId", dto.NationalityId);
        }

        // Persists the additional documents captured on the customer Add page.
        // Documents.DocumentTypeId / UserId are shadow FKs, so set them via the entry.
        private void AddCustomerDocuments(string customerId, List<CustomerDocumentDTO> documents)
        {
            if (documents == null) return;

            foreach (var d in documents)
            {
                if (string.IsNullOrWhiteSpace(d.FilePath)) continue;

                var doc = new Documents
                {
                    FilePath = d.FilePath,
                    Description = d.Description,
                    ExpiresAt = d.ExpiresAt,
                    IsActive = true,
                    IsCustomerDoc = true,
                    Is_deleted = false,
                    Created_at = DateTime.UtcNow
                };

                var entry = _dbContext.Documents.Add(doc);
                SetFk(entry, "DocumentTypeId", d.DocumentTypeId);
                SetFk(entry, "UserId", customerId);
            }
        }

        #region Documents
        // Loads the additional documents attached to a customer (shadow UserId FK).
        public async Task<DynamicResponse<List<CustomerDocumentDTO>>> GetDocumentsAsync(string customerId)
        {
            var response = new DynamicResponse<List<CustomerDocumentDTO>>();
            try
            {
                var list = await _dbContext.Documents
                    .AsNoTracking()
                    .Include(d => d.DocumentType)
                    .Where(d => d.IsCustomerDoc
                                && !d.Is_deleted
                                && EF.Property<string>(d, "UserId") == customerId)
                    .ToListAsync();

                response.Data = list.Select(d => new CustomerDocumentDTO
                {
                    Id = d.Id,
                    DocumentTypeId = d.DocumentType != null ? d.DocumentType.Id : (long?)null,
                    DocumentTypeName = d.DocumentType != null ? d.DocumentType.Name : null,
                    Description = d.Description,
                    FilePath = d.FilePath,
                    ExpiresAt = d.ExpiresAt
                }).ToList();

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

        public async Task<DynamicResponse<bool>> AddDocumentsAsync(string customerId, List<CustomerDocumentDTO> documents)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                AddCustomerDocuments(customerId, documents);
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

        // Soft-deletes a customer document.
        public async Task<DynamicResponse<bool>> DeleteDocumentAsync(long documentId)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.Documents
                    .AsTracking()
                    .FirstOrDefaultAsync(d => d.Id == documentId && !d.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.Is_deleted = true;
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

        // Party entities inherit EUser (Identity user) — populate the required Identity
        // fields when saving directly (i.e. not through UserManager).
        private static void EnsureIdentityFields(EUser user)
        {
            if (string.IsNullOrWhiteSpace(user.Id))
                user.Id = Guid.NewGuid().ToString();
            if (string.IsNullOrWhiteSpace(user.UserName))
                user.UserName = !string.IsNullOrWhiteSpace(user.Email) ? user.Email : $"user_{Guid.NewGuid():N}";
            user.NormalizedUserName = user.UserName.ToUpperInvariant();
            if (!string.IsNullOrWhiteSpace(user.Email))
                user.NormalizedEmail = user.Email.ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(user.SecurityStamp))
                user.SecurityStamp = Guid.NewGuid().ToString();
        }

        #region DTOtoModel / ModeltoDTO
        public Customer FromDTOtoModel(CustomerDTO dto)
        {
            var model = new Customer
            {
                FullName = dto.FullName,
                FullName_ar = dto.FullName_ar,
                Phone = dto.Phone,
                Email = dto.Email,
                DrivingLicense = dto.DrivingLicense,
                Address = dto.Address,
                LicenseExpiryDate = dto.LicenseExpiryDate,
                DOB = dto.DOB,
                EmergencyContact = dto.EmergencyContact,
                GenderId = dto.GenderId,
                Is_deleted = dto.Is_deleted,
                Created_by = dto.Created_by,
                Updated_by = dto.Updated_by,
                Created_at = dto.Created_at,
                Updated_at = dto.Updated_at
            };

            // Only copy the key when the DTO carries one (updates). For new
            // customers dto.Id is null; overwriting the Guid the IdentityUser
            // constructor generated would make EF fail to track the entity
            // ("primary key property 'Id' is null").
            if (!string.IsNullOrWhiteSpace(dto.Id))
                model.Id = dto.Id;

            return model;
        }

        public CustomerDTO FromModeltoDTO(Customer model)
        {
            return new CustomerDTO
            {
                Id = model.Id,
                FullName = model.FullName,
                FullName_ar = model.FullName_ar,
                Phone = model.Phone,
                Email = model.Email,
                DrivingLicense = model.DrivingLicense,
                Address = model.Address,
                LicenseExpiryDate = model.LicenseExpiryDate,
                DOB = model.DOB,
                EmergencyContact = model.EmergencyContact,
                GenderId = model.GenderId,
                NationalityId = model.Nationality?.Id,
                Is_deleted = model.Is_deleted,
                Created_by = model.Created_by,
                Updated_by = model.Updated_by,
                Created_at = model.Created_at,
                Updated_at = model.Updated_at
            };
        }
        #endregion

        #region GetAll
        public async Task<DynamicResponse<List<CustomerDTO>>> GetAllAsync()
        {
            var response = new DynamicResponse<List<CustomerDTO>>();
            try
            {
                var list = await _dbContext.Customers
                    .AsNoTracking()
                    .Include(c => c.Nationality)
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
        public async Task<DynamicResponse<CustomerDTO>> GetAsync(string id)
        {
            var response = new DynamicResponse<CustomerDTO>();
            try
            {
                var model = await _dbContext.Customers
                    .AsNoTracking()
                    .Include(c => c.Nationality)
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
        public async Task<DynamicResponse<bool>> AddAsync(CustomerDTO dto)
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
                if (string.IsNullOrWhiteSpace(model.UserName))
                    model.UserName = !string.IsNullOrWhiteSpace(model.Email) ? model.Email : $"user_{Guid.NewGuid():N}";

                // Create as an Identity user (hashed password + normalized fields).
                var createResult = await _userManager.CreateAsync(model, GeneratePassword());
                if (!createResult.Succeeded)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    response.Message = string.Join("; ", createResult.Errors.Select(e => e.Description));
                    return response;
                }

                await _userManager.AddToRoleAsync(model, "Customer");

                ApplyForeignKeys(model, dto);
                AddCustomerDocuments(model.Id, dto.Documents);
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
        public async Task<DynamicResponse<bool>> UpdateAsync(CustomerDTO dto)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.Customers
                    .AsTracking()
                    .FirstOrDefaultAsync(e => e.Id == dto.Id && !e.Is_deleted);

                if (model == null)
                {
                    response.Data = false;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    return response;
                }

                model.FullName = dto.FullName;
                model.FullName_ar = dto.FullName_ar;
                model.Phone = dto.Phone;
                model.Email = dto.Email;
                model.DrivingLicense = dto.DrivingLicense;
                model.Address = dto.Address;
                model.LicenseExpiryDate = dto.LicenseExpiryDate;
                model.DOB = dto.DOB;
                model.EmergencyContact = dto.EmergencyContact;
                model.GenderId = dto.GenderId;
                model.Updated_by = dto.Updated_by;
                model.Updated_at = DateTime.UtcNow;
                EnsureIdentityFields(model);

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
        public async Task<DynamicResponse<bool>> DeleteAsync(string id)
        {
            var response = new DynamicResponse<bool>();
            try
            {
                var model = await _dbContext.Customers
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
