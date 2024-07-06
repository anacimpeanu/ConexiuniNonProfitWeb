using ConexiuniNonProfit.Models;

namespace ConexiuniNonProfit.Models
{
	public class Actiuni
	{
		public int ActiuniId { get; set; }
		public string ActiuniName { get; set; }
		public string ActiuniAbbreviation { get; set; }
		public string ActiuniDescription { get; set; }

		public virtual ICollection<Post>? Posts { get; set; }
	}
}
