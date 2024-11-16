using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

[Keyless]
public partial class QuizPopularity
{
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

    public int? Likes { get; set; }

    public int? Dislikes { get; set; }
}
