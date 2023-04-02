using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HallOfFame.Models;


/// <summary>
/// Модель навыков сотрудника
/// </summary>
public class Skill
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [JsonIgnore]
    public long Id { get; set; }

    /// <summary>
    /// Название навыка
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Уровень навыка
    /// </summary>
    [Range(1, 10, ErrorMessage = "Level дожен быть от 1 до 10")]
    public byte Level { get; set; }

    /// <summary>
    /// Индектификатор (для связи таблицы навыков с таблицей сотрудников)
    /// </summary>
    [JsonIgnore]
    public long PersonId { get; set; }

    /// <summary>
    /// Сотрудник
    /// </summary>
    [JsonIgnore]
    public Person Person { get; set; }
}