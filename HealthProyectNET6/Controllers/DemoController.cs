using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthProyectNET6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        // GET: api/<DemoController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "valor1", "valor2" });
        }

        // GET api/<DemoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok("Valor obtenido");
        }

        // POST api/<DemoController>
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return Ok("Registro exitoso");
        }

        // PUT api/<DemoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return Ok("Actualizacion exitoso");
        }

        // DELETE api/<DemoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok("Eliminacion exitoso");
        }
    }
}
