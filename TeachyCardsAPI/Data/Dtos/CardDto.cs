using System;
using System.Collections.Generic;

namespace TeachyCardsAPI.Data.Dtos
{
	public record CardDto
	{
		public Guid Id { get; set; }

		public string Question { get; set; }

		public string Answer { get; set; }

		public ICollection<string> Tags { get; set; }

		public ICollection<Guid> ImageIds { get; set; }

		public DateTime Created { get; set; }
	}
}
