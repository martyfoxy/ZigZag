using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameEntities
{
    /// <summary>
    /// Тайл
    /// </summary>
    public class Tile : MonoBehaviour, IPoolable<Vector3, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;

        #region IDisposable Implementation
        /// <summary>
        /// Метод для возвращения тайла в пул
        /// </summary>
        public void Dispose()
        {
            _pool.Despawn(this);
        }
        #endregion

        #region IPoolable Implementation
        /// <summary>
        /// Метод вызывается, когда тайл создается из пула
        /// </summary>
        /// <param name="p1">Позиция создания</param>
        /// <param name="p2">Пул тайлов</param>
        public void OnSpawned(Vector3 p1, IMemoryPool p2)
        {
            Debug.Log("Tile OnSpawned");

            _pool = p2;
            transform.position = p1;
        }

        /// <summary>
        /// Метода вызывается, когда тайл возвращается в пул
        /// </summary>
        public void OnDespawned()
        {
            Debug.Log("Tile OnDespawned");
        }
        #endregion

        public class Factory : PlaceholderFactory<Vector3, Tile>
        { }
    }
}