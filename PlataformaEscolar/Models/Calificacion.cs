using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEscolar.Models
{
    public class Calificacion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AlumnoId { get; set; }
        [Required]
        public int ProfesorId { get; set; }
        public string Materia { get; set; } = string.Empty;
        [Required]
        public decimal Valor { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
} 