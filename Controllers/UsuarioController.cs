using Context;
using CRUD_NET8.DTO;
using CRUD_NET8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRUD_NET8.Controllers
{
    // [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly MyDBContext _myDBContext;

        public UsuarioController(ILogger<UsuarioController> logger, MyDBContext myDBContext)
        {
            _logger = logger;
            _myDBContext = myDBContext;
        }

        //*******************************************************
        //* V O L V E R   A   L A   P A G I N A   I N I C I A L
        //*******************************************************
        [HttpPost]
        public IActionResult Volver()
        {
            // Console.WriteLine("Se presionó el botón Volver.");
            TempData["Titulo"]  = "Cancelación";
            TempData["Mensaje"] = "Cancelación del usuario";
            TempData["Icono"]   = "error";
            // Redirigir al index
            return RedirectToAction("Index");
        }

        //*******************************************************
        //* L I S T A R
        //*******************************************************
        public async Task<IActionResult> Index()
        {
            //Devolvemos todos los usuario de nuestra BD con su tipo
            var usuarios = _myDBContext.Usuarios.Include(x => x.Tusu);
            return View(await usuarios.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        //*******************************************************
        //* C R E A R
        //*******************************************************
        public IActionResult Crear()
        {
            TempData["Titulo"]  = null;
            TempData["Mensaje"] = null;
            TempData["Icono"]   = null;

            //recuperamos todos los tipos de usuarios para mostrarlos en el combo del formulario CREAR
            ViewData["TipoUsuario"] = new SelectList(_myDBContext.TipoUsuarios,"TusuId", "TusuDesc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //Es para que la información la coja del formulario
        public async Task<IActionResult> Crear(UsuarioCreateDTO usuarioDTO)
        {
            //Vamos a utilizar un DTO pero para nuestro ejemplo no vamos a guardar todos los datos del usuario
            //Solo vamos a guardar el nombre, el apellido y el tipo de usuario
            if (ModelState.IsValid) {
                var usuario = new Usuario(){
                    UsuLogin = usuarioDTO.UsuLogin,
                    UsuPwd = usuarioDTO.UsuPwd,
                    UsuNombre = usuarioDTO.UsuNombre,
                    UsuApellido = usuarioDTO.UsuApellido,
                    TusuId = usuarioDTO.TusuId,
                };
                _myDBContext.Add(usuario);
                await _myDBContext.SaveChangesAsync();

                TempData["Titulo"]  = "Exitosamente";
                TempData["Mensaje"] = "El Usuario se ha creado correctamente.";
                TempData["Icono"]   = "success";

                return RedirectToAction(nameof(Index));
            }

            //Aqui recuperamos los registros de la tabla TipoUsuario para mostrarla en 1 Combo en la vista
            ViewData["TipoUsuario"] = new SelectList(_myDBContext.TipoUsuarios,"TusuId", "TusuDesc", usuarioDTO.TusuId);

            TempData["Icono"] = "error";
            TempData["Mensaje"] = "El Usuario no es válido.";

            return View(usuarioDTO);
        }

        //*******************************************************
        //* E D I T A R
        //*******************************************************
        public IActionResult Editar(int id)
        {
            TempData["Titulo"]  = null;
            TempData["Mensaje"] = null;
            TempData["Icono"]   = null;

            ViewData["TipoUsuario"] = new SelectList(_myDBContext.TipoUsuarios,"TusuId", "TusuDesc");
            var usuarioModif = _myDBContext.Usuarios.Where(x => x.UsuId == id).Include(x => x.Tusu).FirstOrDefault();
            return View(usuarioModif);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  //Es para que la información la coja del formulario
        public async Task<IActionResult> Editar(Usuario usuarioModif)
        {
            //Console.WriteLine("ID Usuario Editar: " + usuarioModif.UsuId);
            if (usuarioModif.UsuId > 0) {

                var miUsuario = await _myDBContext.Usuarios.FirstOrDefaultAsync(x => x.UsuId == usuarioModif.UsuId);
                if (miUsuario == null){
                    return View(usuarioModif);
                }

                //Console.WriteLine("Entro: " + usuarioModif.UsuNombre);
                miUsuario.UsuLogin = usuarioModif.UsuLogin;
                miUsuario.UsuPwd = usuarioModif.UsuPwd;
                miUsuario.UsuNombre = usuarioModif.UsuNombre;
                miUsuario.UsuApellido = usuarioModif.UsuApellido;
                miUsuario.TusuId = usuarioModif.TusuId;
            
                await _myDBContext.SaveChangesAsync();
                TempData["Titulo"]  = "Exitosamente";
                TempData["Mensaje"] = "El Usuario se ha actualizado correctamente.";
                TempData["Icono"]   = "success";

                return RedirectToAction(nameof(Index));
            }
            //Console.WriteLine("Usuario NO valido: ");
            TempData["Titulo"]  = "Error";
            TempData["Mensaje"] = "El Usuario no es válido.";
            TempData["Icono"]   = "error";

            //Aqui recuperamos los registros de la tabla TipoUsuario para mostrarla en 1 Combo en la vista
            ViewData["TipoUsuario"] = new SelectList(_myDBContext.TipoUsuarios,"TusuId", "TusuDesc", usuarioModif.TusuId);

            return RedirectToAction("Index");
        }


        //*******************************************************
        //* E L I M I N A R 
        //*******************************************************
        public IActionResult Eliminar(int id)
        {
            // Console.WriteLine("ID Eliminar: " + id); // o
            // Console.WriteLine($"ID Eliminar: {id}");
            var UsuarioEliminar = _myDBContext.Usuarios.FirstOrDefault(x => x.UsuId == id);
            
            if (UsuarioEliminar != null)
            {
                _myDBContext.Usuarios.Remove(UsuarioEliminar);
                _myDBContext.SaveChanges();
                TempData["Titulo"]  = "Exitosamente";
                TempData["Mensaje"] = "El Usuario se ha eliminado correctamente.";
                TempData["Icono"]   = "success";
            }
            else
            {
                TempData["Titulo"]  = "Error";
                TempData["Mensaje"] = "El Usuario no se ha encontrado.";
                TempData["Icono"]   = "error";
            }

            return RedirectToAction("Index");
        }
    }

}