using Library.Base;

namespace Library
{
    public class Book : EntityBase
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int NumberOfCopies { get; set; }
    }
}
