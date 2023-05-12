﻿using Assignment3.Models.Response;
using System.Collections.Generic;

namespace Assignment3.Models.DbModels
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public int ProducerId { get; set; }
        public string CoverImageUrl { get; set; }
    }
}
