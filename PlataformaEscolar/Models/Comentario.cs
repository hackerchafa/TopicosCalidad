using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEscolar.Models
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EmisorId { get; set; }
        [Required]
        public int ReceptorId { get; set; }
        [Required]
        public bool EsDeProfesor { get; set; }
        [Required]
        public string Texto { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
} 