using System.ComponentModel.DataAnnotations;

namespace PerfilRoblero.Modelos
{
    public class Archivo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre es Requerido")]
        [MaxLength(60, ErrorMessage = "Nombre debe de ser Maximo a 60 caracteres")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Estado es Requerido")]
        public bool Estado { get; set; }
        public string ArchivoUrl { get; set; }
    }
}
