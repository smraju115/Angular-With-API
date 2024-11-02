using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Device_API.Models
{
    public enum DeviceType { Mobile=1, Tab}
    public class Device
    {
        public int DeviceId { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = default!;
        [Required, EnumDataType(typeof(DeviceType))]
        public DeviceType DeviceType { get; set; }
        [Required, Column(TypeName ="date")]
        public DateTime ReleaseDate { get; set; }
        [Required, Column(TypeName ="money")]
        public decimal Price { get; set; }
        [Required, StringLength (50)]
        public string Picture { get; set; }= default!;
        public bool InStock { get; set; }
        //Navigation
        public virtual ICollection<Spec> Specs { get; set; } = [];
   

    }
    public class Spec
    {
        public int SpecId { get; set; }
        [Required, StringLength(50)]
        public string SpecName { get; set; } = default!;
        [Required, StringLength(50)]
        public string SpecValue { get; set; } = default!;
        //Fk
        [Required, ForeignKey("Device")]
        public int DeviceId { get; set; }
        //Navigation
        public virtual Device? Devices { get; set; }
    }
    public class DeviceDbContext(DbContextOptions<DeviceDbContext> options) : DbContext(options)
    {
        //public DeviceDbContext(DbContextOptions<DeviceDbContext> options):base(options) {}
        public DbSet<Device> Devices { get; set;}
        public DbSet<Spec> Specs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>().HasData(
                
                new Device { DeviceId=1, Name="Samsung A52 5G", DeviceType=DeviceType.Mobile, ReleaseDate=new DateTime(2021, 1, 1), Price=405000, InStock=true, Picture="1.jpg"}
                );
            modelBuilder.Entity<Spec>().HasData(

                new Spec { SpecId = 1, SpecName = "Ram", SpecValue = "6GB", DeviceId = 1 },
                new Spec { SpecId = 2, SpecName = "Storage", SpecValue = "128GB", DeviceId = 1 }
                );
        }
    }
}
