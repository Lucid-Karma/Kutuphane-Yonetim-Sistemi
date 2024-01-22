internal class Book
{
    public string bookName;
    public string writer;
    public int ISBN = 0;
    public int copyCount = 0;
    public int lendedCopyCount = 0;    
    public DateTime lastLoanDate;

    public Book(string bookName, string writer, int copyCount) 
    {
        this.bookName = bookName;
        this.writer = writer;
        this.copyCount = copyCount;
    }
}

internal class Library
{
    private List<Book> books = new List<Book>();
    private int _ISBN = 0;

    public void AddBook()
    {
        Console.Write("\nBaşlık: ");
        string _title = Console.ReadLine();
        Console.Write("Yazar: ");
        string _writer = Console.ReadLine();
        Console.Write("Kopya Sayısı: ");
        int _copyCount;
        if (int.TryParse(Console.ReadLine(), out _copyCount))
        {
            Book book = new Book(_title, _writer, _copyCount);
            book.ISBN = _ISBN;
            books.Add(book);
            _ISBN ++;
        }
        else
        {
            Console.WriteLine("\nGeçersiz kopya sayısı. Lütfen sayısal bir değer girin.");
        }
    }

    public void ViewAllBooks()
    {
        Console.WriteLine("\nKütüphanedeki bütün kitaplar:");

        foreach (var item in books)
        {
            Console.WriteLine($"Başlık: {item.bookName}, Yazar: {item.writer}, ISBN: {item.ISBN}, Kopya Sayısı: {item.copyCount}, Ödünç Alınan Kopya Sayısı: {item.lendedCopyCount}");
        }
    }

    public void SearchBook()
    {
        Console.WriteLine("\nLütfen kitap başlığı ya da yazar girin.");
        string hint = Console.ReadLine();

        foreach (var item in books)
        {
            if(item.bookName.Contains(hint) || item.writer.Contains(hint))
            {
                Console.WriteLine($"Başlık: {item.bookName}, Yazar: {item.writer}, ISBN: {item.ISBN}, Kopya Sayısı: {item.copyCount}, Ödünç Alınan Kopya Sayısı: {item.lendedCopyCount}");
            }
        }
    }

    public void BorrowBook()
    {
        Console.WriteLine("\nLütfen ödünç alacağınız kitabın başlığını girin.");
        string title = Console.ReadLine();

        foreach (var item in books)
        {
            if (item.bookName == title)
            {
                if (item.copyCount > 0)
                {
                    item.lastLoanDate = DateTime.Now.AddDays(14); 
                    item.copyCount--;
                    item.lendedCopyCount++;
                    Console.WriteLine($"{1} adet \"{title}\" kitabı ödünç alındı. Son iade tarihi: {item.lastLoanDate.ToShortDateString()}");
                }
                else
                {
                    Console.WriteLine("\nYeterli kopya bulunmamaktadır.");
                }
                return;
            }
        }
        Console.WriteLine("\nKitap bulunamadı.");
    }

    public void ReturnBook()
    {
        Console.WriteLine("\nLütfen iade edeceğiniz kitabın başlığını girin.");
        string title = Console.ReadLine();

        foreach (var item in books)
        {
            if (item.bookName == title)
            {
                if (item.lendedCopyCount > 0)
                {
                    item.lendedCopyCount--;
                    item.copyCount++;
                    Console.WriteLine($"{1} adet \"{title}\" kitabı iade edildi.");
                }
                else
                {
                    Console.WriteLine("\nHatalı iade. Toplam kopya sayısı aşıldı.");
                }
                return;
            }
        }
        Console.WriteLine("\nKitap bulunamadı.");
    }

    public void ViewExpiredBooks()
    {
        Console.WriteLine("\nSüresi Geçmiş Kitaplar:");
        foreach (var item in books)
        {
            if (item.lendedCopyCount > 0 && DateTime.Now > item.lastLoanDate)
            {
                Console.WriteLine($"Başlık: {item.bookName}, Yazar: {item.writer}, Son İade Tarihi: {item.lastLoanDate.ToShortDateString()}");
            }
        }
    }
}

internal class ManageLibrary
{
    static void Main()
    {
        Library library = new Library();

        while (true)
        {
            Console.WriteLine("\nKütüphane Yönetim Sistemi");
            Console.WriteLine("1. Kitap Ekle");
            Console.WriteLine("2. Tüm Kitapları Görüntüle");
            Console.WriteLine("3. Kitap Ara");
            Console.WriteLine("4. Kitap Ödünç Al");
            Console.WriteLine("5. Kitap İade Et");
            Console.WriteLine("6. Süresi Geçmiş Kitapları Görüntüle");
            Console.WriteLine("7. Çıkış");

            Console.Write("\nYapmak istediğiniz işlemi seçin (1-7): ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    library.AddBook();
                    break;
                case "2":
                    library.ViewAllBooks(); 
                    break;
                case "3":
                    library.SearchBook();
                    break;
                case "4":
                    library.BorrowBook();
                    break;
                case "5":
                    library.ReturnBook();
                    break;
                case "6":
                    library.ViewExpiredBooks();
                    break;
                case "7":
                    Console.WriteLine("Programdan çıkılıyor...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Geçersiz bir seçim yaptınız. Lütfen tekrar deneyin.");
                    break;
            }
        }
        
    }
    
}