using System;
using System.Collections.Generic;

namespace RentalCar.DomainLayer.DTO
{
    public class CustomerDTO : BaseDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string FullName_ar { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // Stores the uploaded driving-license image file name (photo).
        public string DrivingLicense { get; set; }
        public string Address { get; set; }
        public DateTime LicenseExpiryDate { get; set; }

        // Additional EUser data
        public string DOB { get; set; }
        public string EmergencyContact { get; set; }

        // Foreign keys
        public long? NationalityId { get; set; }
        public long? GenderId { get; set; }

        // Additional customer documents captured on the Add page.
        public List<CustomerDocumentDTO> Documents { get; set; } = new List<CustomerDocumentDTO>();
    }
}
