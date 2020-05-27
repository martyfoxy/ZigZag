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

        public void CreateBlock(Vector3 pos, int length, int width)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Vector3 temp = pos;
                    temp.x += j;
                    temp.z += i;

                    _tilesPool.AddTile(temp);
                }
            }
        }
    }
}