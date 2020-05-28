using Assets.Scripts.Factories;
using Assets.Scripts.GameEntities;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Managers;
using Assets.Scripts.Pools;
using Assets.Scripts.Signals;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    /// <summary>
    /// Установщик сущностей игры
    /// </summary>
    public class GameInstaller : MonoInstaller
    {
        public GameObject playerPrefab;
        public GameObject tilePrefab;

        public override void InstallBindings()
        {
            Debug.Log(">>> Game Installer");

            //Привязываем менеджеры
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraManager>().AsSingle();

            //Декларируем сигналы
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameStateChangedSignal>();
            Container.DeclareSignal<PlayerDiedSignal>();
            Container.BindSignal<GameStateChangedSignal>().ToMethod<IUIManager>(x => x.OnGameStateChanged).FromResolve();
            Container.BindSignal<GameStateChangedSignal>().ToMethod<ICameraManager>(x => x.OnGameStateChangedSignal).FromResolve();
            Container.BindSignal<PlayerDiedSignal>().ToMethod<IGameManager>(x => x.OnPlayerDiedSignal).FromResolve();

            //Привязываем пулы
            Container.Bind<TilesPool>().AsSingle();
            Container.Bind<PlayerPool>().AsSingle();
            Container.BindFactory<Vector3, Tile, Tile.Factory>().FromMonoPoolableMemoryPool(x => x.WithInitialSize(25).FromComponentInNewPrefab(tilePrefab).UnderTransformGroup("Tiles"));
            Container.BindFactory<Player, Player.Factory>().FromMonoPoolableMemoryPool(x => x.WithInitialSize(1).FromComponentInNewPrefab(playerPrefab));

            //Привязываем фабрики
            Container.Bind<BlockFactory>().AsSingle();
            Container.BindFactory<List<Tile>, Block, Block.Factory>();

            //Создаем monobehaviour для выполнения корутин
            Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}