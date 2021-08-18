using System;

namespace TeachyCardsAPI.Data.Modells
{
	public class BaseEntity
	{
		public DateTime Created { get; init; }

		public DateTime? Modified { get; set; }
	}
}