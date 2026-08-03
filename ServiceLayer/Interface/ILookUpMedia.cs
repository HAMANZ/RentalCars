using  RentalCar.DomainLayer.CommonObjects.Responses;
using RentalCar.DomainLayer.DTO;
using RentalCar.DomainLayer.Models;
using System.Collections.Generic;

namespace RentalCar.ServiceLayer.Interface
{
    public interface ILookUpMedia
    {
        public Media Get(long lookUpId);
        public List<Media> GetList();
        public List<Media> Get(long lookUpId, bool isVideo);
        public List<Media> Get(List<long> lookUpIds);
        public void Add(Media newMedia);
        public void Delete(long lookUpId);
        public void Delete(List<long> lookUpId);
    }
}
