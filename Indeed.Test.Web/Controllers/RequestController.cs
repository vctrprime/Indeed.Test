using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indeed.Test.DataAccess.Repositories;
using Indeed.Test.DataAccess.Repositories.Implementation;
using Indeed.Test.Factories;
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
        private readonly IBaseFactory _factory;
        public RequestController(IBaseFactory factory, IHubContext<DistributeHub, ITypedHubClient> hub = null, IRepository<Request> repository = null)
        {
            _factory = factory;
            if (repository is null)
                _repository = Context.Repository<Request>() as RequestRepository;
            else
                _repository = repository;
            hubContext = hub;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result =  _repository.GetAll();
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
        public IActionResult Get(int id)
        {
            try
            {
                var result = _repository.Get(id);
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
            request = _factory.CreateRequest(request);
            if (ModelState.IsValid)
            {
                try
                {
                    request = await _repository.CreateAsync(request);
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
                var result = await _repository.RemoveAsync(id.Value);
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