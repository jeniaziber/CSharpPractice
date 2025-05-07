namespace MyBookApi.Models;

public class Edition
{
    public int Id { get; set; }
    public string Format { get; set; } = string.Empty;  // Пример: Hardcover, eBook и т.д.
    public DateTime ReleaseDate { get; set; }

    // Связь с книгой
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
}