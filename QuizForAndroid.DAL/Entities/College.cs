using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

public partial class College
{
    [Key]
    [Column("CollegeID")]
    public int CollegeId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string CollegeName { get; set; } = null!;

    [Column("UniversityID")]
    public int UniversityId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [InverseProperty("College")]
    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    [InverseProperty("College")]
    public virtual ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();

    [ForeignKey("UniversityId")]
    [InverseProperty("Colleges")]
    public virtual University University { get; set; } = null!;

    [InverseProperty("College")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
