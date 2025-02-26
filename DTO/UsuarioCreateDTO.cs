using System.ComponentModel.DataAnnotations;

namespace CRUD_NET8.DTO
{
    public class UsuarioCreateDTO
    {
        [Required]
        [Display(Name = "Login")]
        public string UsuLogin { get; set; } = null!;

        [Required]
        [Display(Name = "Contrasenya")]
        public string UsuPwd { get; set; } = null!;

        [Required]
        [Display(Name = "Nombre")]
        public string UsuNombre { get; set; } = null!;

        [Required]
        [Display(Name = "Apellido")]
        public string UsuApellido { get; set; } = null!;

        [Required]
        [Display(Name = "Tipo de usuario")]
        public int TusuId { get; set; }
    }
}