using Ecommercedev.Source.Core.DTOs;
using Ecommercedev.Source.Core.DTOs.ClientDTO;

namespace Ecommercedev.Source.Core.Interfaces.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ReadClientDTO>> FindAllAsync();
        Task<ReadClientDTO> FindByIdAsync(Guid id);
        Task<ReadClientDTO> CreateAsync(CreateClientDTO dto);
        Task<ReadClientDTO> UpdateAsync(Guid id, UpdateClientDTO dto);
        Task<int> DeleteAsync(Guid id);
    }
}
