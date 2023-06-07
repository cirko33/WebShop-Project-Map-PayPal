using AutoMapper;
using OnlineStoreApp.DTOs;
using OnlineStoreApp.Exceptions;
using OnlineStoreApp.Interfaces;
using OnlineStoreApp.Interfaces.IServices;
using OnlineStoreApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineStoreApp.Services
{
    public class SellerService : ISellerService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;

        public SellerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task AddProduct(CreateProductDTO product, int userId)
        {
            var prod = _mapper.Map<Product>(product);
            if((await _unitOfWork.Users.Get(x => x.Id == userId)) == null)
                throw new BadRequestException("Error with id in token. Logout and login again");

            prod.SellerId = userId;
            if(product.ImageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    product.ImageFile.CopyTo(ms);
                    prod.Image = ms.ToArray();
                }
            }
            await _unitOfWork.Products.Insert(prod);
            await _unitOfWork.Save();
        }

        public async Task DeleteProduct(int id, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId, new List<string> { "Products" }) ?? throw new UnauthorizedException("Error with id in token. Logout and login again");

            var product = user.Products!.Find(x => x.Id == id) ?? throw new UnauthorizedException("This product isn't yours");
            
            _unitOfWork.Products.Delete(product);
            await _unitOfWork.Save();
        }

        public async Task<List<OrderDTO>> GetNewOrders(int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId, new List<string> { "Products" }) ?? throw new UnauthorizedException("Error with id in token. Logout and login again");

            var orders = await _unitOfWork.Orders.GetAll(x => !x.IsCancelled && x.DeliveryTime > DateTime.Now, null, new List<string> { "Items" });
            if(orders != null)
                orders = orders.ToList().FindAll(x => x.Items!.Any(x => user.Products!.Select(x => x.Id).Contains(x.ProductId)) && !x.IsCancelled);
                
            return _mapper.Map<List<OrderDTO>>(orders!.OrderByDescending(x => x.OrderTime));
        }

        public async Task<List<OrderDTO>> GetOrders(int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId, new List<string> { "Products" }) ?? throw new UnauthorizedException("Error with id in token. Logout and login again");

            var orders = await _unitOfWork.Orders.GetAll(null, null, new List<string> { "Items" });
            if (orders != null)
                orders = orders.ToList().FindAll(x => x.Items!.Any(x => user.Products!.Select(x => x.Id).Contains(x.ProductId)) && !x.IsCancelled);

            return _mapper.Map<List<OrderDTO>>(orders!.OrderByDescending(x => x.OrderTime));
        }

        public async Task<ProductDTO> GetProduct(int id, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId, new List<string> { "Products" }) ?? throw new UnauthorizedException("Error with id in token. Logout and login again");

            var product = user.Products!.Find(x => x.Id == id);
            if (product == null)
                throw new BadRequestException("This product doesn't belong to you");

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<List<ProductDTO>> GetProducts(int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId, new List<string> { "Products" }) ?? throw new UnauthorizedException("Error with id in token. Logout and login again");
            return _mapper.Map<List<ProductDTO>>(user.Products!);
        }

        public async Task UpdateProduct(int id, ProductDTO product, int userId)
        {
            var user = await _unitOfWork.Users.Get(x => x.Id == userId, new List<string> { "Products" }) ?? throw new UnauthorizedException("Error with id in token. Logout and login again");
            var prod = user.Products!.Find(x => x.Id == id) ?? throw new UnauthorizedException("This product doesn't belong to you");

            prod.Amount = product.Amount;
            prod.Name = product.Name;
            prod.Description = product.Description;
            prod.Price = product.Price;
            if(product.ImageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    product.ImageFile.CopyTo(ms);
                    prod.Image = ms.ToArray();
                }
            }

            _unitOfWork.Products.Update(prod);
            await _unitOfWork.Save();
        }
    }
}
