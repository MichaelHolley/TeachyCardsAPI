using System;
using System.ComponentModel.DataAnnotations;

namespace TeachyCardsAPI.Data.Dtos
{
	public record PostCardDto
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		public string Question { get; set; }

		[Required]
		public string Answer { get; set; }
	}
}
