using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

[Keyless]
public partial class PendingWriterApplication
{
    [Column("ApplicationID")]
    public int ApplicationId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string FullName { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime ApplicationDate { get; set; }
}
