using AutoMapper;
using VisitorSystem.CosmosDb;
using VisitorSystem.Model;
using VisitorSystem.Entity;
namespace VisitorSystem.Services
{
    public class MService
    {
        public class ManagerService : IMService
        {
            private readonly ICosmosService _cosmosDbService;
            private readonly IMapper _autoMapper;

            public ManagerService(ICosmosService cosmoDBService, IMapper mapper)
            {
                _cosmosDbService = cosmoDBService;
                _autoMapper = mapper;
            }

            public async Task<Manager> AddManager(Manager managerModel)
            {

                var mEntity = _autoMapper.Map<EntityManager>(managerModel);

                mEntity.Intialize(true, "manager", "Ramya", "Ramya");

                var response = await _cosmosDbService.Add(mEntity);

                return _autoMapper.Map<Manager>(response);
            }

            public async Task<Manager> GetManagerById(string id)
            {
                var security = await _cosmosDbService.GetManagerById(id);
                return _autoMapper.Map<Manager>(security);
            }

            public async Task<Manager> UpdateManager(string id, Manager managerModel)
            {
                var managerEntity = await _cosmosDbService.GetManagerById(id);
                if (managerEntity == null)
                {
                    throw new Exception("Manager not found");
                }
                managerEntity = _autoMapper.Map<EntityManager>(managerModel);
                managerEntity.Id = id;
                var response = await _cosmosDbService.Update(managerEntity);
                return _autoMapper.Map<Manager>(response);
            }

            public async Task DeleteManager(string id)
            {
                await _cosmosDbService.DeleteManager(id);
            }
        }
    }
}
