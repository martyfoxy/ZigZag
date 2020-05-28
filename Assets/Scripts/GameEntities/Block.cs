using System.Collections.Generic;
using Zenject;

namespace Assets.Scripts.GameEntities
{
    /// <summary>
    /// Блок представляет из себя совокупность тайлов
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Список тайлов
        /// </summary>
        public List<Tile> Tiles => _tiles;
        private List<Tile> _tiles;

        public Block(List<Tile> tiles)
        {
            _tiles = new List<Tile>(tiles);
        }

        /// <summary>
        /// Получить первый тайл блока
        /// </summary>
        /// <returns>Первый тайл</returns>
        public Tile GetFirst()
        {
            return _tiles[0];
        }

        /// <summary>
        /// Получить последний тайл блока
        /// </summary>
        /// <returns>Последний тайл</returns>
        public Tile GetLast()
        {
            return _tiles[_tiles.Count - 1];
        }

        public class Factory: PlaceholderFactory<List<Tile>, Block>
        { }
    }
}
