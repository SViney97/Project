using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;

using Newtonsoft.Json;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
        
    public partial class MainWindow : Window
    {
        //connects to the Database
        OrderTableContainer DB = new OrderTableContainer();
        //
        List<Author> Authors = new List<Author>();
        List<Author> FilteredAuthors = new List<Author>();

        List<Book> Books = new List<Book>();
        List<Book> FilteredBooks = new List<Book>();


        decimal totalcost = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Book_Shop_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //Authors
                //makes author objects
                Author Will_shake = new Author() { AuthorName = "William Shakespeare" };
                Author JK_Rowling = new Author() { AuthorName = "J.K Rowling" };

                //Authors Books
                //makes book objects
                Book romeo_juliet = new Book()
                {
                    Genre = BookType.Play,
                    BookName = "romeo and juliet",
                    Published = new DateTime(1859, 04, 04),
                    Sales = 20m,
                    BookIMG = "https://programming-project-images.s3-eu-west-1.amazonaws.com/RomeoandJuliet.jpeg"
                };
                Book King_Lear = new Book()
                {
                    Genre = BookType.Play,
                    BookName = "King Lear",
                    Published = new DateTime(1869, 03, 04),
                    Sales = 15m,
                    BookIMG = "https://programming-project-images.s3-eu-west-1.amazonaws.com/Kinglear.jpeg"
                };


                Book The_Goblet_of_Fire = new Book()
                {
                    Genre = BookType.Fiction,
                    BookName = "The Goblet of Fire",
                    Published = new DateTime(2000, 07, 08),
                    Sales = 12.99m,
                    BookIMG = "https://programming-project-images.s3-eu-west-1.amazonaws.com/gobletoffire.jpeg"
                };
                Book The_Philospher_Stone = new Book()
                {
                    Genre = BookType.Fiction,
                    BookName = "The Philospher's Stone",
                    Published = new DateTime(1997, 06, 26),
                    Sales = 14.99m,
                    BookIMG = "https://programming-project-images.s3-eu-west-1.amazonaws.com/philosphers+stone.jpeg"
                };
                Book Chamber_of_Secrets = new Book()
                {
                    Genre = BookType.Fiction,
                    BookName = "Chamber of Secrets",
                    Published = new DateTime(1998, 07, 02),
                    Sales = 12.99m,
                    BookIMG = "https://programming-project-images.s3-eu-west-1.amazonaws.com/ChamberofSecrets.jpg"
                };
                Book Prisoner_of_Azkaban = new Book()
                {
                    Genre = BookType.Fiction,
                    BookName = "Prisoner of Azkaban",
                    Published = new DateTime(1999, 07, 08),
                    Sales = 15.99m,
                    BookIMG = "https://programming-project-images.s3-eu-west-1.amazonaws.com/Prisonerofaskaban.jpg"
                };

                //Adding book to Author
                Will_shake.Books.Add(romeo_juliet);
                Will_shake.Books.Add(King_Lear);

                JK_Rowling.Books.Add(The_Goblet_of_Fire);
                JK_Rowling.Books.Add(The_Philospher_Stone);
                JK_Rowling.Books.Add(Chamber_of_Secrets);
                JK_Rowling.Books.Add(Prisoner_of_Azkaban);


                Authors.Add(Will_shake);
                Authors.Add(JK_Rowling);

                //calls the method
                RefreshList();

                CBXBookGenre.ItemsSource = new string[] { "All", "Fiction", "Play" };   
            }
            catch(Exception Oe)
            {
                MessageBox.Show("A error has happened when makeing an object: ", Oe.Message, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        } // end of Book_Shop_Loaded
        private void RefreshList()
        {
            //this sets the listboxs to null and then pupoulates the
            //the listboxes with the lists
            //and sorts the authors and books
            LBXAuthors.ItemsSource = null;
            LBXAuthors.ItemsSource = Authors;

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
            
        }//end of LBXAuthors_SelectionChanged
        private void LBXBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*when a book is slected from the listbox books
            the slected books information is showed in a texblock
            and the book image is shown
             */
            Book B_Select = LBXBooks.SelectedItem as Book;

            
            if (B_Select != null)
            {
                TBXInformation.Text = $"Published: {B_Select.Published.ToString("dd/MM/yyyy")}" +
                    $"\nSales: {B_Select.Sales.ToString("c")} \nYears since Published: {B_Select.AgeInYears} years";
                IMGBookCover.Source = new BitmapImage(new Uri(B_Select.BookIMG, UriKind.RelativeOrAbsolute));
            }
        }  //end of LBXBooks_SelectionChanged
        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {/*selects book from the Shopping cart
           it adds the selected book from the books list to the shopping cart list
           and increments the total cost of the shopping cart and 
           updates the listbox from the filtered list
             */
            Book selectedBook = LBXBooks.SelectedItem as Book;

            if (selectedBook != null)
            {
                Books.Remove(selectedBook);
                FilteredBooks.Add(selectedBook);

                totalcost = totalcost + selectedBook.Sales;

                TBLKCost.Text = totalcost.ToString("c");
                LBXShoppingCart.SelectedItem = FilteredBooks;
                RefreshList();

               
            }
            else
            {
                //display a message for when the user has not selected a book 
                MessageBox.Show($"You have not a book to add to the Shopping Cart", "INFORMATION",
                    MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }

        } //end of BTNAdd_Click   
        private void BTNRemove_Click(object sender, RoutedEventArgs e)
        {
            /*slects book from the Shopping cart
             it remvoes the selected book from the list 
             and decreses the total cost of the shopping cart and 
             updates the listbox from the filstedbooks list
             */
            Book selectedBook = LBXShoppingCart.SelectedItem as Book;

            if (selectedBook != null)
            {
                
                FilteredBooks.Remove(selectedBook);

                totalcost = totalcost - selectedBook.Sales;

                TBLKCost.Text = totalcost.ToString("c");
                LBXShoppingCart.ItemsSource = FilteredBooks;
                RefreshList();
            }
            else
            {
                //display a message for when the user has not selected a book 
                MessageBox.Show($"You have not a book to remove from the Shopping Cart", "INFORMATION",
                    MessageBoxButton.OKCancel, MessageBoxImage.Information);
            }
        } // end of BTNRemove_Click
        private void CBXBookGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /* when the combo box is slected and a item is seleted
                it clears the filtered authors and then depending on the selected
                book genre it outputs the authors with the bookType the same as
                the selected genre
             
             
             */
            string selection = CBXBookGenre.SelectedItem as string;

            FilteredAuthors.Clear();
            
                if (CBXBookGenre.SelectedItem != null)
                {
                    if (selection == "All")
                    {
                        LBXAuthors.ItemsSource = null;
                        LBXAuthors.ItemsSource = Authors;
                    LBXBooks.ItemsSource =null;


                    }
                    else if (selection == "Play")
                    {
                        LBXBooks.ItemsSource = null;
                        LBXBooks.ItemsSource = Books.Where(b => b.Genre == BookType.Play);

                        //authors with plays

                        #region FilterAuthors
                        FilteredAuthors.Clear();
                        bool authorHasPlays = false;
                        foreach (Author author in Authors)
                        {
                            foreach (Book book in author.Books)
                            {
                                if (book.Genre == BookType.Play)
                                    authorHasPlays = true;
                            }
                            if (authorHasPlays == true)
                            {
                                FilteredAuthors.Add(author);
                                break;
                            }
                        }
                    LBXAuthors.ItemsSource = null;
                    LBXAuthors.ItemsSource = FilteredAuthors;

                    #endregion FilterAuthors

                }
                    else if (selection == "Fiction")
                    {
                        LBXBooks.ItemsSource = null;
                        LBXBooks.ItemsSource = Books.Where(b => b.Genre == BookType.Fiction);

                        #region FilterAuthors
                        FilteredAuthors.Clear();
                        bool authorHasPlays = false;
                        foreach (Author author in Authors)
                        {
                            foreach (Book book in author.Books)
                            {
                                if (book.Genre == BookType.Fiction)
                                    authorHasPlays = true;
                            }
                            if (authorHasPlays == true)
                            {
                                FilteredAuthors.Add(author);
                                break;
                            }
                        }
                    LBXAuthors.ItemsSource = null;
                    LBXAuthors.ItemsSource = FilteredAuthors;

                    #endregion FilterAuthors
                }
                }
            
            
        }//end of CBXBookGenre_SelectionChanged
        private void BTNSave_Click(object sender, RoutedEventArgs e)
        {
            //for each book in the filtered book list, it will add
            // to the orders database and then saves o the order database
            try
            {

                foreach (Book book in FilteredBooks)
                {
                    Order O = new Order()
                    {
                        BookName = book.BookName,
                        Price = book.Sales
                    };
                    DB.Orders.Add(O);
                }

                DB.SaveChanges();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "A error has happened", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            //this takes the list Filteredbooks and indents the json string and then 
            //calls the streamwriter and outputs the JSON data into the directory Project\bin\Debug 
            string JSONData = JsonConvert.SerializeObject(FilteredBooks, Formatting.Indented);
            using (StreamWriter SW = new StreamWriter("BookData.json"))
            {
                SW.Write(JSONData);
                SW.Close();
            }

            // generates a random number between 7 and 15
            Random r = new Random(); 
            int deliverytime = r.Next(7, 15); 
            
            //display a message for when the user has not selected a book 
            MessageBox.Show($"Thank you for your order\n your order will be with you in {deliverytime} days!", "Order Confirmed",
            MessageBoxButton.OKCancel, MessageBoxImage.None);
            
            /* clears the filteredbokks list and sets the total cost to zero
               and calls the refresh method    
             */
            FilteredBooks.Clear();
            totalcost = 0;
            TBLKCost.Text = totalcost.ToString("c");
            RefreshList();

        }//end of BTNSave_Click
        private void TabOrderTable_Click(object sender, RoutedEventArgs e)
        {
            /*
             this uses the Database and from the database it gets
             the id, book name and the price and then displays it in 
             datagrid view
             */
            try
            {
                using (DB)
                {
                    var query = from order in DB.Orders
                                select new
                                {
                                    order.Id,
                                    order.BookName,
                                    order.Price
                                };
                    OrdersDgVDisplay.ItemsSource = query.ToList();
                }
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message,"A error has happened",  MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            

        }//end of TabOrderTable_Click
    }//end of MainWindow : Window
} // end of namespace
