namespace CDCC.Bussiness.ViewModels
{
    public class UserFirebaseModel
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string PhotoURL { get; set; }
        public string Email { get; set; }
        public bool EmailVertified { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAnonymous { get; set; }
        public dynamic[] ProviderData { get; set; }
        public string ApiKey { get; set; }
        public string AppName { get; set; }
        public string AuthDomain { get; set; }
        public string StsTokenManager { get; set; }
        public string RedirectEventId { get; set; }
        public string LastLogin { get; set; }
        public string CreatedTime { get; set; }

    }
}
