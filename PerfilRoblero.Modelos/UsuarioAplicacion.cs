using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PerfilRoblero.Modelos
{
    public class UsuarioAplicacion : IdentityUser
    {
        [Required(ErrorMessage = "Nombres es Requerido")]
        [MaxLength(80)]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Apellidos es Requerido")]
        [MaxLength(80)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Ciudad es Requerido")]
        [MaxLength(100)]
        public string Ciudad { get; set; }

        [NotMapped]  // No se agrega a la tabla es lo que indica en la propiedad notmapped
        public string Role { get; set; }//es una esoecia de referencia
    }
}
