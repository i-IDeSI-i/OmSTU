using System;

struct SNF
{
    public string Surname;
    public string Name;
    public string Fathername;
    public SNF(string surname, string name, string fathername) { Surname = surname; Name = name; Fathername = fathername; }
}
class Person
{
    public int Id { get; set; }
    public SNF Fio { get; set; }
    public int Born { get; set; }
    public string Adress { get; set; }
    private Person(int id, SNF fio, int born, string adress)
    {
        Id = id;
        Fio = fio;
        Born = born;
        Adress = adress;
    }

    public static Person? Create(int id, SNF fio, int born, string adress)
    {
        if (born.GetType() != typeof(int)) return null;
        return new Person(id, fio, born, adress);
    }
}

class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public SNF Author { get; set; }
    public int Year { get; set; }
    public int Count { get; set; }
    
    public Book(int id, string name, SNF author, int year, int count)
    {
        Id = id;
        Name = name;
        Author = author;
        Year = year;
        Count = count;
    }
}

class BookMove
{
    public Person ReaderInfo { get; set; }
    public Book BookInfo { get; set; }
    public DateTime giveDate { get; set; }
    public DateTime hangDate { get; set; }
    
    private BookMove(Person ri, Book b, DateTime gd, DateTime hd)
    {
        ReaderInfo = ri;
        BookInfo = b;
        giveDate = gd;
        hangDate = hd;
    }

    public static BookMove? Create(Person ri, Book b, DateTime gd, DateTime hd)
    {
        if (hd < gd) return null;
        return new BookMove(ri, b, gd, hd);
    }
}

class Program
{
    public static void Main()
    {
        DateTime emptyDate = new DateTime(0000, 0, 0);
        
        Console.WriteLine("inp readers count");
        int n = Convert.ToInt32(Console.ReadLine());
        BookMove[] array = new BookMove[n];   

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("inp person id");
            int pid = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("inp person surname");
            string psname = Console.ReadLine();
            Console.WriteLine("inp person name");
            string pname = Console.ReadLine();
            Console.WriteLine("inp person fathername");
            string pfname = Console.ReadLine();
            Console.WriteLine("inp person born");
            int pborn = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("inp person adress");
            string padress = Console.ReadLine();

            SNF fio = new SNF(psname, pname, pfname);
            Person person = Person.Create(pid, fio, pborn, padress);

            Console.WriteLine("inp book id");
            int bid = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("inp book name");
            string bname = Console.ReadLine();
            Console.WriteLine("inp book author surname");
            string basname = Console.ReadLine();
            Console.WriteLine("inp book author name");
            string baname = Console.ReadLine();
            Console.WriteLine("inp book author fathername");
            string bafname = Console.ReadLine();
            Console.WriteLine("inp book year");
            int byear = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("inp book count");
            int bcount = Convert.ToInt32(Console.ReadLine());

            SNF bauthor = new SNF(basname, baname, bafname);
            Book book = new Book(bid, bname, bauthor, byear, bcount);

            Console.WriteLine("inp give date (yyyy-mm-dd)");
            DateTime giveDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("inp return date (yyyy-mm-dd) (not return is 0000.00.00)");
            DateTime hangDate = DateTime.Parse(Console.ReadLine());

            BookMove move = BookMove.Create(person, book, giveDate, hangDate);
            array[i] = move;
        }    
        foreach (BookMove mv in array) {
            if (DateTime.Compare(mv.hangDate, emptyDate) != 0) { Console.WriteLine("this book isnt return"); }
            if (DateTime.Compare(mv.hangDate, emptyDate) == 0 && DateTime.Compare(mv.giveDate, emptyDate) == 0) { Console.WriteLine("book never taken"); }
            Console.WriteLine(mv.BookInfo.Name);
            Console.WriteLine(mv.BookInfo.Author);
            Console.WriteLine(mv.ReaderInfo.Fio);
            Console.WriteLine(mv.giveDate);
            Console.WriteLine(mv.hangDate);
            Console.WriteLine(mv.BookInfo.Author);
        }
        while (true)
        {
            Console.WriteLine("1 - take book, 2 - return book, 0 - exit");
            int cmd = Convert.ToInt32(Console.ReadLine());
            
            if (cmd == 0) break;
            
            if (cmd == 1)
            {
                Console.WriteLine("inp book id");
                int bid = Convert.ToInt32(Console.ReadLine());
                
                bool taken = false;
                for (int i = 0; i < n; i++)
                {
                    if (array[i].BookInfo.Id == bid && DateTime.Compare(array[i].hangDate, emptyDate) != 0)
                    {
                        Console.WriteLine("inp give date (yyyy-mm-dd)");
                        DateTime gd = DateTime.Parse(Console.ReadLine());
                        
                        array[i].giveDate = gd;
                        array[i].hangDate = emptyDate;
                        Console.WriteLine("book taken");
                        taken = true;
                        break;
                    }
                }
                if (!taken) Console.WriteLine("book not found or already taken");
            }
            
            if (cmd == 2)
            {
                Console.WriteLine("inp book id");
                int bid = Convert.ToInt32(Console.ReadLine());
                
                bool returned = false;
                for (int i = 0; i < n; i++)
                {
                    if (array[i].BookInfo.Id == bid && DateTime.Compare(array[i].hangDate, emptyDate) == 0)
                    {
                        Console.WriteLine("inp return date (yyyy-mm-dd)");
                        DateTime hd = DateTime.Parse(Console.ReadLine());
                        
                        array[i].hangDate = hd;
                        Console.WriteLine("book returned");
                        returned = true;
                        break;
                    }
                }
                if (!returned) Console.WriteLine("book not found or not taken");
            }
        }
    }
}