using System;
using System.Collections.Generic;

namespace CRUD_NET8.Models;

public partial class Usuario
{
    public int UsuId { get; set; }

    public string UsuLogin { get; set; } = null!;

    public string UsuPwd { get; set; } = null!;

    public string UsuNombre { get; set; } = null!;

    public string UsuApellido { get; set; } = null!;

    /// <summary>
    /// tabla tipo_usuario
    /// </summary>
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
