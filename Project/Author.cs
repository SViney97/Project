using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class Author: IComparable
    {
        public string AuthorName { get; set; }
        public List<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }

        public Author(string authorname)
        {
            AuthorName = authorname;
        }
        public override string ToString()
        {
            return $"{AuthorName}";
        }
        public int CompareTo(object obj)
        {
            //get name for thing beside current activity
            //this sorts the data beside it.
            //for example if the data was C,A,B - it will sort itself to form A,B,C.
            Author objectBeside = obj as Author;
            return this.AuthorName.CompareTo(objectBeside.AuthorName);
        }

    }
    public class Book
    {
        public string BookName { get; set; }
        public DateTime Published { get; set; }

        public decimal Sales { get; set; }

        public Book()
        {

        }
        public Book(string bookname, DateTime published, decimal sales)
        {
            BookName = bookname;
            Published = published;
            Sales = sales;
        }

        /*public int CompareTo(object obj)
        {     
            Book objectBeside = obj as Book;
            return this.Published.Year.CompareTo(objectBeside.Published.Year);
        }*/
        public override string ToString()
        {
            return $"{BookName}";
        }
    }
}
