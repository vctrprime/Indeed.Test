using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indeed.Test.DataAccess.Repositories;
using Indeed.Test.DataAccess.Repositories.Implementation;
using Indeed.Test.Models.Requests;
using Indeed.Test.Web.Infrastructure.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Indeed.Test.Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class RequestController : BaseController
    {
        private readonly IRepository<Request> _repository;
        private readonly IHubContext<DistributeHub, ITypedHubClient> hubContext;
        public RequestController(IHubContext<DistributeHub, ITypedHubClient> hub = null, IRepository<Request> repository = null)
        {
            if (repository is null)
                _repository = Context.Repository<Request>() as RequestRepository;
            else
                _repository = repository;
            hubContext = hub;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAll();
                if (result is null)
                    return NotFound();
                return Ok(result.OrderByDescending(x => x.CreatedDate).ToList());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _repository.Get(id);
                if (result is null)
                    return BadRequest($"Запрос {id} не найден!");
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]Request request)
        {
            request.Name = string.IsNullOrEmpty(request.Name) ? "unknown" : request.Name;
            request.CreatedBy = string.IsNullOrEmpty(request.CreatedBy) ? "unknown" : request.CreatedBy;
            request.CreatedDate = DateTime.Now.Ticks;
            if (ModelState.IsValid)
            {
                try
                {
                    request = await _repository.Create(request);
                    if (request.Id > 0)
                    {
                        if (hubContext != null)
                            await hubContext.Clients.All.SendMessageToClient();
                        return Ok(request);
                    }
                        
                    return BadRequest();

                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            return BadRequest(ModelState);
        }

        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            try
            {
                var result = await _repository.Remove(id.Value);
                if (result == 0)
                {
                    if (hubContext != null)
                        await hubContext.Clients.All.SendMessageToClient();
                    return Ok($"Запрос {id} успешно отменен!");
                }
                return BadRequest();
            }
            catch(NullReferenceException e)
            {
                return BadRequest($"Запрос {id} не найден!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}