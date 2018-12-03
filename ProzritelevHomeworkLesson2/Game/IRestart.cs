using System.Drawing;
interface IRestart
{
    /// <summary>
    /// Перезапуск объекта после столкновения
    /// </summary>
    /// <param name="pos">Объект Point - стартовые координаты</param></param>
    /// <returns></returns>
    void Restart(Point pos);
    /// <summary>
    /// Перезапуск объекта после столкновения с новой скоростью
    /// </summary>
    /// <param name="pos">Объект Point - стартовые координаты</param>
    /// <param name="dir">Объект Point - корость</param>
    /// <returns></returns>
    void Restart(Point pos, Point dir);
    /// <summary>
    /// Перезапуск объекта после столкновения с новой скоростью и размером
    /// </summary>   
    /// <param name="pos">Объект Point - стартовые координаты</param>
    /// <param name="dir">Объект Point - корость</param>
    /// <param name="size">Объект Size - размер</param>
    void Restart(Point pos, Point dir, Size size);
}