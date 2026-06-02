using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterReportingSystem.Models;

[Table("sections")]
[Index("Name", Name = "sections_unique", IsUnique = true)]
public partial class Section
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name", TypeName = "character varying")]
    public string Name { get; set; } = null!;

    [InverseProperty("Section")]
    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

    [InverseProperty("Section")]
    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
