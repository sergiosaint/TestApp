using System.Collections.Generic;

namespace TestEFInserts.Entities
{
    public class Son
    {
        public int SonId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
        public IEnumerable<Toy> Toys { get; set; }
    }
}
