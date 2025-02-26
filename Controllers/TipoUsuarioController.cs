using Context;
using CRUD_NET8.DTO;
using CRUD_NET8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_NET8.Controllers
{
    // [Route("[controller]")]
    public class TipoUsuarioController : Controller
    {
        private readonly ILogger<TipoUsuarioController> _logger;

        private readonly myDBContext _myDBContext;

        public TipoUsuarioController(ILogger<TipoUsuarioController> logger, myDBContext myDBContext)
        {
            _logger = logger;
            _myDBContext = myDBContext;
        }

        public async Task<IActionResult> Index()
        {
            //Devolvemos todos los tipos de usuario de nuestra BD
            return View(await _myDBContext.TipoUsuarios.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public IActionResult Crear(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //Es para que la información la coja del formulario
        public async Task<IActionResult> Crear(TipoUsuarioCreateDTO tipoUsuarioDTO){
            //Vamos a utilizar un DTO pero para nuestro ejemplo
            if (ModelState.IsValid) {
                var tipoUsuario = new TipoUsuario(){
                    TusuDesc = tipoUsuarioDTO.TusuDesc,
                    TusuObs = tipoUsuarioDTO.TusuObs,
                };
                _myDBContext.Add(tipoUsuario);
                await _myDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(tipoUsuarioDTO);

        }
    }
}