using BlazorInvoiceApp.Data;

namespace BlazorInvoiceApp.DTOS
{
    public class InvoiceDTO : IDTO, IOwnedDTO
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int InvoiceNumber { get; set; }
        public string Description { get; set; } = String.Empty;
        public string CustomerId { get; set; } = String.Empty;
        public string CustomerName { get; set; } = String.Empty;
        public string InvoiceTermsId { get; set; } = String.Empty;
        public string InvoiceTermsName { get; set; } = String.Empty;
        public double Paid { get; set; } = 0;
        public double Credit { get; set; } = 0;
        public double TaxRate { get; set; } = 0;
        public double InvoiceTotal { get; set; } = 0;
        public string UserId { get; set; } = null!;
    }
}
