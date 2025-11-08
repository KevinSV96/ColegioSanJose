using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColegioSanJose.Models
{
    public class Materia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MateriaId { get; set; }

        [Required(ErrorMessage = "El nombre de la materia es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nombre de la Materia")]
        public string NombreMateria { get; set; }

        [Required(ErrorMessage = "El docente es obligatorio")]
        [StringLength(100)]
        public string Docente { get; set; }

        public virtual ICollection<Expediente> Expedientes { get; set; }
    }
}