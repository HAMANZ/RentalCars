
using RepositoryLayer.RespositoryPattern;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using ServiceLayer;
using System.Threading.Tasks;
using DomainLayer.DTO_EXT;

namespace RentalCar.ServiceLayer.Implementation
{
    public class ContactServices : IContactus
    {
        private readonly IRepository<Contactus> _repository;
        private RentalCarDbContext _dbContext;
        public ContactServices(IRepository<Contactus> rep, RentalCarDbContext dbContext)
        {

            this._repository = rep; this._dbContext = dbContext;
        }


        #region DTOtoModel/ModeltoDTO 
        public Contactus FromDTOtoModel(ContactusDTO dto)
        {
            Contactus Model = new Contactus();
            Model.Id = dto.Id;
            Model.FirstName = dto.FirstName;
            Model.LastName = dto.LastName;
            Model.Message = dto.Message;
            Model.Email = dto.Email;
            Model.Subject = dto.Subject;
            Model.Is_Seen = dto.Is_Seen;
            Model.Is_deleted = dto.Is_deleted;
            Model.Created_at = dto.Created_at;
            return Model;
        }

        public Contactus FromDTOExtoModel(ContactUsDTOExt dto)
        {
            Contactus Model = new Contactus();
            Model.Id = dto.Id;
            Model.FirstName = dto.FullName;
            Model.LastName = dto.FullName;
            Model.Message = dto.Message;
            Model.Email = dto.Email;
            Model.Subject = dto.Subject;
            Model.Is_Seen = dto.Is_Seen;
            Model.Is_deleted = dto.Is_deleted;
            Model.Created_at = dto.Created_at;
            Model.Updated_at = dto.Updated_at;
            return Model;
        }


        public ContactusDTO FromModeltoDTO(Contactus model)
        {
            ContactusDTO DTO = new ContactusDTO();
            DTO.Id = model.Id;
            DTO.FirstName = model.FirstName;
            DTO.LastName = model.LastName;
            DTO.Message = model.Message;
            DTO.Email = model.Email;
            DTO.Subject = model.Subject;
            DTO.Is_Seen = model.Is_Seen;
            DTO.Is_deleted = model.Is_deleted;
            DTO.Created_at = model.Created_at;
            return DTO;
        }

        #endregion


        #region Get
        public DynamicResponse<ContactusDTO> Get(long Id)
        {
            DynamicResponse<ContactusDTO> response = new DynamicResponse<ContactusDTO>();

            try
            {
                Contactus Model = _dbContext.Contactus.Where(e => e.Id == Id).FirstOrDefault();
                response.Data = FromModeltoDTO(Model);
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion


        #region GetAll
        public DynamicResponse<List<ContactusDTO>> GetAllNotSeen()
        {
            DynamicResponse<List<ContactusDTO>> response = new DynamicResponse<List<ContactusDTO>>();

            try
            {
                List<Contactus> listModel = _dbContext.Contactus.Where(e => e.Is_Seen == false).ToList();
                List<ContactusDTO> listDTO = new List<ContactusDTO>();
                if (listModel.Count != 0)
                {
                    foreach (var item in listModel)
                    {
                        listDTO.Add(FromModeltoDTO(item));
                    }
                }
                response.Data = listDTO;
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion



        #region GetAll
        public DynamicResponse<List<ContactusDTO>> GetAll()
        {
            DynamicResponse<List<ContactusDTO>> response = new DynamicResponse<List<ContactusDTO>>();

            try
            {
                List<Contactus> listModel = _dbContext.Contactus.Where(e => e.Is_deleted == false).ToList();
                List<ContactusDTO> listDTO = new List<ContactusDTO>();
                if (listModel.Count != 0)
                {
                    foreach (var item in listModel)
                    {
                        listDTO.Add(FromModeltoDTO(item));
                    }
                }
                response.Data = listDTO;
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion


        #region Add
        public async Task<DynamicResponse<bool>> Add(ContactusDTO toAdd)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {

                if (toAdd != null)
                {
                    toAdd.Created_at = DateTime.Now;
                    toAdd.Is_deleted = false;
                    await Tools.sendEmail("oonlinetutoring@gmail.com", "from: " +toAdd.Email+": " + toAdd.FirstName + " " + toAdd.LastName+" Subject: "+ toAdd.Subject, toAdd.Message) ;
                    Contactus model = FromDTOtoModel(toAdd);
                    _repository.Insert(model);
                }

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion

        #region Add Ext - added by Hanine
        public async Task<DynamicResponse<bool>> AddMessage(ContactUsDTOExt toAdd)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {

                if (toAdd != null)
                {
                    toAdd.Created_at = DateTime.Now;
                    toAdd.Is_deleted = false;
                    await Tools.sendEmail("oonlinetutoring@gmail.com", "from: " + toAdd.Email + ": " + toAdd.FullName  + " Subject: " + toAdd.Subject, toAdd.Message);
                    Contactus model = FromDTOExtoModel(toAdd);
                    _repository.Insert(model);
                }

                response.Data = true;
                response.HttpStatusCode = HttpStatusCode.OK;

                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion

        #region Update
        public DynamicResponse<bool> Update(ContactusDTO ToUpdate)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                if (ToUpdate.Id != 0)
                {
                    _dbContext.Update(FromDTOtoModel(ToUpdate));
                    _dbContext.SaveChanges();
                    response.Data = true;
                    response.HttpStatusCode = HttpStatusCode.OK;
                    return response;
                }

                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion


        #region Delete
        public DynamicResponse<bool> Delete(long Id)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                Contactus Model = _dbContext.Contactus.Where(e => e.Id == Id).FirstOrDefault();
                if (Model != null)
                {
                    _repository.Remove(Model);
                    response.Data = true;
                    response.HttpStatusCode = HttpStatusCode.OK;
                    return response;
                }

                response.Data = false;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                return response;
            }
            catch (Exception ex)
            {
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                response.Message = "Please try again later";
                response.ServerMessage = ex.Message;

                return response;
            }
        }

        #endregion


    }
}
