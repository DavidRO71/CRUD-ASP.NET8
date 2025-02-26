using System.ComponentModel.DataAnnotations;

namespace CRUD_NET8.DTO
{
    public class TipoUsuarioCreateDTO
    {

        [Required]
        [Display(Name = "Tipo de usuario")]
        public string TusuDesc { get; set; } = null!;

        [Display(Name = "Observaciones")]
        public string? TusuObs { get; set; }

    }
}