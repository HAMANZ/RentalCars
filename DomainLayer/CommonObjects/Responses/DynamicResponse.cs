using System.Collections.Generic;
using System.Net;
using RentalCar.DomainLayer.CommonObjects;
using Microsoft.AspNetCore.Identity;

namespace RentalCar.DomainLayer.CommonObjects.Responses
{
    public class DynamicResponse<T>
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
        public string ServerMessage { get; set; }
        public Pagination Pagination { get; set; }

    }
}
