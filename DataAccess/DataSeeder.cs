namespace MyBookApi.DataAccess;

using MyBookApi.DataAccess;
using MyBookApi.Models;
using Microsoft.EntityFrameworkCore;

public static class DataSeeder
{
    public static async Task SeedAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Если уже есть данные — выходим
        if (await ctx.Authors.AnyAsync()) return;

        // 1) Создаем авторов
        var author1 = new Author { Name = "Александр Пушкин" };
        var author2 = new Author { Name = "Лев Толстой" };
        ctx.Authors.AddRange(author1, author2);
        await ctx.SaveChangesAsync();

        // 2) Создаем книги
        var book1 = new Book { Title = "Евгений Онегин", Year = 1833, AuthorId = author1.Id };
        var book2 = new Book { Title = "Война и мир",     Year = 1869, AuthorId = author2.Id };
        ctx.Books.AddRange(book1, book2);
        await ctx.SaveChangesAsync();

        // 3) Создаем издания
        var editions = new[]
        {
            new Edition { BookId = book1.Id, Format = "Hardcover", ReleaseDate = new DateTime(1833,10,1) },
            new Edition { BookId = book1.Id, Format = "eBook",     ReleaseDate = new DateTime(2000,1,1)  },
            new Edition { BookId = book2.Id, Format = "Paperback", ReleaseDate = new DateTime(1870,5,1)  },
        };
        ctx.Editions.AddRange(editions);
        await ctx.SaveChangesAsync();
    }
}
