using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using TeachyCardsAPI.Data.Modells;

namespace TeachyCardsAPI.Data
{
	public class MongoDbCardsRepository : ICardsRepository
	{
		private const string databaseName = "TeachyCards";
		private const string collectionName = "TeachyCards";

		private readonly IMongoCollection<Card> cardsCollections;
		private FilterDefinitionBuilder<Card> filterBuilder = Builders<Card>.Filter;

		public MongoDbCardsRepository(IMongoClient client)
		{
			IMongoDatabase database = client.GetDatabase(databaseName);
			cardsCollections = database.GetCollection<Card>(collectionName);
		}

		public void CreateCard(Card card)
		{
			card.Modified = null;
			cardsCollections.InsertOne(card);
		}

		public void DeleteCard(Guid id)
		{
			var filter = filterBuilder.Eq(c => c.Id, id);
			cardsCollections.DeleteOne(filter);
		}

		public Card GetCard(Guid id)
		{
			var filter = filterBuilder.Eq(c => c.Id, id);
			return cardsCollections.Find(filter).SingleOrDefault();
		}

		public ICollection<Card> GetCards(string search = null, string tagSearch = null)
		{
			if (!string.IsNullOrEmpty(search))
			{
				if (!string.IsNullOrEmpty(tagSearch))
				{
					var filter = filterBuilder.Where(c => (c.Answer.ToLower().Contains(search.ToLower()) || c.Question.ToLower().Contains(search.ToLower())) && c.Tags.Contains(tagSearch));
					return cardsCollections.Find(filter).ToList();
				}
				else
				{
					var filter = filterBuilder.Where(c => c.Answer.ToLower().Contains(search.ToLower()) || c.Question.ToLower().Contains(search.ToLower()));
					return cardsCollections.Find(filter).ToList();
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(tagSearch))
				{
					var filter = filterBuilder.Where(c => c.Tags.Contains(tagSearch));
					return cardsCollections.Find(filter).ToList();
				}
				else
				{
					return cardsCollections.Find(new BsonDocument()).ToList();
				}
			}
		}

		public void UpdateCard(Card card)
		{
			card.Modified = DateTime.Now;

			var filter = filterBuilder.Eq(existingCard => existingCard.Id, card.Id);
			cardsCollections.ReplaceOne(filter, card);
		}
	}
}
