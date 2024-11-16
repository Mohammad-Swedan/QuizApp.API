using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

public partial class Specialization
{
    [Key]
    [Column("SpecializationID")]
    public int SpecializationId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string SpecializationName { get; set; } = null!;

    [Column("CollegeID")]
    public int CollegeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [ForeignKey("CollegeId")]
    [InverseProperty("Specializations")]
    public virtual College College { get; set; } = null!;

    [InverseProperty("Specialization")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
