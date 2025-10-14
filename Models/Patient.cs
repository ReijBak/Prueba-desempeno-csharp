using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prueba_desempeno_csharp.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required, MaxLength(45)]
        public string Names { get; set; }

        [Required, MaxLength(45)]
        public string LastNames { get; set; }

        [Required, MaxLength(45)]
        public string Email { get; set; }

        [MaxLength(45)]
        public string? Phone { get; set; }

        // Relación 1 (Pacient) a N (Appointments)
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}