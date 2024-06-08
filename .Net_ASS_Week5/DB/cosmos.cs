using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.ComponentModel;
using VisitorSystem.All;
using VisitorSystem.Entity;

namespace VisitorSystem.CosmosDb
{
    public class CosmosDbService
    {
        public class CosmoDBService : ICosmosService
        {
            private readonly CosmosClient _cosmosClient;
            private readonly Microsoft.Azure.Cosmos.Container _container;

            public CosmoDBService()
            {
                _cosmosClient = new CosmosClient(Crediantial.CosmosEndpoint, Crediantial.PrimaryKey);
                _container = _cosmosClient.GetContainer(Crediantial.DatabaseName, Crediantial.ContainerName);
            }

            public async Task<T> Add<T>(T data)
            {
                var response = await _container.CreateItemAsync(data);
                return response.Resource;
            }

            public async Task<IEnumerable<T>> GetAll<T>()
            {
                var query = _container.GetItemQueryIterator<T>();
                var results = new List<T>();

                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    results.AddRange(response);
                }

                return results;
            }

            public async Task<T> Update<T>(T data)
            {
                var response = await _container.UpsertItemAsync(data);
                return response.Resource;
            }

            public async Task DeleteVisitor(string id)
            {
                var visitor = await GetVisitorById(id);
                if (visitor != null)
                {
                    visitor.Active = false;
                    visitor.Archived = true;
                    await Update(visitor);
                }
            }

            public async Task DeleteManager(string id)
            {
                var manager = await GetManagerById(id);
                if (manager != null)
                {
                    manager.Active = false;
                    manager.Archived = true;
                    await Update(manager);
                }
            }

            public async Task DeleteSecurity(string id)
            {
                var security = await GetSecurityById(id);
                if (security != null)
                {
                    security.Active = false;
                    security.Archived = true;
                    await Update(security);
                }
            }

            public async Task DeleteOffice(string id)
            {
                var office = await GetOfficeById(id);
                if (office != null)
                {
                    office.Active = false;
                    office.Archived = true;
                    await Update(office);
                }
            }

            public async Task<EntityOffice> GetOfficeUserByEmail(string email)
            {
                var query = _container.GetItemLinqQueryable<EntityOffice>(true)
                                      .Where(q => q.Email == email && q.Active && !q.Archived)
                                      .ToFeedIterator();

                while (query.HasMoreResults)
                {
                    var resultSet = await query.ReadNextAsync();
                    var officeUser = resultSet.FirstOrDefault();
                    if (officeUser != null)
                    {
                        return officeUser;
                    }
                }

                return null;
            }

            public async Task<EntitySecurity> GetSecurityUserByEmail(string email)
            {
                var query = _container.GetItemLinqQueryable<EntitySecurity>(true)
                                      .Where(q => q.Email == email && q.Active && !q.Archived)
                                      .ToFeedIterator();

                while (query.HasMoreResults)
                {
                    var resultSet = await query.ReadNextAsync();
                    var security = resultSet.FirstOrDefault();
                    if (security != null)
                    {
                        return security;
                    }
                }

                return null;
            }
            public async Task<EntityVisitor> GetVisitorByEmail(string email)
            {
                var query = _container.GetItemLinqQueryable<EntityVisitor>(true)
                                      .Where(q => q.Email == email && q.Active && !q.Archived)
                                      .FirstOrDefault();

                return query;
            }

            public Task<T> GetById<T>(int id)
            {
                throw new NotImplementedException();
            }


            public async Task<EntityVisitor> GetVisitorById(string id)
            {
                try
                {
                    var query = _container.GetItemLinqQueryable<EntityVisitor>(true)
                                          .Where(q => q.Id == id && q.Active && !q.Archived)
                                          .FirstOrDefault();

                    return query;
                }
                catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
            }

            public async Task<EntitySecurity> GetSecurityById(string id)
            {
                try
                {
                    var query = _container.GetItemLinqQueryable<EntitySecurity>(true)
                                          .Where(q => q.Id == id && q.Active && !q.Archived)
                                          .FirstOrDefault();



                    return query;
                }
                catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
            }
            public async Task<EntityManager> GetManagerById(string id)
            {
                try
                {
                    var query = _container.GetItemLinqQueryable<EntityManager>(true)
                                          .Where(q => q.Id == id && q.Active && !q.Archived)
                                          .FirstOrDefault();



                    return query;
                }
                catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
            }

            public async Task<EntityOffice> GetOfficeById(string id)
            {
                try
                {
                    var query = _container.GetItemLinqQueryable<EntityOffice>(true)
                                          .Where(q => q.Id == id && q.Active && !q.Archived)
                                          .FirstOrDefault();



                    return query;
                }
                catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
            }

            public async Task<List<EntityVisitor>> GetVisitorByStatus(bool status)
            {
                var query = _container.GetItemLinqQueryable<EntityVisitor>(true)
                                      .Where(v => v.PassStatus == status && v.Active && !v.Archived)
                                      .AsQueryable();

                var iterator = query.ToFeedIterator();
                var results = new List<EntityVisitor>();

                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    results.AddRange(response);
                }

                return results;
            }

        }
    }
}
