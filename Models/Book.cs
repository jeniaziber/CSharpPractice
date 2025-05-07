namespace MyBookApi.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;

    // Убираем Publisher, так как он больше не нужен

    public int Year { get; set; }

    // Связь с изданиями (одна книга может иметь много изданий)
    public List<Edition> Editions { get; set; } = new();
}