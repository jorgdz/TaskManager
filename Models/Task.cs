using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Task
    {
      public long Id { get; set; }

      [Required]
      public string Name { get; set; }
      public bool IsComplete { get; set; }
    }
}