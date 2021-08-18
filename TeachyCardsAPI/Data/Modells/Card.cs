using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeachyCardsAPI.Data.Modells
{
	public class Card : BaseEntity
	{
		public Guid Id { get; set; }

		public string Question { get; set; }

		public string Answer { get; set; }

		public ICollection<string> Tags { get; set; } = new List<string>();
	}
}
