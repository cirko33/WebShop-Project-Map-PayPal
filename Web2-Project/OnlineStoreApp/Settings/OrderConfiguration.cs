using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineStoreApp.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace OnlineStoreApp.Settings
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DeliveryAddress).HasMaxLength(200).IsRequired();
            builder.Property(x => x.OrderTime).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.DeliveryTime).IsRequired();
            builder.Property(x => x.Comment).HasMaxLength(200);
            builder.Property(x => x.IsCancelled).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.OrderPrice).IsRequired();
            builder.Property(x => x.Approved).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.PositionX).IsRequired();
            builder.Property(x => x.PositionY).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.Orders).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new Order
            {
                Id = 1,
                DeliveryAddress = "Dr Sime Milosevica 10, Novi Sad",
                DeliveryTime = DateTime.Now.AddMinutes(new Random().Next(180)),
                IsCancelled = false,
                UserId = 3,
                OrderPrice = 9.5,
                PositionX = 45.24500555642036,
                PositionY = 19.850283596223083
            }); 
        }
    }
}
