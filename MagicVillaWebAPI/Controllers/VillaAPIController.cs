using MagicVillaWebAPI.Data;
using MagicVillaWebAPI.Logging;
using MagicVillaWebAPI.Model;
using MagicVillaWebAPI.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaWebAPI.Controllers
{
	[Route("api/VillaAPI")] //[("api/controller)] ni lekhna milcha
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		private readonly ApplicationDbContext _db;    //DI ko   kaam garne

		public VillaAPIController(ApplicationDbContext db)
        {
			
			_db = db;
		}


        [HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public ActionResult<IEnumerable<Villadto>> GetVillas() //actionresult ley kun page dekhaune dinch like 200,404
		{
			
			return Ok(_db.Villas.ToList()); //ok bhanya good request
			
		}
		[HttpGet("{id:int}",Name = "GetVilla" )]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public ActionResult <Villadto> GetVilla(int id)//return type of actio result is <villadto>
		{
			
			if(id == 0)
			{
				return BadRequest();
			}
			var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
			if(villa == null)
			{
				return NotFound();
			}
			return Ok(villa);

		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public ActionResult<Villadto> CreateVilla([FromBody] Villadto villadto)
		{
			//if(!ModelState.IsValid)
			//{
			//	return BadRequest(ModelState);    //custom validation ,Modelstate in this case is Villadto
			//}
			if (_db.Villas.FirstOrDefault(x => x.Name.ToLower() == villadto.Name.ToLower()) != null) //making custom validation and adding to model state
			{
				ModelState.AddModelError("customerror", "name already exist");
				return BadRequest(ModelState);
			}

			if (villadto == null)
			{
				return BadRequest(villadto);
			}
			if (villadto.Id > 0)
			{
				return StatusCode(StatusCodes.Status400BadRequest);
			}

			Villa model = new()
			{
				Amenity = villadto.Amenity,
				Details = villadto.Details,
				Id = villadto.Id,
				ImageURL = villadto.ImageURL,
				Name = villadto.Name,
				Occupancy = villadto.Occupancy,
				Rate = villadto.Rate,


			};
			_db.Villas.Add(model);
			_db.SaveChanges();

			return CreatedAtRoute("GetVilla", new { id = villadto.Id }, villadto); // gives the location where it is created 


		}
		//public ActionResult<Villa> CreateVilla([FromBody] Villa villad)
		//{
		//	//if(!ModelState.IsValid)
		//	//{
		//	//	return BadRequest(ModelState);    //custom validation ,Modelstate in this case is Villadto
		//	//}
		//	if (_db.Villas.FirstOrDefault(x => x.Name.ToLower() == villad.Name.ToLower()) != null) //making custom validation and adding to model state
		//	{
		//		ModelState.AddModelError("customerror", "name already exist");
		//		return BadRequest(ModelState);
		//	}

		//	if (villad== null)
		//	{
		//		return BadRequest(villad);
		//	}
		//	if (villad.Id > 0)
		//	{
		//		return StatusCode(StatusCodes.Status400BadRequest);
		//	}

		//	Villa model = new()
		//	{
		//		Amenity = villad.Amenity,
		//		Details = villad.Details,
		//		Id = villad.Id,
		//		ImageURL = villad.ImageURL,
		//		Name = villad.Name,
		//		Occupancy = villad.Occupancy,
		//		Rate = villad.Rate,


		//	};
		//	_db.Villas.Add(model);
		//	_db.SaveChanges();

		//	return CreatedAtRoute("GetVilla", new { id = villad.Id }, villad); // gives the location where it is created 


		//}







		[HttpDelete("{id:int}", Name = "DeleteVilla")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public IActionResult DeleteVilla (int id) //Iactionresult use garda we dont need return type
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = _db.Villas.FirstOrDefault(x => x.Id == id);
			if (villa == null)
			{
				return BadRequest();
			}
			_db.Villas.Remove(villa);
			_db.SaveChanges();
			return NoContent();

		}

		[HttpPut("{id:int}", Name = "UpdateVilla")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public IActionResult UpdateVilla(int id, [FromBody] Villadto villadto)
		{
			if (villadto == null || villadto.Id != id)
			{
				return BadRequest();
			}
			//var villa = VillaStore.VillaList.FirstOrDefault(x => x.Id == id);
			//villa.Name = villadto.Name;
			//villa.Sqft = villadto.Sqft;
			//villa.Occupancy = villadto.Occupancy;

			Villa model = new()
			{
				Amenity = villadto.Amenity,
				Details = villadto.Details,
				Id = villadto.Id,
				ImageURL = villadto.ImageURL,
				Name = villadto.Name,
				Occupancy = villadto.Occupancy,
				Rate = villadto.Rate,


			};
			_db.Villas.Update(model);
			_db.SaveChanges();

			return NoContent();

		}
	}
}
