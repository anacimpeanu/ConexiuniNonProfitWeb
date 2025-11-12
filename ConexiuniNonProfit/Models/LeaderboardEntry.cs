using ConexiuniNonProfit.Models;
using System.ComponentModel.DataAnnotations;

public class LeaderboardEntry
{
    //[Key]
    public int Id { get; set; }

    public ApplicationUser User { get; set; }
    public int Points { get; set; }

     public int Rank { get; set; }
     public DateTime LastParticipationDate { get; set; }
}