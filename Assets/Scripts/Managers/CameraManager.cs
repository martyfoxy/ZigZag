using Assets.Scripts.GameEntities;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.Pools;
using Assets.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Managers
{
    public class CameraManager : ICameraManager, IInitializable, ITickable
    {
        private Camera _camera;
        private Vector3 _startOffset;

        bool allowMovement = false;

        #region DI
        private PlayerPool _pool;
        #endregion

        Player _player;

        public CameraManager(PlayerPool pool)
        {
            _pool = pool;
        }

        public void Initialize()
        {
            Debug.Log(">>> Camera Manager Init");

            _camera = Camera.main;

            _startOffset = _camera.transform.position;
        }

        public void Tick()
        {
            if (_player == null || !allowMovement)
                return;

            _camera.transform.position = _player.transform.position + _startOffset;
        }

        public void OnGameStateChangedSignal(GameStateChangedSignal signal)
        {
            switch(signal.NewState)
            {
                case GameStateEnum.Startup:
                case GameStateEnum.GameOver:
                    {
                        _player = null;
                        allowMovement = false;
                        break;
                    }
                case GameStateEnum.Playing:
                    {
                        _player = _pool.Player;
                        allowMovement = true;
                        break;
                    }
            }
        }
    }
}
