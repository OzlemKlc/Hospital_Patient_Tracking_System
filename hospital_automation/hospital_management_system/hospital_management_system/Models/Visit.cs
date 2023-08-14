using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_management_system.Models
{
    public class Visit
    {
        [Key]
        public int? Id { get; set; }

        public int PatientId { get; set; } 

        [Required(ErrorMessage = "Ziyaret tarihi alanı boş bırakılamaz.")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "timestamp(0) without time zone")]
        public DateTime VisitingDate { get; set; }

        [Required(ErrorMessage = "Doktor Adı alanı boş bırakılamaz.")]
        public string DoctorName { get; set; } = null!;

        public string Complaint { get; set; } = null!;

        public string TypeOfTreatment { get; set; } = null!;
    }
}
