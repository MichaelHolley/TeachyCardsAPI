using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeachyCardsAPI.Data.Dtos
{
	public record CreateCardDto
	{
		[Required]
		public string Question { get; set; }

		[Required]
		public string Answer { get; set; }

		public ICollection<string> Tags { get; set; }
	}
}
