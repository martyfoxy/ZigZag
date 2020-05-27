using Assets.Scripts.Factories;
using Assets.Scripts.GameEntities;
using Assets.Scripts.Managers;
using Assets.Scripts.Pools;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    /// <summary>
    /// Установщик сущностей игры
    /// </summary>
    public class GameInstaller : MonoInstaller
    {
        public GameObject tilePrefab;

        public override void InstallBindings()
        {
            Debug.Log(">>> Game Installer");

            //Привязываем менеджеры
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();

            //Привязываем пулы
            Container.Bind<TilesPool>().AsSingle();
            Container.BindFactory<Vector3, Tile, Tile.Factory>().FromMonoPoolableMemoryPool(x => x.WithInitialSize(25).FromComponentInNewPrefab(tilePrefab).UnderTransformGroup("Tiles"));

            //Привязываем фабрики
            Container.Bind<BlockFactory>().AsSingle();

            //Создаем monobehaviour для выполнения корутин
            Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}