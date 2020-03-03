using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indeed.Test.DataAccess.Repositories;
using Indeed.Test.DataAccess.Repositories.Implementation;
using Indeed.Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace Indeed.Test.Web.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly IRepository<Settings> _repository;
        public SettingsController(IRepository<Settings> repository = null)
        {
            if (repository is null)
                _repository = Context.Repository<Settings>() as SettingsRepository;
            else
                _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _repository.Get(0);
                if (result is null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm]Settings settings)
        {
            try
            {
                settings = await _repository.UpdateAsync(settings);
                return Ok(settings);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}