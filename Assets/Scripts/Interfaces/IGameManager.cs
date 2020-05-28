using Assets.Scripts.Models.Enums;
using Assets.Scripts.Signals;

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// Интерфейс всех игровых менеджеров
    /// </summary>
    public interface IGameManager
    {
        /// <summary>
        /// Начать игру
        /// </summary>
        void StartGame();

        /// <summary>
        /// Перезапустить игру
        /// </summary>
        void RestartGame();

        /// <summary>
        /// Обработчик сигнала смерти игрока
        /// </summary>
        /// <param name="signal">Сигнал</param>
        void OnPlayerDiedSignal(PlayerDiedSignal signal);
    }
}
