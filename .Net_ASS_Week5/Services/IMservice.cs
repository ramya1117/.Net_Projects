using VisitorSystem.Model;

namespace VisitorSystem.Services
{
    public interface IMService
    {
        Task<Manager> AddManager(Manager managerModel);
        Task<Manager> GetManagerById(string id);
        Task<Manager> UpdateManager(string id, Manager managerModel);
        Task DeleteManager(string id);
    }
}
