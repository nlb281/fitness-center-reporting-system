using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterReportingSystem.Models;

[Table("coaches")]
public partial class Coach
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("fio", TypeName = "character varying")]
    public string Fio { get; set; } = null!;

    [Column("section_id")]
    public int SectionId { get; set; }

    [ForeignKey("SectionId")]
    [InverseProperty("Coaches")]
    public virtual Section Section { get; set; } = null!;

    [InverseProperty("Coach")]
    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
