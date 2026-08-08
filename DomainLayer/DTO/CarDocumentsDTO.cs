using System;

namespace RentalCar.DomainLayer.DTO
{
    public class CarDocumentsDTO : BaseDTO
    {
        public long Id { get; set; }
        public string FilePath { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool? IsActive { get; set; } = true;

        // Computed (read-only)
        public bool IsExpired => ExpiresAt.HasValue && DateTime.UtcNow > ExpiresAt.Value;

        // Foreign keys
        public long? DocumentTypeId { get; set; }
        public long? CarId { get; set; }
    }
}
