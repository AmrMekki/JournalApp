using System.ComponentModel.DataAnnotations;

namespace JournalApp.Models
{
    public class JournalEntry
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = "";

        public string Mood { get; set; } = "🙂";
    }
}
