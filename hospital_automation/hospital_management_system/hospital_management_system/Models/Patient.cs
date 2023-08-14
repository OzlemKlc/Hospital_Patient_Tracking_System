using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hospital_management_system.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik Numarası 11 karakter olmalıdır.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "TC Kimlik Numarası sadece rakamlardan oluşmalıdır.")]
        public string TcNum { get; set; }

        [Required]
        [StringLength(40)]
        public string NameSurname { get; set; }
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        //public List<Visit> Visits { get; set; }
    }
}
