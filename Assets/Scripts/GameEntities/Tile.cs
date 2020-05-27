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
        IMemoryPool _pool;

        #region IDisposable Implementation
        public void Dispose()
        {
            _pool.Despawn(this);
        }
        #endregion

        #region IPoolable Implementation
        public void OnSpawned(Vector3 p1, IMemoryPool p2)
        {
            _pool = p2;
            transform.position = p1;
        }

        public void OnDespawned()
        {
            
        }
        #endregion


        /*[Inject]
        public void Construct()
        {
            Debug.Log("Tile Construct");
        }

        void Reset(Vector3 position)
        {
            transform.position = position;
        }

        public class Pool : MonoMemoryPool<Vector3, Tile>
        {
            protected override void Reinitialize(Vector3 position, Tile item)
            {
                item.Reset(position);
            }
        }*/

        public class Factory : PlaceholderFactory<Vector3, Tile>
        { }
    }
}