using System.ComponentModel.DataAnnotations;

namespace ShopTARge24.Core.Domain
{
    public class RealEstate
    {
        public Guid? Id { get; set; }
        [Range(typeof(double), "0", "1.7976931348623157E+308", ErrorMessage = "Area cannot be negative.")]
        public double? Area { get; set; }
        public string? Location { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Room number cannot be negative.")]
        public int? RoomNumber { get; set; }
        public string ? BuildingType { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set;}
    }
}
