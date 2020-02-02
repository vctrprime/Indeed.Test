using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indeed.Test.DataAccess.Repositories.Implementation;
using Indeed.Test.Models.Workers;
using Microsoft.AspNetCore.Mvc;

namespace Indeed.Test.Web.Controllers
{
    [Route("[controller]s")]
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

        [HttpPost("create")]
        public async Task<IActionResult> Create(Worker worker)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    worker = Context.workerFactory.CreateWorker(worker);
                    worker = await _repository.Create(worker);
                    if (worker.Id > 0)
                    {
                        return Ok(worker);
                    }

                    return NotFound();

                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            return BadRequest(ModelState);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Worker worker)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    worker = Context.workerFactory.CreateWorker(worker);
                    worker = await _repository.Update(worker);
                    return Ok(worker);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            return BadRequest(ModelState);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(Worker worker)
        {
            if (worker.Id == 0)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _repository.Remove(worker.Id);
                    if (result == 0)
                        return Ok(worker);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

            }
            return BadRequest(ModelState);
        }



    }
}