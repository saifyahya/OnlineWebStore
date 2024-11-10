using OnlineWebStore.Dto;

namespace OnlineWebStore.service
{
    public interface IProductService
    {
       public  string addStoreProduct(ProductDto productDto);
        void deleteStoreProduct(string productName,int storeId );
        void editStoreProduct(ProductDto productDto, string productName);
        List<ProductDto> getAllProducts();
        ProductDto getStoreProduct(string productName, int storeId);
        List<ProductDto> getStoreProducts(int storeId);
    }
}