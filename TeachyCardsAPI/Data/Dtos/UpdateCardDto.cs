using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeachyCardsAPI.Data.Dtos
{
	public record UpdateCardDto
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public string Question { get; set; }

		[Required]
		public string Answer { get; set; }

		public ICollection<string> Tags { get; set; }
	}
}
