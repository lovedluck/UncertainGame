using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Pooling
{
    public class ObjectPool : IObjectPool
    {
        private readonly Queue<GameObject> m_PoolQueue;
        private readonly List<GameObject> mInUseObjects;
        // 对象池名称
        protected string m_PoolName;
        // 需要缓存的对象
        protected GameObject prefab;
        protected Transform m_Parent;
        protected int Capacity { get; set; }
        public GameObject Prefab
        {
            get => prefab;
            set => prefab = value;
        }

        public string PoolName
        {
            get => m_PoolName;
            set => m_PoolName = value;
        }

        /// <summary>
        /// 获取游戏对象队列
        /// </summary>
        public Queue<GameObject> PoolQueue
        {
            get => m_PoolQueue;
        }

        /// <summary>
        /// 获取当前所有正在使用中的游戏对象列表。
        /// </summary>
        public List<GameObject> InUseObjects
        {
            get => mInUseObjects;
        }

        /// <summary>
        /// 构造一个<see cref="ObjectPool&lt;T&gt;"/>的实例，并指定容量无上限。
        /// </summary>
        public ObjectPool()
        {
            Capacity = int.MaxValue;
            mInUseObjects = new List<GameObject>();
            m_PoolQueue = new Queue<GameObject>();
        }

        /// <summary>
        /// 构造一个<see cref="ObjectPool&lt;T&gt;"/>的实例，并指定最大容量。
        /// </summary>
        /// <param name="capacity">最大容量</param>
        public ObjectPool(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException("capacity is zero!");

            Capacity = capacity;
            mInUseObjects = new List<GameObject>();
            m_PoolQueue = new Queue<GameObject>();
        }

        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="poolName">对象池名称</param>
        /// <param name="transform">对象池父物体位置</param>
        public virtual void Init(string poolName, Transform transform)
        {
            m_PoolName = poolName;
            m_Parent = transform;
        }

        /// <summary>
        /// 从对象池中获取一个对象。
        /// </summary>
        /// <returns></returns>
        public virtual GameObject Get(float lifetime)
        {
            if (lifetime < 0)
            {
                return null;
            }
            GameObject returnObj;
            if (PoolQueue.Count > 0)
            {
                // 从对象池中取出对象，同时也要移除对象
                returnObj = PoolQueue.Dequeue();
            }
            else
            {
                returnObj = Create();
            }
            ObjectInfo info = returnObj.GetComponent<ObjectInfo>();
            if (info == null)
            {
                info = returnObj.AddComponent<ObjectInfo>();
            }
            info.poolName = PoolName;
            if (lifetime > 0)
            {
                info.lifeTime = lifetime;
            }
            return returnObj;
        }

        /// <summary>
        /// 当需要创建一个新的对象时调用此方法。
        /// </summary>
        /// <returns></returns>
        protected virtual GameObject Create()
        {
            if (prefab == null)
            {
                LDebug.Instance.PrintLog(EDebugGrade.ERROR, "没有可以用来生成的对象, prefab = null");
                return null;
            }
            GameObject newObj;
            newObj = GameObject.Instantiate<GameObject>(prefab);
            newObj.transform.SetParent(m_Parent);
            newObj.SetActive(false);
            return newObj;
        }

        /// <summary>
        /// 将指定的对象回收到对象池。
        /// </summary>
        /// <param name="obj">对象</param>
        public virtual void Put(GameObject obj)
        {
            if (PoolQueue.Contains(obj))
            {
                return;
            }
            if (PoolQueue.Count > Capacity)
            {
                GameObject.Destroy(obj);
            }
            else
            {
                PoolQueue.Enqueue(obj);
                obj.SetActive(false);
            }
        }

        /// <summary>
        /// 回收毁灭对象
        /// </summary>
        public virtual void Destroy()
        {
            foreach (var item in PoolQueue)
            {
                UnityEngine.Object.Destroy(item as UnityEngine.Object);
            }
            PoolQueue.Clear();
        }
    }
}