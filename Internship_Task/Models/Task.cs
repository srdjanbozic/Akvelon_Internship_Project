using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Task = Internship_Task.Models.Task;

namespace Internship_Task.Models
{
    [Table("task")]
    public class Task
    {
        [Key]
        [Column("TaskId")]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Task name is required!")]
        [MaxLength(50, ErrorMessage = "Length must be less than 50 characters")]
        public string TaskName { get; set; }

        // Enumerator for task status
        // ToDo = 1; InProgress = 2; Done = 3;
        public enum TaskStatus
        {
            ToDo = 1,
            InProgress,
            Done
        }
        public TaskStatus Status { get; set; }

        [Required(ErrorMessage = "Status is required!")]
        // Range between 1 and 5
        [Range(1, 5)]
        public int TaskPriority { get; set; }

        //Foreign Key

        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        // Nulable fields
        #nullable enable
        [StringLength(500, ErrorMessage = "Task description cannot be longer than 500 characters")]
        public string? TaskDescription { get; set; }

        // Navigation property
        [SwaggerSchema(ReadOnly = true)]
        [JsonIgnore]
        public Project? Project { get; set; }
        #nullable disable
    }
}
