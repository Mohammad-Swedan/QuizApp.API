using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace QuizForAndroid.DAL.Entities;

[Table("QuestionTypeInfo")]
[Index("TypeName", Name = "UQ__Question__D4E7DFA8393065AB", IsUnique = true)]
public partial class QuestionTypeInfo
{
    [Key]
    [Column("QuestionTypeID")]
    public int QuestionTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string TypeName { get; set; } = null!;
}
