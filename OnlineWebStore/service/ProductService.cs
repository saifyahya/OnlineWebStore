using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineWebStore.config;
using OnlineWebStore.Dto;
using OnlineWebStore.entity;

namespace OnlineWebStore.service
{
    public class ProductService : IProductService
    {
        StoreContext context;
        IMapper mapper;
        public ProductService(StoreContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public string addStoreProduct(ProductDto productDto)
        {
            if (productDto == null || productDto.StoreId == null)
            {
                throw new Exception($"Product|Store is null.");
            }

            Store store = context.stores.Include("Products").FirstOrDefault(s => s.Id == productDto.StoreId);

            if (store == null)
            {
                throw new Exception($"Store with id {productDto.StoreId} Not Exists.");
            }

            if (store.Products!=null && store.Products.Any(item => item.Name == productDto.Name))
            {
                throw new Exception($"Product {productDto.Name} Already Exists.");
            }
            productDto.Code = Guid.NewGuid().ToString();
            context.products.Add(mapper.Map<Product>(productDto));
            context.SaveChanges();
            return productDto.Code;
        }

        public void editStoreProduct(ProductDto productDto, string productName)
        {
            if (productDto == null)
            {
                throw new ArgumentNullException(nameof(productDto), "Product data cannot be null.");
            }

            if (productName == null)
            {
                throw new ArgumentNullException(nameof(productName), "Product name cannot be null.");
            }

            // Retrieve the store and include related products
            Store store = context.stores.Include(s => s.Products).FirstOrDefault(s => s.Id == productDto.StoreId);

            if (store == null)
            {
                throw new Exception($"Store with id {productDto.StoreId} does not exist.");
            }

            // Retrieve the existing product by name
            Product retrievedProduct = store.Products.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

            if (retrievedProduct == null)
            {
                throw new Exception($"Product with name '{productName}' does not exist.");
            }

            // Check for duplicate product by name
            Product duplicateProduct = store.Products.FirstOrDefault(p => p.Name.Equals(productDto.Name, StringComparison.OrdinalIgnoreCase));

            if (duplicateProduct != null)
            {
                throw new Exception($"Product with name '{productDto.Name}' already exists.");
            }

            // Update product fields
            retrievedProduct.Name = productDto.Name;
            retrievedProduct.Description = productDto.Description;
            retrievedProduct.Price = productDto.Price;
            retrievedProduct.StockLevel = productDto.StockLevel;
            retrievedProduct.StoreId = productDto.StoreId;

            // Save changes to the context
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving changes to the database.", ex);
            }
        }


        public void deleteStoreProduct(string productName, int storeId)
        {
            if (productName == null)
            {
                throw new Exception($"Product is null.");
            }
            Store store = context.stores.Include("Products").FirstOrDefault(s => s.Id == storeId);
            if (store == null)
            {
                throw new Exception($"Store with id {storeId} Not Exists.");
            }
            Product retrievedProduct = store.Products.FirstOrDefault(p => p.Name == productName);
            if (retrievedProduct == null)
            {
                throw new Exception($"Product {productName} Not Exists.");
            }
            context.Remove(retrievedProduct);
            context.SaveChanges();
        }

        public List<ProductDto> getStoreProducts(int storeId)
        {
            Store store = context.stores.Include("Products").FirstOrDefault(s => s.Id == storeId);
            if (store == null)
            {
                throw new Exception($"Store with id {storeId} Not Exists.");
            }
            return mapper.Map<List<ProductDto>>(store.Products);
        }

        public ProductDto getStoreProduct(String productName, int storeId)
        {
            Store store = context.stores.Include("Products").FirstOrDefault(s => s.Id == storeId);
            if (store == null)
            {
                throw new Exception($"Store with id {storeId} Not Exists.");
            }
            Product retrievedProduct = store.Products.FirstOrDefault(p => p.Name == productName);
            if (retrievedProduct == null)
            {
                throw new Exception($"Product {productName} Not Exists.");
            }
            return mapper.Map<ProductDto>(retrievedProduct);
        }

        public List<ProductDto> getAllProducts() //Manager Access
        {
            return mapper.Map<List<ProductDto>>(context.products.ToList());
        }

    }
}
