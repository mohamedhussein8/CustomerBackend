using Api.DTO;
using AutoMapper;
using Core.Entities;

namespace Api.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Customer, CustomerDTO>()
          
              .ForMember(d => d.Addresses, o => o.MapFrom(s => s.Addresses.Select(x => x.Address)));

            CreateMap<CustomerDTO, Customer>()

              .ForMember(d => d.Addresses, o => o.MapFrom(s => s.Addresses.Select(e => new Addresses() { Address=e , CustomerId=s.Id})));



        }
    }
}
