using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace ConexiuniNonProfit.Models
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Numele categoriei este obligatoriu")]
		[StringLength(50, ErrorMessage = "Numele categoriei nu poate avea mai mult de 50 de caractere")]
		public string CategoryName { get; set; }

		public virtual ICollection<Post>? Posts { get; set; }
	}
}
