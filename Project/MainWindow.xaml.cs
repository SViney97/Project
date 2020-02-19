using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Author> Authors = new List<Author>();
        List<Book> Books = new List<Book>();
        List<Book> FilteredBooks = new List<Book>();
        decimal totalcost = 0;
        //List<Author> filteredAuthors = new List<Author>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Book_Shop_Loaded(object sender, RoutedEventArgs e)
        {
            Author Will_shake = new Author 
            { 
                AuthorName = "William Shakespeare"
            };
            Author JK_Rowling = new Author
            {
                AuthorName = "J.K. Rowling",
                
            };
            Author A3 = new Author
            {
                AuthorName = "Dr. Seuss"
            };
            Author A4 = new Author
            {
                AuthorName = "Stephen King"
            };
            Author A5 = new Author
            {
                AuthorName = "Roald Dahl"
            };
            Author A6 = new Author
            {
                AuthorName = "J.R.R. Tolkien"
            };
            Author A7 = new Author
            {
                AuthorName = "Lewis Carroll"
            };
            Author A8 = new Author
            {
                AuthorName = "EL James"
            };

            Authors.Add(Will_shake);
            Authors.Add(JK_Rowling);
            /*Authors.Add(A3);
            Authors.Add(A4);
            Authors.Add(A5);
            Authors.Add(A6);
            Authors.Add(A7);
            Authors.Add(A8);*/

            //add some books
            Book JK_Rowling_Book1 = new Book() 
            { BookName = "The Goblet of Fire",
                Published =new DateTime(2000,07,08),
                Sales = 12.99m

            };
            Book JK_Rowling_Book2 = new Book()
            {
                BookName = "The Philospher's Stone",
                Published = new DateTime(1997, 06, 26),
                Sales = 15.99m

            };
            Book JK_Rowling_Book3 = new Book()
            {
                BookName = "Chamber of Secrets",
                Published = new DateTime(1998,07,02),
                Sales = 15.99m

            };
            Book JK_Rowling_Book4 = new Book()
            {
                BookName = "Prisoner of Azkaban",
                Published = new DateTime(1999, 07, 08),
                Sales = 15.99m

            };

            JK_Rowling.Books.Add(JK_Rowling_Book1);
            JK_Rowling.Books.Add(JK_Rowling_Book2);
            JK_Rowling.Books.Add(JK_Rowling_Book3);
            JK_Rowling.Books.Add(JK_Rowling_Book4);
           

            RefreshList();
            LBXAuthors.ItemsSource = Authors;

        }
        private void RefreshList()
        {
            LBXAuthors.ItemsSource = null;
            LBXAuthors.ItemsSource = Authors;

            LBXBooks.ItemsSource = null;
            LBXBooks.ItemsSource = Books;

            LBXShoppingCart.ItemsSource = null;
            LBXShoppingCart.ItemsSource = FilteredBooks;

            Authors.Sort();
            Books.Sort();

        }//end of RefreshList

        private void LBXAuthors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Author Selected = LBXAuthors.SelectedItem as Author;

            
            if (Selected != null)
            {             
               LBXBooks.ItemsSource = Selected.Books.OrderBy(b => b.Published);

      
            }
            
        }

        private void LBXBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book B_Select = LBXBooks.SelectedItem as Book;
            if (B_Select != null)
            {
                TBXInformation.Text = $"Published:{B_Select.Published} \t sales{B_Select.Sales}";
            }
        }

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = LBXBooks.SelectedItem as Book;

            if (selectedBook != null)
            {
                Books.Remove(selectedBook);
                FilteredBooks.Add(selectedBook);

                totalcost = totalcost + selectedBook.Sales;

                LblCost.Text = totalcost.ToString("c");
                LBXShoppingCart.ItemsSource = FilteredBooks;
                RefreshList();
            }
            
        }

        private void BTNRemove_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = LBXShoppingCart.SelectedItem as Book;

            if(selectedBook !=null)
            {
                //Books.Add(selectedBook);
                FilteredBooks.Remove(selectedBook);

                totalcost = totalcost - selectedBook.Sales;

                LblCost.Text = totalcost.ToString("c");
                LBXShoppingCart.ItemsSource = FilteredBooks;
                RefreshList();
            }
        }
    }
}
