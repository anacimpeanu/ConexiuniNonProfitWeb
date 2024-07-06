using ConexiuniNonProfit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConexiuniNonProfit.Models
{
	public class ApplicationUser : IdentityUser
	{

		public virtual ICollection<Comment>? Comments { get; set; }
		public virtual ICollection<Post>? Posts { get; set; }

		//public virtual ICollection<Friend>? Friends { get; set; }

		public virtual Profile Profile { get; set; }
		[NotMapped]
		[ForeignKey("User1_Id")]
		public virtual ICollection<Friend> SentRequests { get; set; }
		[NotMapped]
		[ForeignKey("User2_Id")]
		public virtual ICollection<Friend> ReceivedRequests { get; set; }
		[NotMapped]
		public IEnumerable<SelectListItem>? AllRoles { get; set; }


	}


}