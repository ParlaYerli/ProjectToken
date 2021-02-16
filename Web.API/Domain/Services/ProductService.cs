using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Domain.Model;
using Web.API.Domain.Repositories;
using Web.API.Domain.Responses;
using Web.API.Domain.UnitOfWork;

namespace Web.API.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductResponse> AddProduct(Product product)
        {
            try
            {
                await _repository.AddProductAsync(product);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün eklenirken bir hata oluştu{ ex.Message }");
            }
        }

        public async Task<ProductResponse> FindByIdAsync(int productId)
        {
            try
            {
                Product product = await _repository.FindByIdAsync(productId);
                if (product==null)
                {
                    return new ProductResponse("ürün bulunamadı.");
                }
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün bulunurken bir hata oluştu:{ex.Message}");
            }
        }

        public async Task<ProductListResponse> ListAsync()
        {
            try
            {
                IEnumerable<Product> productLists = await _repository.ListAsync();
                return new ProductListResponse(productLists);
            }
            catch (Exception ex)
            {
                return new ProductListResponse($"Ürün listelenirken bir hata oluştu:{ex.Message}");
            }
        }

        public async Task<ProductResponse> RemoveProduct(int productId)
        {
            try
            {
                Product product = await _repository.FindByIdAsync(productId);
                if (product==null)
                {
                    return new ProductResponse("ürün bulunumadı.");
                }
                else
                {
                    await _repository.RemoveProductAsync(productId);
                    await _unitOfWork.CompleteAsync();
                    return new ProductResponse(product);
                }
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün silinirken bir hata oluştu:{ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateResponse(Product product, int productId)
        {
            try
            {
                var firstProduct = await _repository.FindByIdAsync(productId);
                if (firstProduct==null)
                {
                    return new ProductResponse("Ürün bulunamadı");
                }
                firstProduct.Name = product.Name;
                firstProduct.Category = product.Category;
                firstProduct.Price = product.Price;
                _repository.UpdateProduct(firstProduct);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(firstProduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"Ürün silinirken bir hata oluştu:{ex.Message}");
            }
        }
    }
}
