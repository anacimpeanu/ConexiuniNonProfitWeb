using System.ComponentModel.DataAnnotations;
using ConexiuniNonProfit.Models;
using System.Text.RegularExpressions;
using ConexiuniNonProfit.Models;

namespace ConexiuniNonProfit.Models
{
	public class Post
	{
		[Key]
		public int PostId { get; set; }

		[Required(ErrorMessage = "Continutul postarii este obligatoriu")]
		public string PostContent { get; set; }
		public DateTime PostDate { get; set; }

		public byte[]? Image { get; set; }
		public string? UserId { get; set; }

		public virtual ApplicationUser? User { get; set; }


		public int? CategoryId { get; set; }
		public virtual Category? Category { get; set; }

		public int? ActiuniId { get; set; }
		public virtual Actiuni? Actiuni { get; set; }
		public virtual ICollection<Comment>? Comments { get; set; }

	}
}
