using Assets.Scripts.GameEntities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Pools
{
    /// <summary>
    /// Пул тайлов
    /// </summary>
    public class TilesPool
    {
        readonly Tile.Factory _tileFactory;
        readonly List<Tile> _tiles = new List<Tile>();

        public TilesPool(Tile.Factory pool)
        {
            _tileFactory = pool;
        }

        /// <summary>
        /// Получить скрытый тайл или создать новый
        /// </summary>
        /// <param name="pos">Позиция тайла</param>
        public Tile AddTile(Vector3 pos)
        {
            var tile = _tileFactory.Create(pos);

            _tiles.Add(tile);

            return tile;
        }

        /// <summary>
        /// Удалить тайл из пула
        /// </summary>
        public void RemoveTile()
        {
            if(_tiles.Any())
            {
                var tile = _tiles[0];
                tile.Dispose();
                _tiles.Remove(tile);
            }
        }

        /// <summary>
        /// Удалить тайл из пула
        /// </summary>
        /// <param name="tile">Тайл</param>
        public void RemoveTile(Tile tile)
        {
            var index = _tiles.FindIndex(x => x == tile);
            if(index != -1)
            {
                _tiles[index].Dispose();
                _tiles.RemoveAt(index);
            }
        }
    }
}
