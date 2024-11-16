using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

public partial class Choice
{
    [Key]
    [Column("ChoiceID")]
    public int ChoiceId { get; set; }

    [Column("QuestionID")]
    public int QuestionId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string ChoiceText { get; set; } = null!;

    public bool IsCorrect { get; set; }

    [ForeignKey("QuestionId")]
    [InverseProperty("Choices")]
    public virtual Question Question { get; set; } = null!;
}
