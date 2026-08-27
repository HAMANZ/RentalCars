using System;

namespace RentalCar.DomainLayer.DTO
{
    /// <summary>
    /// A single additional document attached to a customer.
    /// FilePath holds the saved file name; the controller resolves the physical path.
    /// Id is populated for existing documents (used for deletion on the Edit page).
    /// </summary>
    public class CustomerDocumentDTO
    {
        public long Id { get; set; }
        public long? DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
