using Assets.Scripts.Models.Enums;
using Assets.Scripts.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameEntities
{
    public class Player : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        private SignalBus _signalBus;
        private IMemoryPool _pool;
        private LayerMask _layerMask;

        private DirectionEnum _direction;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _layerMask = LayerMask.GetMask("Tile");
            _direction = DirectionEnum.Forward;
        }

        private void Update()
        {
            if(!ChechGround())
                _signalBus.Fire(new PlayerDiedSignal());

            //TODO быдлокод
            if (Input.GetMouseButtonDown(0))
                _direction = _direction == DirectionEnum.Forward ? DirectionEnum.Right : DirectionEnum.Forward;

            switch(_direction)
            {
                case DirectionEnum.Forward:
                    {
                        transform.Translate(new Vector3(0,0, 0.05f), Space.World);
                        break;
                    }
                case DirectionEnum.Right:
                    {
                        transform.Translate(new Vector3(0.05f, 0, 0), Space.World);
                        break;
                    }
            }
        }

        public bool ChechGround()
        {
            var hit = Physics.Raycast(transform.position, Vector3.down, 1f, _layerMask);
            return hit;
        }

        #region IDispose implementation
        public void Dispose()
        {
            _pool.Despawn(this);
        }
        #endregion

        #region IPoolable implementation
        public void OnSpawned(IMemoryPool p2)
        {
            Debug.Log("Player spawned");

            _pool = p2;
            transform.position = new Vector3(0, 0.5f, 0);
        }

        public void OnDespawned()
        {
            Debug.Log("Player despawned");
        }
        #endregion

        public class Factory : PlaceholderFactory<Player>
        { }
    }
}