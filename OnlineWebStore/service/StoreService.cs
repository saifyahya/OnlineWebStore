using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineWebStore.config;
using OnlineWebStore.Dto;
using OnlineWebStore.entity;

namespace OnlineWebStore.service
{
    public class StoreService : IStoreService
    {
        StoreContext context;
        IMapper mapper;
        public StoreService(StoreContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public void addStore(StoreDto storeDto)
        {
            context.stores.Add(mapper.Map<Store>(storeDto));
            context.SaveChanges();
        }

        public List<StoreDto> getStores()
        {
            return mapper.Map<List<StoreDto>>(context.stores.Include("Orders").Include("Products"));
        }

        public StoreDto getStore(int id)
        {
            return mapper.Map<StoreDto>(context.stores.Include("Orders").Include("Products"));
        }


    }
}
