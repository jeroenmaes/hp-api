namespace HP.Api.Dto
{
    public class Character
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<string> alternate_names { get; set; }
        public string species { get; set; }
        public string gender { get; set; }
        public string house { get; set; }
        public string dateOfBirth { get; set; }
        public int? yearOfBirth { get; set; }
        public bool wizard { get; set; }
        public string ancestry { get; set; }
        public string eyeColour { get; set; }
        public string hairColour { get; set; }
        public Wand wand { get; set; }
        public string patronus { get; set; }
        public bool hogwartsStudent { get; set; }
        public bool hogwartsStaff { get; set; }
        public string actor { get; set; }
        public List<object> alternate_actors { get; set; }
        public bool alive { get; set; }
        public string image { get; set; }
    }

    public class Wand
    {
        public string wood { get; set; }
        public string core { get; set; }
        public double? length { get; set; }
    }
}
