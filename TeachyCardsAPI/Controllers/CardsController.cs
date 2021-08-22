using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TeachyCardsAPI.Data;
using TeachyCardsAPI.Data.Dtos;
using TeachyCardsAPI.Data.Modells;

namespace TeachyCardsAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CardsController : ControllerBase
	{
		private readonly ICardsRepository cardRepository;

		public CardsController(ICardsRepository cardsRepository)
		{
			this.cardRepository = cardsRepository;
		}

		[HttpGet]
		public ActionResult<IEnumerable<CardDto>> GetCards(string search = null)
		{
			return Ok(cardRepository.GetCards(search));
		}

		[HttpGet("[action]")]
		public ActionResult<IEnumerable<string>> GetTags()
		{
			var cards = cardRepository.GetCards();
			var tags = cards.SelectMany(c => c.Tags).Distinct();
			return Ok(tags);
		}

		[HttpGet("{id}")]
		public ActionResult<CardDto> GetCard(Guid id)
		{
			var card = cardRepository.GetCard(id);

			if (card == null)
			{
				return NotFound();
			}

			return Ok(card.AsDto());
		}

		[HttpPost]
		public ActionResult<CardDto> CreateCard([FromBody] CreateCardDto card)
		{
			if (ModelState.IsValid)
			{
				var newCard = new Card() { Question = card.Question, Answer = card.Answer, Tags = card.Tags, Created = DateTime.Now };
				cardRepository.CreateCard(newCard);
				return Ok();
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpPut]
		public ActionResult<CardDto> UpdateCard([FromBody] UpdateCardDto card)
		{
			var existingCard = cardRepository.GetCard(card.Id);
			if (ModelState.IsValid && existingCard != null)
			{
				existingCard.Question = card.Question;
				existingCard.Answer = card.Answer;
				existingCard.Tags = card.Tags;

				cardRepository.UpdateCard(existingCard);
				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
