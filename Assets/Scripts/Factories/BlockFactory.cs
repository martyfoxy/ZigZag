using Assets.Scripts.GameEntities;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Factories
{
    /// <summary>
    /// Фабрика блоков
    /// </summary>
    public class BlockFactory : IFactory<int, int, Vector3, DirectionEnum, Block>
    {
        readonly TilesPool _tilesPool;
        readonly Block.Factory _factory;
        
        public BlockFactory(TilesPool tilesPool, Block.Factory factory)
        {
            _tilesPool = tilesPool;
            _factory = factory;
        }

        /// <summary>
        /// Создать новый блок по параметрам
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Длина</param>
        /// <param name="pos">Позиция создания</param>
        /// <param name="direction">Направление</param>
        /// <returns>Созданный блок</returns>
        public Block Create(int width, int height, Vector3 pos, DirectionEnum direction = DirectionEnum.Forward)
        {
            var tiles = CreateBlock(width, height, pos, direction);

            return _factory.Create(tiles);
        }

        /// <summary>
        /// Удалить блок, состоящий из тайлов
        /// </summary>
        /// <param name="tiles">Список тайлов для удаления</param>
        public void RemoveBlock(Block block)
        {
            block.Tiles.ForEach(tile => _tilesPool.RemoveTile(tile));
        }

        /// <summary>
        /// Создать блок тайлов с заданным направлением
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="length">Длина</param>
        /// <param name="pos">Начальная позиция</param>
        /// <param name="direction">Направление</param>
        /// <returns>Список тайлов блока</returns>
        private List<Tile> CreateBlock(int width, int length, Vector3 pos, DirectionEnum direction)
        {
            List<Tile> block = new List<Tile>();

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    switch(direction)
                    {
                        case DirectionEnum.Forward:
                            {
                                Vector3 temp = pos;
                                temp.x += j;
                                temp.z += i;

                                Tile newTile = _tilesPool.AddTile(temp);
                                block.Add(newTile);
                                break;
                            }
                            case DirectionEnum.Right:
                            {
                                Vector3 temp = pos;
                                temp.z += j;
                                temp.x += i;

                                Tile newTile = _tilesPool.AddTile(temp);
                                block.Add(newTile);
                                break;
                            }
                    }
                }
            }

            return block;
        }
    }
}