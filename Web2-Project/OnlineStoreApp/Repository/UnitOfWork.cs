using OnlineStoreApp.Interfaces;
using OnlineStoreApp.Models;
using OnlineStoreApp.Settings;

namespace OnlineStoreApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        public IRepository<User> Users { get; }
        public IRepository<Order> Orders { get; }
        public IRepository<Item> Items { get; }
        public IRepository<Product> Products { get; }

        public UnitOfWork(StoreDbContext context, IRepository<User> users, IRepository<Order> orders, IRepository<Item> items, IRepository<Product> products)
        {
            _context = context;
            Users = users;
            Orders = orders;
            Items = items;
            Products = products;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
