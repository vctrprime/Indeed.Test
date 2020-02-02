using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indeed.Test.DataAccess.Repositories.Implementation;
using Indeed.Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace Indeed.Test.Web.Controllers
{
    public class SettingsController : BaseController
    {
        private readonly SettingsRepository _repository;
        public SettingsController()
        {
            _repository = Context.Repository<Settings>() as SettingsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _repository.Get(0);
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
            if (ModelState.IsValid)
            {
                try
                {
                    settings = await _repository.Update(settings);
                    return Ok(settings);
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