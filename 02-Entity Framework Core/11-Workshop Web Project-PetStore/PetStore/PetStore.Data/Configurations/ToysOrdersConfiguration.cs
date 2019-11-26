using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetStore.Models;

namespace PetStore.Data.Configurations
{
    public class ToysOrdersConfiguration : IEntityTypeConfiguration<ToysOrders>
    {
        public void Configure(EntityTypeBuilder<ToysOrders> toysOrders)
        {
            toysOrders.HasKey(to => new { to.OrderId, to.ToyId });

            toysOrders.HasOne(to => to.Order)
                .WithMany(o => o.OrdersToys)
                .HasForeignKey(to => to.OrderId);

            toysOrders.HasOne(to => to.Toy)
                .WithMany(t => t.ToyOrders)
                .HasForeignKey(to => to.ToyId);
        }
    }
}
