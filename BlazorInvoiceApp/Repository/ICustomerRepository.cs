using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;

namespace BlazorInvoiceApp.Repository
{
    public interface ICustomerRepository : IGenericOwnedRepository<Customer , CustomerDTO>
    {
    }
}
