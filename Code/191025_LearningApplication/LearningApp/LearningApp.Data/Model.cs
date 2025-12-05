using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LearningApp.Data;

public class VocabCard
{
    [Key]
    public int Id { get; set; }
    public int VocabSetId { get; set; }

    [Required]
    public string Language { get; set; } = string.Empty;
    [Required]
    public string Original { get; set; } = string.Empty;
    [Required]
    public string Translation { get; set; } = string.Empty;
    public string ExampleSentence { get; set; } = string.Empty;
    public int DifficultyLevel { get; set; }

    public virtual VocabSet VocabSet { get; set; } = null!;
}

public class VocabSet
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<VocabCard> VocabCards { get; set; } = new List<VocabCard>();
}