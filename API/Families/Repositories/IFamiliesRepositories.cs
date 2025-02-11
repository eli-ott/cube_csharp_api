using MonApi.API.Families.Models;
using MonApi.Shared.Repositories;
using MonApi.API.Families.Repositories;
using MonApi.API.Families.Services;

namespace MonApi.API.Families.Repositories
{
    public interface IFamiliesRepository : IBaseRepository<Family>
    {
    }
}
