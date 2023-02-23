namespace IMDB
{
    public class Person
    { 
        public string Name { get; set; }
        public int Id { get; set; }
        public Person(int id , string name)
        {
            Id = id;
            Name = name;
        }

    }
   
}
