using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

public partial class Question
{
    [Key]
    [Column("QuestionID")]
    public int QuestionId { get; set; }

    [Column("QuizID")]
    public int QuizId { get; set; }

    [StringLength(1000)]
    [Unicode(false)]
    public string QuestionText { get; set; } = null!;

    public int QuestionType { get; set; }

    [InverseProperty("Question")]
    public virtual ICollection<Choice> Choices { get; set; } = new List<Choice>();

    [ForeignKey("QuizId")]
    [InverseProperty("Questions")]
    public virtual Quiz Quiz { get; set; } = null!;
}
