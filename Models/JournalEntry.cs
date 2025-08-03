using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public List<string> Tags { get; set; } = new List<string>();

        // 👉 This is just for form input (not saved to JSON)
        [NotMapped]
        public string TagsInput
        {
            get => string.Join(", ", Tags ?? []);
            set => Tags = value?.Split(',').Select(t => t.Trim()).Where(t => !string.IsNullOrWhiteSpace(t)).ToList() ?? new List<string>();
        }
    }
}
