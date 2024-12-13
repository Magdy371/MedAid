using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedAid.Models
{
#nullable disable
    public class Prescription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DoctorId { get; set; } // Link to the Doctor user

        [Required]
        public string PatientId { get; set; } // Link to the Patient user

        [Required]
        public string MedicationName { get; set; }

        [Required]
        public string Dosage { get; set; }

        public string Frequency { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}
