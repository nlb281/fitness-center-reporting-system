using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterReportingSystem.Models;

[Table("visits")]
public partial class Visit
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("section_id")]
    public int SectionId { get; set; }

    [Column("coach_id")]
    public int CoachId { get; set; }

    [Column("registered_visitors")]
    public int RegisteredVisitors { get; set; }

    [Column("attended_visitors")]
    public int AttendedVisitors { get; set; }

    [ForeignKey("CoachId")]
    [InverseProperty("Visits")]
    public virtual Coach Coach { get; set; } = null!;

    [ForeignKey("SectionId")]
    [InverseProperty("Visits")]
    public virtual Section Section { get; set; } = null!;
}
