using System;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEscolar.Models
{
    public class ClaseHorario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NombreClase { get; set; } = string.Empty;
        [Required]
        public int ProfesorId { get; set; }
        [Required]
        public int GradoGrupoId { get; set; }
        [Required]
        public string DiaSemana { get; set; } = string.Empty;
        [Required]
        public TimeSpan HoraInicio { get; set; }
        [Required]
        public TimeSpan HoraFin { get; set; }
    }
} 