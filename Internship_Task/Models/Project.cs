using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Internship_Task.Models
{
    [Table("project")]
    public class Project
    {
        [Key]
        [Column("ProjectId")]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Project name is required!")]
        [StringLength(50, ErrorMessage = "Project name cannot be longer than 50 characters")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Start date is required!")]
        [DataType(DataType.Date)]
        [Display(Name = "Starting Date" )]
        public DateTime StartDate { get; set; }

        // Enumerator for project status
        // NotStarted = 1; Active = 2; Completed = 3;
        public enum ProjectStatus
        {
            NotStarted = 1,
            Active,
            Completed
        }

        public ProjectStatus Status { get; set; }

        [Required(ErrorMessage = "Priority is required!")]
        // Range between 1 and 5
        [Range(1, 5)]
        public int Priority { get; set; }

        // Nullable fields
        #nullable enable
        [DataType(DataType.Date)]
        [Display(Name = "Completion Date")]
        public DateTime? CompletionDate { get; set; }

        [SwaggerSchema(ReadOnly = true)]
        public ICollection<Task>? Tasks { get; set; }
        #nullable disable
    }
}
