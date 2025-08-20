using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "Hola Mundo";
    }
}
