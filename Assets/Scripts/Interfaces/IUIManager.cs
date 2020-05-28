using Assets.Scripts.Signals;

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// Интерфейс всех UI менеджеров
    /// </summary>
    public interface IUIManager
    {
        /// <summary>
        /// Обработчик сигнала изменения состояния игры
        /// </summary>
        /// <param name="signal">Сигнал</param>
        void OnGameStateChanged(GameStateChangedSignal signal);
    }
}
