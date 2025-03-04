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

        private readonly MyDBContext _myDBContext;

        public TipoUsuarioController(ILogger<TipoUsuarioController> logger, MyDBContext myDBContext)
        {
            _logger = logger;
            _myDBContext = myDBContext;
        }

        //*******************************************************
        //* V O L V E R   A   L A   P A G I N A   I N I C I A L
        //*******************************************************
        public IActionResult Volver(){
            return RedirectToAction("Index");
        }
        
        //*******************************************************
        //* L I S T A R
        //*******************************************************
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

        //*******************************************************
        //* C R E A R
        //*******************************************************
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

        //*******************************************************
        //* E D I T A R
        //*******************************************************
        public IActionResult Editar(int id)
        {
            var tipoUsuarioModif = _myDBContext.TipoUsuarios.Where(x => x.TusuId == id).FirstOrDefault();
            return View(tipoUsuarioModif);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //Es para que la información la coja del formulario
        public async Task<IActionResult> Editar(TipoUsuario tipoUsuarioModif)
        {
            Console.WriteLine("ID Usuario Editar: " + tipoUsuarioModif.TusuId);
            if (tipoUsuarioModif.TusuId > 0) {

                var miTipoUsuario = await _myDBContext.TipoUsuarios.FirstOrDefaultAsync(x => x.TusuId == tipoUsuarioModif.TusuId);
                if (miTipoUsuario == null){
                    return View(miTipoUsuario);
                }

                Console.WriteLine("Entro: " + tipoUsuarioModif.TusuObs);
                miTipoUsuario.TusuDesc = tipoUsuarioModif.TusuDesc;
                miTipoUsuario.TusuObs = tipoUsuarioModif.TusuObs;
            
                await _myDBContext.SaveChangesAsync();
                TempData["Mensaje"] = "Tipo de usuario actualizado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            //Console.WriteLine("Usuario NO valido: ");
            TempData["Mensaje"] = "Tipo de usuario no valido.";

            return RedirectToAction("Index");
        }
    }
}