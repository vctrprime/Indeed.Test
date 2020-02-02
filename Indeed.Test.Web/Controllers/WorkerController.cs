using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indeed.Test.DataAccess.Repositories.Implementation;
using Indeed.Test.Models.Workers;
using Microsoft.AspNetCore.Mvc;

namespace Indeed.Test.Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class WorkerController : BaseController
    {
        private readonly WorkerRepository _repository;
        public WorkerController()
        {
            _repository = Context.Repository<Worker>() as WorkerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repository.GetAll();
                if (result is null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}