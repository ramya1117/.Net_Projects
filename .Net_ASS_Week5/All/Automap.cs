using AutoMapper;
using VisitorSystem.Model;
using VisitorSystem.Entity;

namespace VisitorSystem.All
{
    public class AutoMap
    {
        public AutoMap()
        {
            CreateMap<EntityVisitor, Visitor>().ReverseMap();
            CreateMap<EntitySecurity, Security>().ReverseMap();
            CreateMap<EntityOffice, Office>().ReverseMap();
            CreateMap<EntityManager, Manager>().ReverseMap();
        }

        
    }
}
