using System.ComponentModel.DataAnnotations;

namespace MagicVillaWebAPI.Model.Dto
{
	public class Villadto
	{
		public int Id { get; set; }
		[Required]//yo sab api controller ho so explicitly uta bhannu pardaina
		[MaxLength(100)]//if apicontroller na bhako bhae explicitly hamro controller maa bhannu partho MODELSTATE use garera, custom validation use garnu paryo bhane pani modelstate use garne
		public string Name { get; set; }

		public string Details { get; set; }
		[Required]
		public double Rate { get; set; }
		public int Occupancy { get; set; }
		public int Sqft { get; set; }
		public string ImageURL { get; set; }
		public string Amenity { get; set; }
	}
}
