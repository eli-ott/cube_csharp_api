using MonApi.API.Passwords.Models;

namespace MonApi.API.Customers.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
        public string PasswordId { get; set; }
        public Password Password { get; set; }
    }
}
