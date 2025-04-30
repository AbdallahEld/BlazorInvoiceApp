using AutoMapper;
using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;

namespace BlazorInvoiceApp.Repository
{
    public class InvoiceRepository : GenericOwnedRepository<Invoice , InvoiceDTO>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context,IMapper mapper) : base(context,mapper)
        {
            
        }
    }
}
