using System;
using System.Collections.Generic;
using TeachyCardsAPI.Data.Modells;

namespace TeachyCardsAPI.Data
{
	public interface ICardsRepository
	{
		public ICollection<Card> GetCards(string search = null, string tagSearch = null);
		public Card GetCard(Guid id);
		public void CreateCard(Card card);
		public void UpdateCard(Card card);
		public void DeleteCard(Guid id);
	}
}
