using System;

namespace IMDB.Models.DbModels
{
    public class Persons
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime DOB { get; set; }
        public string Sex { get; set; }
    }
}
