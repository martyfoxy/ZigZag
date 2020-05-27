using Assets.Scripts.Factories;
using Assets.Scripts.GameEntities;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.Pools;
using Assets.Scripts.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
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
        readonly AsyncProcessor _asyncProcessor;
        readonly GameSettings _gameSettings;

        private int _pathWidth;

        public GameManager(BlockFactory blockFactory, GameSettings gameSettings, AsyncProcessor asyncProcessor)
        {
            _blockFactory = blockFactory;
            _asyncProcessor = asyncProcessor;
            _gameSettings = gameSettings;
        }

        public void Initialize()
        {
            Debug.Log(">>> Game Manager Init");

            SetDifficulty(_gameSettings.Difficulty);

            //_asyncProcessor.StartCoroutine(asdf());
        }

        /// <summary>
        /// Установить ширину дорожек в зависимости от сложности
        /// </summary>
        /// <param name="difficulty">Сложность</param>
        public void SetDifficulty(DifficultyEnum difficulty)
        {
            switch (difficulty)
            {
                case DifficultyEnum.Easy:
                    _pathWidth = 3;
                    break;
                case DifficultyEnum.Medium:
                    _pathWidth = 2;
                    break;
                case DifficultyEnum.Hard:
                    _pathWidth = 1;
                    break;
            }
        }

        private void GenerateLevel()
        {
            //Создаем платформу
            var platform = CreateSpawnPlatform();
            Paint(platform);

            //Запоминаем последний тайл блока, где нужно создавать следующий блок
            Tile lastTile = platform[platform.Count - 1];

            for (int i = 0; i < 100; i++)
            {
                //Четное - вперед, нечетное - вправо
                bool forward = i % 2 == 0;

                var startPos = forward ? lastTile.transform.position + Vector3.forward : lastTile.transform.position + Vector3.right;

                var block = SpawnRandomBlock(startPos, forward);
                Paint(block);

                lastTile = block[block.Count - 1];
            }
        }

        IEnumerator asdf()
        {
            //Создаем платформу
            var platform = CreateSpawnPlatform();
            Paint(platform);

            //Запоминаем последний тайл блока, где нужно создавать следующий блок
            Tile lastTile = platform[platform.Count - 1];

            for (int i = 0; i < 100; i++)
            {
                //Четное - вперед, нечетное - вправо
                bool forward = i % 2 == 0;

                var startPos = forward ? lastTile.transform.position + Vector3.forward : lastTile.transform.position + Vector3.right;

                var block = SpawnRandomBlock(startPos, forward);
                //Paint(block);
                //Удаляем через 3 секунды
                //_asyncProcessor.StartCoroutine(RemoveBlockAfterSeconds(block, 3));

                lastTile = block[block.Count - 1];

                yield return new WaitForSeconds(0.5f);
            }
        }

        IEnumerator RemoveBlockAfterSeconds(List<Tile> block, float time)
        {
            yield return new WaitForSeconds(time);

            _blockFactory.RemoveBlock(block);
        }

        
        public void Paint(List<Tile> block)
        {
            block[0].GetComponent<MeshRenderer>().material.color = Color.green;
            block[block.Count - 1].GetComponent<MeshRenderer>().material.color = Color.red;
        }


        /// <summary>
        /// Создать случайную дорожку с заданной позиции и направлением
        /// </summary>
        /// <param name="pos">Позиция создания</param>
        /// <param name="isForward">Направлена вперед?</param>
        /// <returns></returns>
        public List<Tile> SpawnRandomBlock(Vector3 pos, bool isForward)
        {
            if(isForward)
                return _blockFactory.CreateBlock(pos, Random.Range(1, 10), _pathWidth, DirectionEnum.Forward);
            else
                return _blockFactory.CreateBlock(pos, Random.Range(1, 10), _pathWidth, DirectionEnum.Right);
        }

        /// <summary>
        /// Создать платформу 3х3 из тайлов
        /// </summary>
        /// <returns>Тайлы</returns>
        public List<Tile> CreateSpawnPlatform()
        {
            return _blockFactory.CreateBlock(new Vector3(), 3, 3);
        }
    }
}