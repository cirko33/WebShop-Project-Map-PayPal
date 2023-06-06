using OnlineStoreApp.Models;

namespace OnlineStoreApp.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Order> Orders { get; }
        IRepository<Item> Items { get; }
        IRepository<Product> Products { get; }


        Task Save();

    }
}
