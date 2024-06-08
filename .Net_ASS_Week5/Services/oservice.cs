using AutoMapper;
using VisitorSystem.CosmosDb;
using VisitorSystem.Model;
using VisitorSystem.Entity;
using VisitorSystem.Services;

namespace VisitorSystem.Services
{
    public class OfficeService : IOService
    {
        private readonly ICosmosService _cosmoDBService;
        private readonly IMapper _autoMapper;
        public OfficeService(ICosmosService cosmoDBService, IMapper mapper)
        {
            _cosmoDBService = cosmoDBService;
            _autoMapper = mapper;
        }

        public async Task<Office> AddOffice(Office officeModel)
        {

            var officeEntity = _autoMapper.Map<EntityOffice>(officeModel);

            officeEntity.Intialize(true, "office", "Atul", "Atul");

            var response = await _cosmoDBService.Add(officeEntity);

            return _autoMapper.Map<Office>(response);
        }

        public async Task<Office> GetOfficeById(string id)
        {
            var office = await _cosmoDBService.GetOfficeById(id);
            return _autoMapper.Map<Office>(office);
        }

        public async Task<Office> UpdateOffice(string id, Office officeModel)
        {
            var officeEntity = await _cosmoDBService.GetOfficeById(id);
            if (officeEntity == null)
            {
                throw new Exception("Office not found");
            }
            officeEntity = _autoMapper.Map<EntityOffice>(officeModel);
            officeEntity.Id = id;
            var response = await _cosmoDBService.Update(officeEntity);
            return _autoMapper.Map<Office>(response);
        }

        public async Task DeleteOffice(string id)
        {
            await _cosmoDBService.DeleteVisitor(id);
        }


        public async Task<Office> LoginOfficeUser(string email, string password)
        {
            var officeUser = await _cosmoDBService.GetOfficeUserByEmail(email);

            if (officeUser == null || officeUser.Password != password)
            {
                return null;
            }

            var officedto = new Office
            {
                Id = officeUser.Id,
                Name = officeUser.Name,
                Email = officeUser.Email,
                Phone = officeUser.Phone,
                Role = officeUser.Role,

            };

            return officedto;
        }
    }
}
