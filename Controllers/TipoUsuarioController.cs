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
        // public IActionResult Volver(){
        //     Console.WriteLine("Entro salir");
        //     TempData["Mensaje"] = "Cancelación del usuario";
        //     return RedirectToAction("Index");
        // }
        [HttpPost]
        public IActionResult Volver()
        {
            // Lógica para el botón "Volver"
            // Console.WriteLine("Se presionó el botón Volver.");
            TempData["Titulo"]  = "Cancelación";
            TempData["Mensaje"] = "Cancelación del usuario";
            TempData["Icono"]   = "error";
            // Redirigir o realizar la acción correspondiente
            return RedirectToAction("Index"); // O la vista que necesites
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
            TempData["Mensaje"] = null;
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

                TempData["Titulo"]  = "Exitosamente";
                TempData["Mensaje"] = "Tipo de usuario se ha creado correctamente.";
                TempData["Icono"]   = "success";

                return RedirectToAction(nameof(Index));
            }

            TempData["Icono"] = "error";
            TempData["Mensaje"] = "Tipo de usuario no valido.";

            return View(tipoUsuarioDTO);
        }

        //*******************************************************
        //* E D I T A R
        //*******************************************************
        public IActionResult Editar(int id)
        {
            TempData["Titulo"]  = null;
            TempData["Mensaje"] = null;
            var tipoUsuarioModif = _myDBContext.TipoUsuarios.Where(x => x.TusuId == id).FirstOrDefault();
            return View(tipoUsuarioModif);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //Es para que la información la coja del formulario
        public async Task<IActionResult> Editar(TipoUsuario tipoUsuarioModif)
        {
            // Console.WriteLine("ID Usuario Editar: " + tipoUsuarioModif.TusuId);
            if (tipoUsuarioModif.TusuId > 0) {

                var miTipoUsuario = await _myDBContext.TipoUsuarios.FirstOrDefaultAsync(x => x.TusuId == tipoUsuarioModif.TusuId);
                if (miTipoUsuario == null){
                    return View(miTipoUsuario);
                }

                Console.WriteLine("Entro: " + tipoUsuarioModif.TusuObs);
                miTipoUsuario.TusuDesc = tipoUsuarioModif.TusuDesc;
                miTipoUsuario.TusuObs = tipoUsuarioModif.TusuObs;
            
                await _myDBContext.SaveChangesAsync();
                TempData["Titulo"]  = "Exitosamente";
                TempData["Mensaje"] = "Tipo de usuario actualizado correctamente.";
                TempData["Icono"]   = "success";

                // return RedirecktToAction(nameof(Index));
                return RedirectToAction("Index");
            }
            //Console.WriteLine("Usuario NO valido: ");
            TempData["Titulo"]  = "Error";
            TempData["Mensaje"] = "Tipo de usuario no valido.";
            TempData["Icono"]   = "error";

            return RedirectToAction("Index");
        }

        //*******************************************************
        //* E L I M I N A R 
        //*******************************************************
        public IActionResult Eliminar(int id)
        {
            // Console.WriteLine("ID Eliminar: " + id); // o
            // Console.WriteLine($"ID Eliminar: {id}");
            var tipoUsuarioEliminar = _myDBContext.TipoUsuarios.FirstOrDefault(x => x.TusuId == id);
            
            if (tipoUsuarioEliminar != null)
            {
                _myDBContext.TipoUsuarios.Remove(tipoUsuarioEliminar);
                _myDBContext.SaveChanges();
                TempData["Mensaje"] = "Usuario eliminado correctamente.";
            }
            else
            {
                TempData["Error"] = "Usuario no encontrado.";
            }

            return RedirectToAction("Index");
        }
    }
}