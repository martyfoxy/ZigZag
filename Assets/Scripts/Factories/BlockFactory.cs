using Assets.Scripts.GameEntities;
using Assets.Scripts.Models.Enums;
using Assets.Scripts.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Factories
{
    /// <summary>
    /// Фабрика блоков
    /// </summary>
    public class BlockFactory
    {
        readonly TilesPool _tilesPool;

        public BlockFactory(TilesPool tilesPool)
        {
            _tilesPool = tilesPool;
        }

        /// <summary>
        /// Создать блок тайлов
        /// </summary>
        /// <param name="pos">Начальная позиция</param>
        /// <param name="length">Длина</param>
        /// <param name="width">Ширина</param>
        /// <returns>Список тайлов блока</returns>
        public List<Tile> CreateBlock(Vector3 pos, int length, int width)
        {
            List<Tile> block = new List<Tile>();

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Vector3 temp = pos;
                    temp.x += j;
                    temp.z += i;

                    Tile newTile = _tilesPool.AddTile(temp);
                    block.Add(newTile);
                }
            }

            return block;
        }

        /// <summary>
        /// Создать блок тайлов с заданным направлением
        /// </summary>
        /// <param name="pos">Начальная позиция</param>
        /// <param name="length">Длина</param>
        /// <param name="width">Ширина</param>
        /// <param name="direction">Направление</param>
        /// <returns>Список тайлов блока</returns>
        public List<Tile> CreateBlock(Vector3 pos, int length, int width, DirectionEnum direction)
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

        /// <summary>
        /// Удалить блок, состоящий из тайлов
        /// </summary>
        /// <param name="tiles">Список тайлов для удаления</param>
        public void RemoveBlock(List<Tile> tiles)
        {
            tiles.ForEach(tile => _tilesPool.RemoveTile(tile));
        }
    }
}