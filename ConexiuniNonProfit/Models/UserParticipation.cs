using ConexiuniNonProfit.Models;
using System.ComponentModel.DataAnnotations;

public class UserParticipation
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; }
    public int PostId { get; set; }
    public int Points { get; set; }
    public DateTime ParticipationDate { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual Post Post { get; set; }
}