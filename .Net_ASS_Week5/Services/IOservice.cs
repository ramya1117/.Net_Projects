using VisitorSystem.Model;

namespace VisitorSystem.Services
{
    public interface IOService
    {
        Task<Office> AddOffice(Office officeModel);
        Task<Office> GetOfficeById(string id);
        Task<Office> UpdateOffice(string id, Office officeModel);
        Task DeleteOffice(string id);

        Task<Office> LoginOfficeUser(string email, string password);
    }
}
