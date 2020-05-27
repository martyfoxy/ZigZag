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

            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();

            
            Container.Bind<TilesPool>().AsSingle();
            Container.BindFactory<Vector3, Tile, Tile.Factory>().FromMonoPoolableMemoryPool(x => x.WithInitialSize(100).FromComponentInNewPrefab(tilePrefab).UnderTransformGroup("Tiles"));

            Container.Bind<BlockFactory>().AsSingle();
        }
    }
}