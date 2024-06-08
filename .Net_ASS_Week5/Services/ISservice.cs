using VisitorSystem.Model;

namespace VisitorSystem.Services
{
    public interface ISService
    {
        Task<Security> AddSecurity(Security securityModel);
        Task<Security> GetSecurityById(string id);
        Task<Security> UpdateSecurity(string id, Security securityModel);
        Task DeleteSecurity(string id);

        Task<Security> LoginSecurityUser(string email, string password);
    }
}
