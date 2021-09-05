using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
		private readonly IConfiguration configuration;

		public CardsController(ICardsRepository cardsRepository, IConfiguration configuration)
		{
			this.cardRepository = cardsRepository;
			this.configuration = configuration;
		}

		[HttpGet]
		public ActionResult<IEnumerable<CardDto>> GetCards(string search = null, string tagSearch = null)
		{
			return Ok(cardRepository.GetCards(search, tagSearch));
		}

		[HttpGet("[action]")]
		public ActionResult<IEnumerable<string>> GetTags()
		{
			var cards = cardRepository.GetCards();
			var tags = cards.SelectMany(c => c.Tags).Distinct();
			return Ok(tags.OrderBy(t => t));
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
				return Ok(newCard);
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[HttpPost("[action]")]
		public ActionResult<CardDto> UploadFiles(ICollection<IFormFile> files)
		{
			//Filter to only accept images
			files = files.Where(f => f.ContentType.StartsWith("image")).ToList();

			long size = files.Sum(f => f.Length);
			var filePath = GetOrCreateUploadFolder();

			List<Guid> imageIds = new List<Guid>();
			foreach (var formFile in files)
			{
				if (formFile.Length > 0)
				{
					var imageId = Guid.NewGuid();
					using (var stream = System.IO.File.Create(Path.Combine(filePath.FullName, imageId + "_" + formFile.FileName)))
					{
						formFile.CopyTo(stream);
						imageIds.Add(imageId);
					}
				}
			}

			return Ok(imageIds);
		}

		[HttpGet("[action]")]
		public ActionResult DownloadFile([FromQuery] Guid imageId)
		{
			var filePath = GetOrCreateUploadFolder();

			var files = Directory.GetFiles(filePath.FullName, imageId + "*");

			if (files.Length > 1)
			{
				return BadRequest("Multiple Files with requested ID");
			}
			else
			{
				var file = files.SingleOrDefault();
				return PhysicalFile(file, MimeTypes.GetMimeType(file));
			}
		}

		[HttpPut]
		public ActionResult<CardDto> UpdateCard([FromBody] UpdateCardDto card)
		{
			var existingCard = cardRepository.GetCard(card.Id);
			if (ModelState.IsValid && existingCard != null)
			{
				// Delete images here if decided to do so

				existingCard.Question = card.Question;
				existingCard.Answer = card.Answer;
				existingCard.Tags = card.Tags;
				existingCard.ImageIds = card.ImageIds;

				cardRepository.UpdateCard(existingCard);

				return Ok(existingCard);
			}
			else
			{
				return BadRequest();
			}
		}

		private DirectoryInfo GetOrCreateUploadFolder()
		{
			return Directory.CreateDirectory(configuration["UploadFolder"]);
		}
	}
}
