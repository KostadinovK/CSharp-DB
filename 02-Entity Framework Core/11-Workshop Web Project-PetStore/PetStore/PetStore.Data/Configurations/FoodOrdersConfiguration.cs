using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetStore.Models;

namespace PetStore.Data.Configurations
{
    public class FoodOrdersConfiguration : IEntityTypeConfiguration<FoodOrders>
    {
        public void Configure(EntityTypeBuilder<FoodOrders> foodOrders)
        {
            foodOrders.HasKey(fo => new { fo.FoodId, fo.OrderId });

            foodOrders.HasOne(fo => fo.Order)
                .WithMany(o => o.OrdersFood)
                .HasForeignKey(fo => fo.OrderId);

            foodOrders.HasOne(fo => fo.Food)
                .WithMany(f => f.OrdersFood)
                .HasForeignKey(fo => fo.FoodId);
        }
    }
}
