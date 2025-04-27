using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorInvoiceApp.Data
{
    public class Invoice : IEntity, IOwnedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = null!;
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceNumber { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Description { get; set; } = String.Empty;
        public string CustomerId { get; set; } = String.Empty;
        public string InvoiceTermsId { get; set; } = String.Empty;
    }
}
