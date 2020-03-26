using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Author> Authors = new List<Author>();
        List<Author> FilteredAuthors = new List<Author>();

        List<Book> Books = new List<Book>();
        List<Book> FilteredBooks = new List<Book>();

        List<Book> shopingCart = new List<Book>();

        decimal totalcost = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Book_Shop_Loaded(object sender, RoutedEventArgs e)
        {

            //Authors
            Author Will_shake = new Author() { AuthorName = "William Shakespeare" };
            Author JK_Rowling = new Author() { AuthorName = "J.K Rowling" };

        //Authors Books
            Book romeo_juliet = new Book()
            {
                Genre = BookType.Play,
                BookName = "romeo and juliet",
                Published = new DateTime(1859, 04, 04),
                Sales = 20m
            };
            Book King_Lear = new Book()
            {
                Genre = BookType.Play,
                BookName = "King Lear",
                Published = new DateTime(1869, 03, 04),
                Sales = 15m
            };


            Book The_Goblet_of_Fire = new Book()
            {
                Genre = BookType.Fiction,
                BookName = "The Goblet of Fire",
                Published = new DateTime(2000, 07, 08),
                Sales = 12.99m
            };
            Book The_Philospher_Stone = new Book()
            {
                Genre = BookType.Fiction,
                BookName = "The Philospher's Stone",
                Published = new DateTime(1997, 06, 26),
                Sales = 15.99m
            };
            Book Chamber_of_Secrets = new Book()
            {
                Genre = BookType.Fiction,
                BookName = "Chamber of Secrets",
                Published = new DateTime(1998, 07, 02),
                Sales = 15.99m
            };
            Book Prisoner_of_Azkaban = new Book()
            {
                Genre = BookType.Fiction,
                BookName = "Prisoner of Azkaban",
                Published = new DateTime(1999, 07, 08),
                Sales = 15.99m
            };

            //Book Harry_Potter_and_the_Cursed_Child = new Book()
            //{
            //    Genre = BookType.Play,
            //    BookName = "Harry Potter and the Cursed Child",
            //    Published = new DateTime(2016, 06, 31),
            //    Sales = 20m
            //};
            //JK_Rowling.Books.Add(Harry_Potter_and_the_Cursed_Child);

            //Adding book to Author
            Will_shake.Books.Add(romeo_juliet);
            Will_shake.Books.Add(King_Lear);

            JK_Rowling.Books.Add(The_Goblet_of_Fire);
            JK_Rowling.Books.Add(The_Philospher_Stone);
            JK_Rowling.Books.Add(Chamber_of_Secrets);
            JK_Rowling.Books.Add(Prisoner_of_Azkaban);
            

            Authors.Add(Will_shake);
            Authors.Add(JK_Rowling);

           
            RefreshList();
            CBXBookGenre.ItemsSource = new string[] { "All", "Fiction", "Play" };


        } // end of Book_Shop_Loaded

        private void RefreshList()
        {
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
            //RefreshList();
        } //end of LBXAuthors_SelectionChanged

        private void LBXBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book B_Select = LBXBooks.SelectedItem as Book;

            
            if (B_Select != null)
            {
                TBXInformation.Text = $"Published: {B_Select.Published.ToString("dd/MM/yyyy")}" +
                    $"\nSales: {B_Select.Sales.ToString("c")} \nYears since Published: {B_Select.AgeInYears} years";
            }
        }  //end of LBXBooks_SelectionChanged

        private void BTNAdd_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = LBXBooks.SelectedItem as Book;

            if (selectedBook != null)
            {
                Books.Remove(selectedBook);
                FilteredBooks.Add(selectedBook);

                totalcost = totalcost + selectedBook.Sales;

                TBLKCost.Text = totalcost.ToString("c");
                LBXShoppingCart.ItemsSource = FilteredBooks;
                RefreshList();
            }

        } //end of BTNAdd_Click

        private void BTNRemove_Click(object sender, RoutedEventArgs e)
        {
            Book selectedBook = LBXShoppingCart.SelectedItem as Book;

            if (selectedBook != null)
            {
                //Books.Add(selectedBook);
                FilteredBooks.Remove(selectedBook);

                totalcost = totalcost - selectedBook.Sales;

                TBLKCost.Text = totalcost.ToString("c");
                LBXShoppingCart.ItemsSource = FilteredBooks;
                RefreshList();
            }
        } // end of BTNRemove_Click

        private void CBXBookGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
            
        }
    }//end of MainWindow : Window
} // end of namespace
