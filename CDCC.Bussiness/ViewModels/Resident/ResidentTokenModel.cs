

namespace CDCC.Bussiness.ViewModels.Resident
{
    public class ResidentTokenModel
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public int BuildingId { get; set; }
        public int ApartmentId { get; set; }
        public bool? Status { get; set; }
    }
}
