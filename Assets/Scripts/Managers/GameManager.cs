using Assets.Scripts.Factories;
using Assets.Scripts.GameEntities;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Pools;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// Игровой менеджер
    /// </summary>
    public class GameManager : IGameManager, IInitializable
    {
        readonly BlockFactory _blockFactory;


        public GameManager(BlockFactory blockFactory)
        {
            _blockFactory = blockFactory;
        }

        public void Initialize()
        {
            Debug.Log(">>> Game Manager Init");

            Test();
        }

        public void Test()
        {
            _blockFactory.CreateBlock(new Vector3(), 5, 3);
        }
    }
}