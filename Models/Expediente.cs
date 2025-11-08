using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColegioSanJose.Models
{
    public class Expediente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpedienteId { get; set; }

        [Required(ErrorMessage = "El alumno es obligatorio")]
        [Display(Name = "Alumno")]
        public int AlumnoId { get; set; }

        [Required(ErrorMessage = "La materia es obligatoria")]
        [Display(Name = "Materia")]
        public int MateriaId { get; set; }

        [Required(ErrorMessage = "La nota final es obligatoria")]
        [Range(0, 10, ErrorMessage = "La nota debe estar entre 0 y 10")]
        [Display(Name = "Nota Final")]
        [Column(TypeName = "decimal(3,1)")]
        public decimal NotaFinal { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        [ForeignKey("AlumnoId")]
        public virtual Alumno Alumno { get; set; }

        [ForeignKey("MateriaId")]
        public virtual Materia Materia { get; set; }
    }
}