
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using RentalCar.ServiceLayer.Interface;
using RepositoryLayer.RespositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RentalCar.ServiceLayer.Implementation
{
    public class MessageTemplateServices : IMessageTemplate
    {
        private readonly RentalCarDbContext _dbContext;
        private readonly IRepository<MessageTemplate> _repository;

        public MessageTemplateServices(IRepository<MessageTemplate> rep, RentalCarDbContext dBContext)
        {
            this._dbContext = dBContext;
            this._repository = rep;

        }

        public MessageTemplateServices(IRepository<MessageTemplate> rep)
        {

            _repository = rep;
        }

        public MessageTemplateServices(RentalCarDbContext dBContext)
        {
            this._dbContext = dBContext;

        }
        public MessageTemplateServices()
        {
       
        }
        public MessageTemplate FromDTOtoModel(MessageTemplateDTO dto)
        {
            MessageTemplate Model = new MessageTemplate();
            Model.Id = dto.Id;
            Model.Subject = dto.Subject;;
            Model.Text = dto.Text;
            Model.Is_deleted = dto.Is_deleted;
            Model.Created_at = dto.Created_at;
            return Model;
        }
        
        public MessageTemplateDTO FromModeltoDTO(MessageTemplate Model)
        {
            MessageTemplateDTO dto = new MessageTemplateDTO();
            dto.Id = Model.Id;
            dto.Subject = Model.Subject;
            dto.Name = Model.Name;
            dto.Description = Model.Description;
            dto.Text = Model.Text;
            dto.Is_deleted = Model.Is_deleted;
            dto.Created_at = Model.Created_at;
            return dto;
        }


        public List<MessageTemplateDTO> GetAll()
        {
            try
            {
                List<MessageTemplate> listModel = _dbContext.MessageTemplates.Where(e => e.Is_deleted == false).ToList();
                List<MessageTemplateDTO> listDTO = new List<MessageTemplateDTO>();
                if(listModel!=null && listModel.Count != 0) {
                    foreach (var item in listModel)
                    {
                        listDTO.Add(FromModeltoDTO(item));
                    }
                }
               
                return listDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

     
        public string Add(MessageTemplateDTO msg)
        {
            try
            {
                msg.Created_at = DateTime.Now;
                _dbContext.MessageTemplates.Add(FromDTOtoModel(msg));
                _dbContext.SaveChanges();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string Edit(MessageTemplateDTO msg)
        {
            try
            {
                MessageTemplate Model = _dbContext.MessageTemplates.Where(e => e.Id == msg.Id).FirstOrDefault();
                Model = FromDTOtoModel(msg);
                _dbContext.SaveChanges();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Delete(long Id)
        {
            try
            {
                MessageTemplate msgs = _dbContext.MessageTemplates.Where(e => e.Id == Id).FirstOrDefault();
                if (msgs != null)
                    msgs.Is_deleted = true;
                _dbContext.SaveChanges();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public MessageTemplateDTO Get(long Id)
        {
            try
            {
                MessageTemplateDTO media = new MessageTemplateDTO();
                MessageTemplate med = _dbContext.MessageTemplates.Where(e => e.Id == Id).FirstOrDefault();
                if (med != null)
                    media = FromModeltoDTO(med);
                return media;
            }
            catch (Exception ex)
            {
                throw;
            }
        }




    }
}
