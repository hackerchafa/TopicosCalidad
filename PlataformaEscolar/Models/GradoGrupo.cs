using System.ComponentModel.DataAnnotations;

namespace PlataformaEscolar.Models
{
    public class GradoGrupo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
} 