using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_desempeno_csharp.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [MaxLength(45)]
        public string? Observations { get; set; }
        
        
        public string State { get; set; }  = "Pending";

        // Clave foránea hacia Pacient
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        // Clave foránea hacia Medic
        [ForeignKey("Medic")]
        public int MedicId { get; set; }
        public Medic? Medic { get; set; }
    }
}