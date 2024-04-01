using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IService _service;

        public ValuesController(IService service)
        {
            _service = service;
        }
        [HttpPost]
        public Planepath Create(Field model)
        {
            return _service.Create(model);
        }


    }
}
