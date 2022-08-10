﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkyAPI.Models.Dtos
{
    public class TrailDto
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

        public NationalParkDto  NationalPark { get; set; }
    }
}
