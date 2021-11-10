namespace CDCC.Bussiness.ViewModels.Resident
{
    public class ResidentInsertModel
    {

        public bool IsAdmin { get; set; }
        public bool? Status { get; set; }
        public int UserId { get; set; }
        public int BuildingId { get; set; }
        public int ApartmentId { get; set; }
    }
}
