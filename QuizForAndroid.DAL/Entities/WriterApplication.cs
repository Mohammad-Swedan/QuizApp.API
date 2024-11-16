using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

public partial class WriterApplication
{
    [Key]
    [Column("ApplicationID")]
    public int ApplicationId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string FullName { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime ApplicationDate { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    public int? ReviewedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReviewedDate { get; set; }

    [ForeignKey("ReviewedBy")]
    [InverseProperty("WriterApplicationReviewedByNavigations")]
    public virtual User? ReviewedByNavigation { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("WriterApplicationUsers")]
    public virtual User User { get; set; } = null!;
}
