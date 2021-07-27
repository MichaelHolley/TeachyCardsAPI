using System;
using System.ComponentModel.DataAnnotations;

namespace TeachyCardsAPI.Data
{
	public class Card : BaseEntity
	{
		[Key]
		public Guid Id { get; set; }
		public string Question { get; set; }
		public string Answer { get; set; }
	}
}
