namespace HallOfFame.Models;


/// <summary>
/// Модель Сотрудника
/// </summary>
public class Person
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Отображаемое имя
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Список навыков
    /// </summary>
    public List<Skill> Skills { get; set; }
}