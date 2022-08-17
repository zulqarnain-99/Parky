using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkyAPI.Models.Dtos
{
    public class TrailUpsertDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Distance { get; set; }
        public enum DifficultType { Easy, Moderate, Difficult, Expert }
        public DifficultType Difficulty { get; set; }

        [Required]
        public int NationalParkId { get; set; }
    }
}
