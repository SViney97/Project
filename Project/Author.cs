using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    
    public class Author: IComparable
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
        public int CompareTo(object obj)
        {
            //get name for thing beside current activity
            //this sorts the data beside it.
            //for example if the data was C,A,B - it will sort itself to form A,B,C.
            Author objectBeside = obj as Author;
            return this.AuthorName.CompareTo(objectBeside.AuthorName);
        }
        public override string ToString()
        {
            return $"{AuthorName}";
        }

    }
}




