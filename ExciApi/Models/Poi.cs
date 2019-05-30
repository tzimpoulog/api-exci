using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExciApi.Models
{
    [Table(name: "POI")]
    public class Poi
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }


        public double Longitude { get; set; }

        public double Latitude { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(2)]
        ///<summary>
        /// new RegionInfo( "US" ).TwoLetterISORegionName
        /// </summary>
        public string Country { get; set; }

        [StringLength(250)]
        public string ImageUrl { get; set; }

        [StringLength(250)]
        public string InfoUrl { get; set; }

        [StringLength(250)]
        public string PriceList { get; set; }

        [StringLength(250)]
        public string OpenHours { get; set; }
    }
}