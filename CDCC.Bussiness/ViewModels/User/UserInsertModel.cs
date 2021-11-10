namespace CDCC.Bussiness.ViewModels.User
{
    public class UserInsertModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsSystemAdmin { get; set; }
        public bool? Status { get; set; }
    }
}
