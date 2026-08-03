using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Net;

namespace RentalCar.DomainLayer.CommonObjects
{
    public class DynamicResponse<T>
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
        public string ServerMessage { get; set; }

    }
}
