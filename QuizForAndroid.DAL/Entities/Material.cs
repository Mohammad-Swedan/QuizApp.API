using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

public partial class Material
{
    [Key]
    [Column("MaterialID")]
    public int MaterialId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string MaterialName { get; set; } = null!;

    [Column("CollegeID")]
    public int CollegeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [ForeignKey("CollegeId")]
    [InverseProperty("Materials")]
    public virtual College College { get; set; } = null!;

    [InverseProperty("Material")]
    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
