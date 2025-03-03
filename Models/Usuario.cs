using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD_NET8.Models;

public partial class Usuario
{
    [Display(Name = "ID Usuario")]
    public int UsuId { get; set; }

    [Display(Name = "Login")]
    public string UsuLogin { get; set; } = null!;

    [Display(Name = "Contraseña")]
    public string UsuPwd { get; set; } = null!;

    [Display(Name = "Nombre")]
    public string UsuNombre { get; set; } = null!;

    [Display(Name = "Apellido")]
    public string UsuApellido { get; set; } = null!;

    /// <summary>
    /// tabla tipo_usuario
    /// </summary>
    [Display(Name = "Tipo de Usuario")]
    public int TusuId { get; set; }

    public bool? UsuActivo { get; set; }

    public string? UsuBusqueda { get; set; }

    public int? TemporalId { get; set; }

    public string? TemporalText { get; set; }

    public string? UsuUc { get; set; }

    public DateTime UsuFc { get; set; }

    public string? UsuUm { get; set; }

    public DateTime? UsuFm { get; set; }

    public virtual TipoUsuario Tusu { get; set; } = null!;
}
