using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    /// <summary>
    /// Установщик игровых настроек
    /// </summary>
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public GameSettings GameSettings;

        public override void InstallBindings()
        {
            Debug.Log(">>> Game Settings Installer");

            Container.BindInstance(GameSettings);
        }
    }
}