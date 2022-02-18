using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Pooling
{
    public abstract class ObjectPool<T> : IObjectPool<T> where T : class, IDisposable
    {
        private readonly Queue<T> m_PoolQueue;
        private readonly List<T> mInUseObjects;
        protected int Capacity { get; set; }

        /// <summary>
        /// 构造一个<see cref="ObjectPool&lt;T&gt;"/>的实例，并指定容量无上限。
        /// </summary>
        public ObjectPool()
        {
            Capacity = int.MaxValue;
            mInUseObjects = new List<T>();
            m_PoolQueue = new Queue<T>();
        }

        /// <summary>
        /// 构造一个<see cref="ObjectPool&lt;T&gt;"/>的实例，并指定最大容量。
        /// </summary>
        /// <param name="capacity">最大容量</param>
        public ObjectPool(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException("capacity");

            Capacity = capacity;
            mInUseObjects = new List<T>();
            m_PoolQueue = new Queue<T>();
        }

        /// <summary>
        /// 获取当前所有正在使用中的游戏对象列表。
        /// </summary>
        protected List<T> InUseObjects
        {
            get { return mInUseObjects; }
        }

        /// <summary>
        /// 从对象池中获取一个对象。
        /// </summary>
        /// <returns></returns>
        public abstract T Get();

        /// <summary>
        /// 当需要创建一个新的对象时调用此方法。
        /// </summary>
        /// <returns></returns>
        protected abstract T Create();

        /// <summary>
        /// 初始化对象池
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// 将指定的对象回收到对象池。
        /// </summary>
        /// <param name="obj">对象</param>
        public abstract void Put(T obj);

        /// <summary>
        /// 回收毁灭对象
        /// </summary>
        protected abstract void Destroy(); 
    }
}
