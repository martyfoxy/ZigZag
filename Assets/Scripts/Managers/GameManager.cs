using Assets.Scripts.Factories;
using Assets.Scripts.GameEntities;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.Pools;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Managers
{
    /// <summary>
    /// Игровой менеджер
    /// </summary>
    public class GameManager : IGameManager, IInitializable, ITickable
    {
        /// <summary>
        /// Состояние игры
        /// </summary>
        public GameStateEnum GameState {
            get
            {
                return _gameState;
            }
            private set
            {
                _gameState = value;
                _signalBus.Fire(new GameStateChangedSignal() { NewState = value });
            }
        }

        #region DI
        readonly AsyncProcessor _asyncProcessor;
        readonly GameSettings _gameSettings;
        readonly LevelManager _levelManager;
        private readonly PlayerPool _playerPool;
        private readonly SignalBus _signalBus;
        #endregion

        //Локальные переменные
        private GameStateEnum _gameState;

        public GameManager(GameSettings gameSettings, AsyncProcessor asyncProcessor, LevelManager levelManager, PlayerPool playerPool, SignalBus signalBus)
        {
            _asyncProcessor = asyncProcessor;
            _gameSettings = gameSettings;
            _levelManager = levelManager;
            _playerPool = playerPool;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            Debug.Log(">>> Game Manager Init");

            _gameState = GameStateEnum.Startup;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch(GameState)
                {
                    case GameStateEnum.Startup:
                        {
                            StartGame();
                            break;
                        }
                    case GameStateEnum.GameOver:
                        {
                            RestartGame();
                            break;
                        }
                }
            }
                
        }

        #region IGameManager implementation
        public void StartGame()
        {
            _levelManager.GenerateLevel(10);

            _playerPool.SpawnPlayer();

            GameState = GameStateEnum.Playing;
        }

        public void RestartGame()
        {
            _levelManager.Reset();
            _levelManager.GenerateLevel(10);

            _playerPool.SpawnPlayer();

            GameState = GameStateEnum.Playing;
        }

        public void OnPlayerDiedSignal(PlayerDiedSignal signal)
        {
            _playerPool.DespawnPlayer();

            GameState = GameStateEnum.GameOver;
        }
        #endregion


    }
}