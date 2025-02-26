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
        private readonly myDBContext _myDBContext;

        public UsuarioController(ILogger<UsuarioController> logger, myDBContext myDBContext)
        {
            _logger = logger;
            _myDBContext = myDBContext;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = _myDBContext.Usuarios.Include(x => x.Tusu);
            return View(await usuarios.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        public IActionResult Crear()
        {
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

                return RedirectToAction(nameof(Index));
            }

            //Aqui recuperamos los registros de la tabla TipoUsuario para mostrarla en 1 Combo en la vista
            ViewData["TipoUsuario"] = new SelectList(_myDBContext.TipoUsuarios,"TusuId", "TusuDesc", usuarioDTO.TusuId);

            return View(usuarioDTO);
        }

    }
}