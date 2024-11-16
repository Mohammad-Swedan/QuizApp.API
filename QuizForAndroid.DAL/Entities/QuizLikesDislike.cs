using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

[PrimaryKey("UserId", "QuizId")]
public partial class QuizLikesDislike
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [Key]
    [Column("QuizID")]
    public int QuizId { get; set; }

    public bool IsLike { get; set; }

    [ForeignKey("QuizId")]
    [InverseProperty("QuizLikesDislikes")]
    public virtual Quiz Quiz { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("QuizLikesDislikes")]
    public virtual User User { get; set; } = null!;
}
