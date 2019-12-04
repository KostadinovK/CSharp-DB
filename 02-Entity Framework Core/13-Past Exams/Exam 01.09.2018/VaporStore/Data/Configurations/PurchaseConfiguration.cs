using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaporStore.Data.Models;

namespace VaporStore.Data.Configurations
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> purchase)
        {
            purchase.HasOne(p => p.Card)
                .WithMany(c => c.Purchases)
                .HasForeignKey(p => p.CardId);
        }
    }
}
