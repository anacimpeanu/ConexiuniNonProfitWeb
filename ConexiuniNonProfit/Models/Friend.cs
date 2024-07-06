﻿using ConexiuniNonProfit.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ConexiuniNonProfit.Models;


public class Friend
{
	[Key]
	public int FriendId { get; set; }

	[ForeignKey("User1")]
	public string User1_Id { get; set; }
	public virtual ApplicationUser User1 { get; set; }

	[ForeignKey("User2")]
	public string User2_Id { get; set; }
	public virtual ApplicationUser User2 { get; set; }

	public DateTime RequestTime { get; set; }

	public bool Accepted { get; set; }
	public int? ActiuniId { get; set; }
	public virtual Actiuni Actiuni { get; set; }
}
