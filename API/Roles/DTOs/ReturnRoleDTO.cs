namespace MonApi.API.Roles.DTOs
{
    public class ReturnRoleDTO
    {
        public int RoleId { get; set; }

        public string Name { get; set; } = null!;

        public DateTime? DeletionTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
