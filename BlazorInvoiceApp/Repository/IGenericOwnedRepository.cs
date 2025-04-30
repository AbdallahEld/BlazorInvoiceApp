using BlazorInvoiceApp.Data;
using BlazorInvoiceApp.DTOS;
using System.Security.Claims;

namespace BlazorInvoiceApp.Repository
{
    public interface IGenericOwnedRepository <TEntity , TDTO> 
        where TEntity : class , IEntity , IOwnedEntity
        where TDTO : class, IDTO , IOwnedDTO
    {
        Task<TDTO> GetMineById (ClaimsPrincipal user, string id);
        Task<List<TDTO>> GetAllMine (ClaimsPrincipal user);
        Task<string> AddMine (ClaimsPrincipal user, TDTO dto);
        Task<TDTO> UpdateMine (ClaimsPrincipal user, TDTO dto);
        Task<bool> DeleteMine (ClaimsPrincipal user, string id);
    }
}
