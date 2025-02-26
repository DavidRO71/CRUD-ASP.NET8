using System;
using System.Collections.Generic;

namespace CRUD_NET8.Models;

public partial class TipoUsuario
{
    public int TusuId { get; set; }

    public string TusuDesc { get; set; } = null!;

    public string? TusuObs { get; set; }

    public bool? TusuActivo { get; set; }

    public string? TemporalText { get; set; }

    public int? TemporalId { get; set; }

    public string? TusuUc { get; set; }

    public DateTime TusuFc { get; set; }

    public string? TusuUm { get; set; }

    public DateTime? TusuFm { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
