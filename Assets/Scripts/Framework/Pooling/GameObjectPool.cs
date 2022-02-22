using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Framework.Pooling
{
    public class GameObjectPool : ObjectPool
    {
        // 需要缓存的对象
        private float m_LiftTime;

        public GameObjectPool(string poolName, Transform transform, float lifetime)
        {
            PoolName = poolName;
            m_Parent = transform;
            m_LiftTime = lifetime;
        }
    }
}