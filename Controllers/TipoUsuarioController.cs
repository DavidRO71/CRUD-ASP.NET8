using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
    }
}