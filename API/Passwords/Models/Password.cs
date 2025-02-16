using MonApi.API.Customers.Models;
using MonApi.API.Employees.Models;

namespace MonApi.API.Passwords.Models;

public partial class Password
{
    public int PasswordId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public int AttemptCount { get; set; }

    public DateTime? ResetDate { get; set; }

    public DateTime? DeletionTime { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
