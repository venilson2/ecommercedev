using Ecommercedev.Source.Core.DTOs;
using Ecommercedev.Source.Core.DTOs.ClientDTO;
using Ecommercedev.Source.Core.Entites;
using Ecommercedev.Source.Core.Interfaces.Repositories;
using Ecommercedev.Source.Core.Interfaces.Services;

namespace Ecommercedev.Source.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly CloudinaryService _cloudinaryService;

        public ClientService(IClientRepository clientRepository, CloudinaryService cloudinaryService)
        {
            _clientRepository = clientRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<IEnumerable<ReadClientDTO>> FindAllAsync()
        {
            try
            {
                var entities = await _clientRepository.FindAllAsync();

                var dtos = entities.Select(entity => new ReadClientDTO
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Email = entity.Email,
                    LogoUrl = entity.LogoUrl
                });

                return dtos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReadClientDTO> FindByIdAsync(Guid id)
        {
            try
            {
                var entity = await _clientRepository.FindByIdAsync(id);

                return new ReadClientDTO
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Email = entity.Email,
                    LogoUrl = entity.LogoUrl
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReadClientDTO> CreateAsync(CreateClientDTO dto)
        {
            try
            {
                var entity = new ClientEntity
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    LogoUrl = dto.LogoUrl
                };

                var createdEntity = await _clientRepository.CreateAsync(entity);

                return new ReadClientDTO
                {
                    Id = createdEntity.Id,
                    Name = createdEntity.Name,
                    Email = createdEntity.Email,
                    LogoUrl = createdEntity.LogoUrl
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ReadClientDTO> UpdateAsync(Guid id, UpdateClientDTO dto)
        {
            try
            {
                var entity = new ClientEntity
                {
                    Id = id,
                    Name = dto.Name,
                    Email = dto.Email,
                    LogoUrl = dto.LogoUrl
                };

                var updatedEntity = await _clientRepository.UpdateAsync(id, entity);

                return new ReadClientDTO
                {
                    Id = updatedEntity.Id,
                    Name = updatedEntity.Name,
                    Email = updatedEntity.Email,
                    LogoUrl = updatedEntity.LogoUrl
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                return await _clientRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
