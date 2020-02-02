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
            bool ValidateForm()
            {
                if (settings.TimeDirector <= settings.TimeManager ||
                    settings.ExecuteTimeLimitRight <= settings.ExecuteTimeLimitLeft ||
                    settings.TimeDirector < 5 || settings.TimeDirector > 100 ||
                    settings.TimeManager < 5 || settings.TimeManager > 100 ||
                    settings.ExecuteTimeLimitRight < 5 || settings.ExecuteTimeLimitRight > 100 ||
                    settings.ExecuteTimeLimitLeft < 5 || settings.ExecuteTimeLimitLeft > 100)
                    return false;
                return true;

            }
            if (ValidateForm())
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
            return BadRequest(Json(_repository.Get(0)));
        }
    }
}