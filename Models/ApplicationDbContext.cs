using Microsoft.EntityFrameworkCore;

namespace HallOfFame.Models;

/// <summary>
/// Контекст базы данных
/// </summary>
public sealed class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Конструктор контекста базы данных
    /// </summary>
    /// <param name="options">Параметры контекста</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.Migrate();
    }

    /// <summary>
    /// Сотрудники
    /// </summary>
    public DbSet<Person> Persons { get; set; }

    /// <summary>
    /// Навыки
    /// </summary>
    public DbSet<Skill> Skills { get; set; }
}