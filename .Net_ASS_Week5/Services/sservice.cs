using AutoMapper;
using VisitorSystem.CosmosDb;
using VisitorSystem.Model;
using VisitorSystem.Entity;
using VisitorSystem.Services;

namespace VisitorSystem.Services
{
    public class SService
    {
        public class SecurityService : ISService
        {
            private readonly ICosmosService _cosmoDBService;
            private readonly IMapper _autoMapper;
            public SecurityService(ICosmosService cosmoDBService, IMapper mapper)
            {
                _cosmoDBService = cosmoDBService;
                _autoMapper = mapper;
            }

            public async Task<Security> AddSecurity(Security securityModel)
            {

                var securityEntity = _autoMapper.Map<EntitySecurity>(securityModel);

                securityEntity.Intialize(true, "security", "Ramya", "Ramya");

                var response = await _cosmoDBService.Add(securityEntity);

                return _autoMapper.Map<Security>(response);
            }

            public async Task<Security> GetSecurityById(string id)
            {
                var security = await _cosmoDBService.GetSecurityById(id);
                return _autoMapper.Map<Security>(security);
            }

            public async Task<Security> UpdateSecurity(string id, Security securityModel)
            {
                var sEntity = await _cosmoDBService.GetSecurityById(id);
                if (sEntity == null)
                {
                    throw new Exception("Security not found");
                }
                sEntity = _autoMapper.Map<EntitySecurity>(securityModel);
                sEntity.Id = id;
                var response = await _cosmoDBService.Update(sEntity);
                return _autoMapper.Map<Security>(response);
            }

            public async Task DeleteSecurity(string id)
            {
                await _cosmoDBService.DeleteSecurity(id);
            }

            public async Task<Security> LoginSecurityUser(string email, string password)
            {
                var securityUser = await _cosmoDBService.GetSecurityUserByEmail(email);

                if (securityUser == null || securityUser.Password != password)
                {
                    return null;
                }

                var securityi = new Security
                {
                    Id = securityUser.Id,
                    Name = securityUser.Name,
                    Email = securityUser.Email,
                };

                return securityi;
            }
        }
    }
}
