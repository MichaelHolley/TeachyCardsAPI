using System;

namespace TeachyCardsAPI.Data.Dtos
{
	public record CardDto
	{
		public Guid Id { get; set; }

		public string Question { get; set; }

		public string Answer { get; set; }

		public DateTime Created { get; set; }
	}
}
