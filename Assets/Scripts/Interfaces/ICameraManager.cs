using Assets.Scripts.Signals;

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// Интерфейсы всех менеджеров камер
    /// </summary>
    public interface ICameraManager
    {
        /// <summary>
        /// Обработчик сигнала изменения состояния игры
        /// </summary>
        /// <param name="signal">Сигнал</param>
        void OnGameStateChangedSignal(GameStateChangedSignal signal);
    }
}
