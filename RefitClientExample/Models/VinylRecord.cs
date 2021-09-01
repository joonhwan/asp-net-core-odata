using System.ComponentModel.DataAnnotations;

namespace RefitClientExample.Models
{
    public class VinylRecord
    {
        public int VinylRecordId { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public string CatalogNumber { get; set; }

        public int? Year { get; set; }

        public int PressingDetailId { get; set; }

        public virtual Person Person { get; set; }

        public int PersonId { get; set; } 
    }
}
