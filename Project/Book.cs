using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{

    public enum BookType { Fiction, Play}

    public class Book
    {
        public string BookName { get; set; }
        public DateTime Published { get; set; }

        public int AgeInYears
        {
            get
            {
                int ageInYears = DateTime.Now.Year - Published.Year;

                //account for date later in year
                if (Published.DayOfYear > DateTime.Now.DayOfYear)
                    ageInYears--;

                return ageInYears;
            }
        }


        public BookType Genre { get; set; }

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

        public override string ToString()
        {
            return $"{BookName}";
        }
    }
}
