using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfilRoblero.Modelos
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        public string UsuarioAplicacionId { get; set; }

        [ForeignKey("UsuarioAplicacionId")]
        public UsuarioAplicacion UsuarioAplicacion { get; set; }
        [Required(ErrorMessage = "Texto es Requerido")]
        public string Textos { get; set; }

        //[Required(ErrorMessage = "Usuario es Requerido")]
        //[Required]

    }
}
