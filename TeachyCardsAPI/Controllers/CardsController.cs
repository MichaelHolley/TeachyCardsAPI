using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TeachyCardsAPI.Data;
using TeachyCardsAPI.Data.Dtos;
using TeachyCardsAPI.Data.Modells;

namespace TeachyCardsAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CardsController : ControllerBase
	{
		private readonly ICardsRepository repository;

		public CardsController(ICardsRepository repository)
		{
			this.repository = repository;
		}

		[HttpGet]
		public ActionResult<IEnumerable<CardDto>> GetCards(string search = "")
		{
			return Ok(repository.GetCards(search));
		}

		[HttpGet("{id}")]
		public ActionResult<CardDto> GetCard(Guid id)
		{
			var card = repository.GetCard(id);

			if (card == null)
			{
				return NotFound();
			}

			return Ok(card.AsDto());
		}

		[HttpPost]
		public ActionResult<CardDto> PostCard([FromBody] PostCardDto card)
		{
			if (ModelState.IsValid)
			{
				var existingCard = repository.GetCard(card.Id);

				if (existingCard != null)
				{
					existingCard.Question = card.Question;
					existingCard.Answer = card.Answer;

					repository.UpdateCard(existingCard);
				}
				else
				{
					repository.CreateCard(new Card() { Question = card.Question, Answer = card.Answer, Created = DateTime.Now });
				}

				return Ok();
			}
			else
			{
				return BadRequest();
			}
		}
	}
}
