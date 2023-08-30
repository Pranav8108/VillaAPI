using MagicVillaWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaWebAPI.Data
{
	public class ApplicationDbContext : DbContext // this is necessary while working with EF
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options) 
        {
                
        }
     public DbSet<Villa> Villas { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) //how data are being sent by the user
		{
			modelBuilder.Entity<Villa>().HasData(
				new Villa()
				{
					Id = 1,
					Name = "Name",
					Details = "Name bhane naam ho",
					ImageURL ="",
					Occupancy = 5,
					Rate = 5,
					Sqrt = 5,
					Amenity="",
					CreatedDate = DateTime.Now,

				});
				
		}


	}


}
