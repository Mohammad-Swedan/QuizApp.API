using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

public partial class Draft
{
    [Key]
    [Column("DraftID")]
    public int DraftId { get; set; }

    [Column("QuizID")]
    public int QuizId { get; set; }

    [ForeignKey("QuizId")]
    [InverseProperty("Drafts")]
    public virtual Quiz Quiz { get; set; } = null!;
}
