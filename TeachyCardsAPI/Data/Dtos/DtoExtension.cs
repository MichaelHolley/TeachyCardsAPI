using System.Collections.Generic;
using System.Linq;
using TeachyCardsAPI.Data.Modells;

namespace TeachyCardsAPI.Data.Dtos
{
	public static class DtoExtension
	{
		public static CardDto AsDto(this Card card)
		{
			return new CardDto()
			{
				Id = card.Id,
				Question = card.Question,
				Answer = card.Answer,
				Created = card.Created
			};
		}

		public static ICollection<CardDto> AsDto(this ICollection<Card> card)
		{
			return card.Select(c => c.AsDto()).ToList();
		}
	}
}
