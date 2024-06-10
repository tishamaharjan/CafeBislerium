namespace CafeBislerium.Data
{
    //using getter and setter method for storing different user details.
    public class User
    {
        public Guid UserID { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}
