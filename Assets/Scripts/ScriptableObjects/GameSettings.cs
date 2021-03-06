﻿using Assets.Scripts.Models.Enums;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    /// <summary>
    /// Скриптовый объект с настройками игры
    /// </summary>
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField]
        private DifficultyEnum _difficulty;
        
        [SerializeField]
        private float _playerSpeed;

        [SerializeField]
        private int _maximumTilesPerPathCount;

        [SerializeField]
        private bool _crystalSpawnParameter;

        /// <summary>
        /// Сложность игры
        /// </summary>
        public DifficultyEnum Difficulty => _difficulty;

        /// <summary>
        /// Скорость игрока
        /// </summary>
        public float PlayerSpeed => _playerSpeed;

        /// <summary>
        /// Максимальная длина одной дорожки
        /// </summary>
        public int MaximumTilesPerPath => _maximumTilesPerPathCount;

        //TODO: Параметр конфигурирующий спаун кристаллов
        public bool CrystalSpawnParameter => _crystalSpawnParameter;
    }
}
