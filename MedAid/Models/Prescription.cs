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
        public int PatientId { get; set; } // Foreign key to the Patient (Updated to int)

        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; } // Navigation property

        [Required]
        public string MedicationName { get; set; }

        [Required]
        public string Dosage { get; set; }

        public string Frequency { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        [NotMapped]
        public string DoctorName { get; set; }

    }

}
