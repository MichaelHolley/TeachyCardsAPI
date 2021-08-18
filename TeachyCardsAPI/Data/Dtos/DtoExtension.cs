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
				Tags = card.Tags,
				Created = card.Created
			};
		}

		public static ICollection<CardDto> AsDto(this ICollection<Card> cards)
		{
			return cards.Select(c => c.AsDto()).ToList();
		}
	}
}
