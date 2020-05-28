using Assets.Scripts.GameEntities;
using System;
using UnityEngine;

namespace Assets.Scripts.Pools
{
    /// <summary>
    /// Пул для игрока
    /// </summary>
    public class PlayerPool
    {
        readonly Player.Factory _playerFactory;

        public Player Player => _player;
        private Player _player;

        public PlayerPool(Player.Factory pool)
        {
            _playerFactory = pool;
        }

        public void SpawnPlayer()
        {
            if (_player != null)
                throw new Exception("Нельзя создать еще одного игрока");

            var player = _playerFactory.Create();
            player.transform.position = new Vector3(0, 0.5f, 0);
            _player = player;
        }

        public void DespawnPlayer()
        {
            if (_player == null)
                throw new Exception("Игрока не существует, чтобы его удалить");

            _player.Dispose();
            _player = null;
        }
    }
}
