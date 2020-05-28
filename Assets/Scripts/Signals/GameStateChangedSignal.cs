using Assets.Scripts.Models.Enums;

namespace Assets.Scripts.Signals
{
    /// <summary>
    /// Сигнал изменения режима игры
    /// </summary>
    public class GameStateChangedSignal
    {
        public GameStateEnum NewState;
    }
}
