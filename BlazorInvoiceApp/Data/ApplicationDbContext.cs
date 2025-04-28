using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlazorInvoiceApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Invoice> Invoices { get; set;}
        public DbSet<Customer> Customers { get; set;}
        public DbSet<InvoiceTerms> InvoiceTerms { get; set;}
        public DbSet<InvoiceLineItem> invoiceLineItems { get; set;}

        protected void RemoveFixups (ModelBuilder modelBuilder , Type type)
        {
            foreach (var relationship in modelBuilder.Model.FindEntityType(type)!.GetForeignKeys())
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientNoAction;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            RemoveFixups(builder, typeof(Invoice));
            RemoveFixups(builder , typeof(Customer));
            RemoveFixups (builder , typeof(InvoiceTerms));
            RemoveFixups (builder, typeof(InvoiceLineItem));

            builder.Entity<Invoice>().Property(e => e.InvoiceNumber).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Entity<InvoiceLineItem>()
                   .Property(e => e.TotalPrice)
                   .HasComputedColumnSql("[UnitPrice] * [Quantity]");

            base.OnModelCreating(builder);
        }
    }
}
