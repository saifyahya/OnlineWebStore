using OnlineWebStore.Dto;

namespace OnlineWebStore.service
{
    public interface IStoreService
    {
        void addStore(StoreDto storeDto);
        public StoreDto getStore(int id);
        public List<StoreDto> getStores();
    }
}