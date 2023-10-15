using Microsoft.AspNetCore.Mvc;
using Ecommercedev.Source.Core.DTOs;
using Ecommercedev.Source.Core.DTOs.ClientDTO;
using Ecommercedev.Source.Core.Interfaces.Services;

namespace Ecommercedev.Source.Application.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            try
            {
                IEnumerable<ReadClientDTO> clients = await _clientService.FindAllAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            try
            {
                ReadClientDTO client = await _clientService.FindByIdAsync(id);

                if (client == null)
                {
                    return NotFound();
                }

                return Ok(client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientDTO clientDto)
        {
            try
            {
                if (clientDto == null)
                {
                    return BadRequest("Cliente inválido");
                }

                var createdClient = await _clientService.CreateAsync(clientDto);
                return CreatedAtAction(nameof(FindById), new { id = createdClient.Id }, createdClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClientDTO client)
        {
            try
            {
                if (client == null)
                {
                    return BadRequest("Cliente inválido");
                }

                ReadClientDTO existingClient = await _clientService.FindByIdAsync(id);

                if (existingClient == null)
                {
                    return NotFound();
                }

                var updatedClient = await _clientService.UpdateAsync(id, client);
                return Ok(updatedClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var existingClient = await _clientService.FindByIdAsync(id);

                if (existingClient == null)
                {
                    return NotFound();
                }

                var result = await _clientService.DeleteAsync(id);
                if (result > 0)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500, "Falha ao excluir o cliente");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
