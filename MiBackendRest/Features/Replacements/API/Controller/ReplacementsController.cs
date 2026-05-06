using Microsoft.AspNetCore.Mvc;
using MiBackendRest.Features.Replacements.Domain.Entity;

namespace MiBackendRest.Features.Replacements.API.Controller
{
        [ApiController]
        [Route("api/repuestos")]
        public class RepuestosController : ControllerBase
        {
            [HttpGet]
            public IActionResult GetAllReplacements()
            {
                var Replacements = new List<Replacement>
            {
                new Replacement { Id = 1, Nombre = "Filtro de aceite", Precio = 15000 },
                new Replacement { Id = 2, Nombre = "Bujía", Precio = 8000 },
                new Replacement { Id = 3, Nombre = "Cadena", Precio = 45000 },
                new Replacement { Id = 4, Nombre = "Pastillas de freno", Precio = 30000 },
                new Replacement { Id = 5, Nombre = "Aceite motor", Precio = 20000 }
            };

                return Ok(Replacements);
            }
        }
    
}
