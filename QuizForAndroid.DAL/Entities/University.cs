using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

[Index("UniversityName", Name = "UQ__Universi__53F0B53C07D8FA45", IsUnique = true)]
public partial class University
{
    [Key]
    [Column("UniversityID")]
    public int UniversityId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string UniversityName { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [InverseProperty("University")]
    public virtual ICollection<College> Colleges { get; set; } = new List<College>();

    [InverseProperty("University")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
