using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

[Index("MaterialId", "IsDraft", "CreatedDate", Name = "IX_Quizzes_MaterialID_IsDraft_CreatedDate", IsDescending = new[] { false, false, true })]
public partial class Quiz
{
    [Key]
    [Column("QuizID")]
    public int QuizId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [StringLength(1000)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("WriterID")]
    public int WriterId { get; set; }

    [Column("MaterialID")]
    public int MaterialId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public bool IsDraft { get; set; }

    public bool IsTrusted { get; set; }

    public bool IsDoctor { get; set; }

    [InverseProperty("Quiz")]
    public virtual ICollection<Draft> Drafts { get; set; } = new List<Draft>();

    [ForeignKey("MaterialId")]
    [InverseProperty("Quizzes")]
    public virtual Material Material { get; set; } = null!;

    [InverseProperty("Quiz")]
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    [InverseProperty("Quiz")]
    public virtual ICollection<QuizLikesDislike> QuizLikesDislikes { get; set; } = new List<QuizLikesDislike>();

    [ForeignKey("WriterId")]
    [InverseProperty("Quizzes")]
    public virtual User Writer { get; set; } = null!;
}
