using Assets.Scripts.Factories;
using Assets.Scripts.GameEntities;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// Менеджер уровня
    /// </summary>
    public class LevelManager : IInitializable
    {
        #region DI
        private readonly BlockFactory _blockFactory;
        private readonly AsyncProcessor _asyncProcessor;
        private readonly GameSettings _gameSettings;
        #endregion

        //Локальные переменные
        private int _pathWidth;
        private List<Block> _blocks = new List<Block>();

        public LevelManager(BlockFactory blockFactory, AsyncProcessor asyncProcessor, GameSettings gameSettings)
        {
            _blockFactory = blockFactory;
            _asyncProcessor = asyncProcessor;
            _gameSettings = gameSettings;
        }

        public void Initialize()
        {
            SetLineWidth(_gameSettings.Difficulty);
        }

        /// <summary>
        /// Задать ширину дорожки в зависимости от сложности
        /// </summary>
        /// <param name="difficulty">Сложность</param>
        public void SetLineWidth(DifficultyEnum difficulty)
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

        public void GenerateLevel(int count)
        {
            _blocks = new List<Block>();

            //Создаем платформу
            Block spawnPoint = CreateSpawnPlatform();
            _blocks.Add(spawnPoint);
            Paint(spawnPoint);

            //Запоминаем последний тайл блока, где нужно создавать следующий блок
            Tile lastTile = spawnPoint.GetLast();

            for (int i = 0; i < count; i++)
            {
                Block path = SpawnRandomPath(lastTile);
                _blocks.Add(path);
                Paint(path);
                //_asyncProcessor.StartCoroutine(RemoveBlockAfterSeconds(block, 2));

                lastTile = path.GetLast();
            }
        }

        /// <summary>
        /// Сброс. Удаляет все блоки
        /// </summary>
        public void Reset()
        {
            _blocks.ForEach(block => _blockFactory.RemoveBlock(block));
        }

        #region test
        private void Paint(Block block)
        {
            block.GetFirst().GetComponent<MeshRenderer>().material.color = Color.green;
            block.GetLast().GetComponent<MeshRenderer>().material.color = Color.red;
        }

        IEnumerator SpawnWithRemoving()
        {
            //Создаем платформу
            var platform = CreateSpawnPlatform();
            _blocks.Add(platform);
            Paint(platform);

            //Запоминаем последний тайл блока, где нужно создавать следующий блок
            Tile lastTile = platform.GetLast();

            for (int i = 0; i < 1; i++)
            {
                var block = SpawnRandomPath(lastTile);
                _blocks.Add(block);
                Paint(block);
                //Удаляем через 3 секунды
                _asyncProcessor.StartCoroutine(RemoveBlockAfterSeconds(block, 3));

                lastTile = block.GetLast();

                yield return new WaitForSeconds(0.5f);
            }
        }

        IEnumerator RemoveBlockAfterSeconds(Block block, float time)
        {
            yield return new WaitForSeconds(time);

            _blockFactory.RemoveBlock(block);
        }
        #endregion

        /// <summary>
        /// Создать платформу 3х3 из тайлов
        /// </summary>
        /// <returns>Блок</returns>
        private Block CreateSpawnPlatform()
        {
            return _blockFactory.Create(3, 3, new Vector3());
        }

        /// <summary>
        /// Создать случайную дорожку с определенного тайла
        /// </summary>
        /// <param name="previousTile">Последний тайл предыдущего блока</param>
        /// <returns>Блок</returns>
        private Block SpawnRandomPath(Tile previousTile)
        {
            //Случайная длина блока
            int length = Random.Range(1, _gameSettings.MaximumTilesPerPath);

            //Случаное направление
            if (Random.value > 0.5f)
                return _blockFactory.Create(_pathWidth, length, previousTile.transform.position + Vector3.forward, DirectionEnum.Forward);
            else
                return _blockFactory.Create(_pathWidth, length, previousTile.transform.position + Vector3.right, DirectionEnum.Right);
        }
    }
}
