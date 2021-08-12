using System;
using System.ComponentModel.DataAnnotations;

namespace TeachyCardsAPI.Data.Modells
{
	public class Card : BaseEntity
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		public string Question { get; set; }

		[Required]
		public string Answer { get; set; }
	}
}
