using AutoMapper;
using Billing_API_Net8.Controllers.Resources;
using Billing_API_NET8.Helpers;
using Billing_API_Net8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
//using System.Data.Entity;

namespace Billing_API_NET8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public ClientController(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //[AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetClient()
        {
            var list = await context.Client.OrderBy(c => c.Name).ToListAsync();
            return Ok(mapper.Map<List<Client>, List<ClientResource>>(list));
        }

        [HttpGet("GetClient1/{Balance}")]
        public async Task<IActionResult> GetClient1(decimal Balance)
        {
            var list = await context.Client
                .Where(c => c.Balance >= Balance)
                .ToListAsync();

            return Ok(mapper.Map<List<Client>, List<ClientResource>>(list));
        }

        [HttpGet("GetClient2")]
        public async Task<IActionResult> GetClient2([FromQuery] decimal Balance)
        {
            var list = await context.Client
                .Where(c => c.Balance >= Balance)
                .ToListAsync();

            return Ok(mapper.Map<List<Client>, List<ClientResource>>(list));

        }

        [HttpPost]
        public async Task<IActionResult> PostClient([FromBody] Client payload)
        {
            Client newRegister = new Client();
            newRegister.Id = Guid.NewGuid();
            newRegister.Name = payload.Name;
            newRegister.Address = payload.Address;
            newRegister.Balance = payload.Balance;

            context.Client.Add(newRegister);
            context.SaveChanges();

            return Ok(newRegister);
        }
        [HttpPost("Post100")]
        public async Task<IActionResult> Post100Client()
        {
            List<Client> clientsToCreate = new List<Client>();

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    Client newRegister = new Client();
                    newRegister.Id = Guid.NewGuid();
                    newRegister.Name = $"client{i}";
                    newRegister.Address = $"address{i}";
                    newRegister.Balance = 0; // Or other default value

                    clientsToCreate.Add(newRegister);
                }

                context.Client.AddRange(clientsToCreate);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log or handle database errors
                return BadRequest("An error occurred while creating clients. Please check logs for details.");
            }

            return Ok("Successfully created 100 clients.");
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> PutClient(Guid id, [FromBody] ClientResource resource)

        {

            var register = await context.Client.Where(c => c.Id == id).FirstOrDefaultAsync(); //Para que devuelva lo primero que encuentre

            mapper.Map<ClientResource, Client>(resource, register); //Actualiza

            await context.SaveChangesAsync();

            var result = mapper.Map<ClientResource, Client>(resource);

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var register = await context.Client.Where(c => c.Id == id).ExecuteDeleteAsync();
            return Ok();

        }

}
}