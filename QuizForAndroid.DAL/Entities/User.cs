using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

[Index("Email", Name = "UQ__Users__A9D10534A35D02CE", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    


    [Column("UniversityID")]
    public int UniversityId { get; set; }

    [Column("CollegeID")]
    public int CollegeId { get; set; }

    [Column("SpecializationID")]
    public int SpecializationId { get; set; }

    [Column("RoleID")]
    public int RoleId { get; set; }

    public bool IsTrusted { get; set; }

    public bool IsDoctor { get; set; }

    [ForeignKey("CollegeId")]
    [InverseProperty("Users")]
    public virtual College College { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<QuizLikesDislike> QuizLikesDislikes { get; set; } = new List<QuizLikesDislike>();

    [InverseProperty("Writer")]
    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("SpecializationId")]
    [InverseProperty("Users")]
    public virtual Specialization Specialization { get; set; } = null!;

    [ForeignKey("UniversityId")]
    [InverseProperty("Users")]
    public virtual University University { get; set; } = null!;

    [InverseProperty("ReviewedByNavigation")]
    public virtual ICollection<WriterApplication> WriterApplicationReviewedByNavigations { get; set; } = new List<WriterApplication>();

    [InverseProperty("User")]
    public virtual ICollection<WriterApplication> WriterApplicationUsers { get; set; } = new List<WriterApplication>();
}
