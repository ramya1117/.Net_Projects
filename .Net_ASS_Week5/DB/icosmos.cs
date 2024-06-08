using VisitorSystem.Entity;
using VisitorSystem.All;
using VisitorSystem.Model;
using VisitorSystem.Services;

namespace VisitorSystem.CosmosDb
{
    public class ICosmosService
    {
        Task<EntityVisitor> GetVisitorByEmail(string email);
        Task<List<EntityVisitor>> GetVisitorByStatus(bool status);
        Task<EntityVisitor> GetVisitorById(string id);
        Task<EntityVisitor> GetSecurityById(string id);
        Task<EntityManager> GetManagerById(string id);
        Task<EntityOffice> GetOfficeById(string id);
        Task<T> GetById<T>(int id);
        Task<IEnumerable<T>> GetAll<T>();
        Task<T> Add<T>(T entity);
        Task<T> Update<T>(T entity);

        Task<EntityOffice> GetOfficeUserByEmail(string email);
        Task<EntitySecurity> GetSecurityUserByEmail(string email);
        Task DeleteVisitor(string id);
        Task DeleteManager(string id);
        Task DeleteSecurity(string id);
        Task DeleteOffice(string id);

    }
}
