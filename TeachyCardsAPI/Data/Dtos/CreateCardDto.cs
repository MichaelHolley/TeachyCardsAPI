using Microsoft.AspNetCore.Http;
using System;
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

		public ICollection<string> Tags { get; set; } = new List<string>();

		public ICollection<Guid> ImageIds { get; set; } = new List<Guid>();
	}
}
