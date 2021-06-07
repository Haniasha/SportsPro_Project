// Authors: Cecilia Santiago, David McDonald, Ehsan Jalali, Hanieh Shahrokhi
// Workshop 5
// Date May 28, 2021

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SportsPro.Models
{
    internal class RegistrationConfig : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> entity)
        {
            entity.HasKey(r => new { r.CustomerID, r.ProductID });

            // one-to-many relationship between Customer and Registrations            
            entity.HasOne(r => r.Customer)
                  .WithMany(c => c.Registrations)
                  .HasForeignKey(r => r.CustomerID);

            // one-to-many relationship between Product and Registrations            
            entity.HasOne(r => r.Product)
                  .WithMany(p => p.Registrations)
                  .HasForeignKey(r => r.ProductID);

        }
    }
}
