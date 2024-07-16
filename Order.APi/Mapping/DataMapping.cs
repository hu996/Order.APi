using AutoMapper;
using Order.APi.Dtos;
using Order.Core.Entities;
using Order.Repository.Repositories.CustomerRepository;

namespace Order.APi.Mapping
{
    public class DataMapping:Profile
    {
        public DataMapping()
        {
            CreateMap<Customer,CustomerDto>().ReverseMap();
            CreateMap<Orders,OrderDto>().ReverseMap();
        }
    }
}
