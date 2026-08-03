
using RepositoryLayer.RespositoryPattern;
using RentalCar.DomainLayer.CommonObjects;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Reflection;
using ServiceLayer;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using System.Threading.Tasks;

namespace RentalCar.ServiceLayer.Implementation
{
    public class NotificationsServices : INotification
    {
        private readonly IRepository<Notifications> _repository;
        private RentalCarDbContext _dbContext;
        private IMessageTemplate _IMessageTemplate;
        
        public NotificationsServices(IRepository<Notifications> rep, RentalCarDbContext dbContext, IMessageTemplate IMessageTemplate)
        {

            this._repository = rep;
            this._dbContext = dbContext;
            this._IMessageTemplate = IMessageTemplate;
        }


        #region DTOtoModel/ModeltoDTO 
        public Notifications FromDTOtoModel(NotificationsDTO dto)
        {
            Notifications Model = new Notifications();
            Model.Id = dto.Id;
            Model.UserId = dto.UserId;
            Model.NotificationTitle = dto.NotificationTitle;
            Model.NotificationSubject = dto.NotificationSubject;
            Model.NotificationContent = dto.NotificationContent;
            Model.Is_Seen = dto.Is_Seen;
            Model.Is_deleted = dto.Is_deleted;
            Model.Created_at = dto.Created_at; 
            Model.Updated_by = dto.Updated_by;
            Model.Updated_at = dto.Updated_at;
            Model.Created_by = dto.Created_by;
            return Model;
        }



        public NotificationsDTO FromModeltoDTO(Notifications model)
        {
            NotificationsDTO DTO = new NotificationsDTO();
            DTO.Id = model.Id;
            DTO.UserId = model.UserId;
            DTO.NotificationTitle = model.NotificationTitle;
            DTO.NotificationSubject = model.NotificationSubject;
            DTO.NotificationContent = model.NotificationContent;
            DTO.Is_Seen = model.Is_Seen;
            DTO.Is_deleted = model.Is_deleted;

            DTO.Date = model.Created_at.ToString("dd MM yyyy at HH:mm tt");
            DTO.Created_at = model.Created_at;
            DTO.Updated_by = model.Updated_by;
            DTO.Updated_at = model.Updated_at;
            DTO.Created_by = model.Created_by;
            return DTO;
        }

        #endregion




        #region GetAll
        public DynamicResponse<List<NotificationsDTO>> GetAll(string UserId, int offset, int limit)
        {
            DynamicResponse<List<NotificationsDTO>> response = new DynamicResponse<List<NotificationsDTO>>();

            try
            {
                List<Notifications> listModel = _dbContext.Notifications.Where(e => e.UserId == UserId).Skip(offset).Take(limit).ToList();
                List<NotificationsDTO> listDTO = new List<NotificationsDTO>();
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
        public DynamicResponse<List<NotificationsDTO>> GetAllUser( int offset, int limit)
        {
            DynamicResponse<List<NotificationsDTO>> response = new DynamicResponse<List<NotificationsDTO>>();

            try
            {
                List<Notifications> listModel = _dbContext.Notifications.Where(e => e.Is_deleted == false).Skip(offset).Take(limit).ToList();
                List<NotificationsDTO> listDTO = new List<NotificationsDTO>();
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
        public DynamicResponse<List<NotificationsDTO>> GetAll(string UserId)
        {
            DynamicResponse<List<NotificationsDTO>> response = new DynamicResponse<List<NotificationsDTO>>();

            try
            {
                List<Notifications> listModel = _dbContext.Notifications.Where(e=>e.UserId== UserId).ToList();
                List<NotificationsDTO> listDTO = new List<NotificationsDTO>();
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
        public async Task<DynamicResponse<bool>> Add(NotificationsDTO toAdd)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                if (toAdd != null)
                {
                    Notifications model = FromDTOtoModel(toAdd);
                    await this.ReplaceVarByVal( toAdd);
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

        #region replace variable by value

        public async Task<DynamicResponse<NotificationsDTO>> ReplaceVarByVal( NotificationsDTO nots)
        {
            string subvalue = ""; string value = "";

         //   Student std = null;
           
            DynamicResponse<NotificationsDTO> response = new DynamicResponse<NotificationsDTO>();
            try
            {
                var user = _dbContext.Users.Where(e => e.Email == nots.Email).FirstOrDefault();
                if (user != null)
                {

                   // std = _dbContext.Student.Where(e => e.Id == user.Id).FirstOrDefault();
                }
                MessageTemplateDTO msg = _IMessageTemplate.Get(nots.MessageId);
                NotificationsDTO not = new NotificationsDTO();
                string source = msg.Text;
                string subsource = msg.Subject;
                List<string> subattrname = subsource.EverythingBetween("[[[", "]]]");
                List<string> attrname = source.EverythingBetween("[[[", "]]]");
                List<string> attrvalue = new List<string>();
                foreach (string item in attrname)
                {

                    if (item.Equals("Date"))
                    {
                        value = not.Date.ToString();
                    }
                    if (item.Equals("UserId"))
                    {
                        value = user.Id.ToString();
                    }
                    if (item.Equals("UserName"))
                    {
                        value = "" + user.UserName;
                    }
                    //else if (item.Equals("StudentFullName_en"))
                    //{
                    //    value = std.FirstName_en+" "+std.LastName_en;
                    //}
                    //else if (item.Equals("StudentFullName_ar"))
                    //{
                    //    value = std.FirstName_ar + " " + std.LastName_ar;
                    //}

                    //else { value = Helper.GetPropValue<string>(submission.Data, item); }
                    source = source.Replace("[[[" + item + "]]]", value);

                }
                foreach (string item in subattrname)
                {
                    if (item.Equals("Date"))
                    {
                        value = not.Date.ToString();
                    }
                    if (item.Equals("UserId"))
                    {
                        value = user.Id.ToString();
                    }
                    if (item.Equals("UserName"))
                    {
                        value = "" + user.UserName;
                    }
                  
                    //else if (item.Equals("StudentFullName_en"))
                    //{
                    //    value = std.FirstName_en + " " + std.LastName_en;
                    //}
                    //else if (item.Equals("StudentFullName_ar"))
                    //{
                    //    value = std.FirstName_ar + " " + std.LastName_ar;
                    //}
                    //else { value = Helper.GetPropValue<string>(submission.Data, item); }
                    subsource = subsource.Replace("[[[" + item + "]]]", subvalue);

                }
                not.NotificationSubject = subsource;
                not.NotificationContent = source;

                not.TokenId = await Tools.sendNotification(subsource, source, nots.TokenId);
                await Tools.sendEmail(nots.Email,subsource, source);


                //StringBuilder s = StringBuilder(source);
                response.Data = not;
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
        #region Seen
        public DynamicResponse<bool> Seen(long Id)
        {
            DynamicResponse<bool> response = new DynamicResponse<bool>();

            try
            {
                if (Id != 0)
                {
                    Notifications model = _dbContext.Notifications.Where(e => e.Id == Id).FirstOrDefault();
                    model.Is_Seen = true;
                    _repository.Update(model);

                    response.Data = true;
                    response.HttpStatusCode = HttpStatusCode.OK;

                    return response;

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




    }
}
