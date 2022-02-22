using UnityEngine;

namespace Framework.Pooling
{
    public interface IObjectPool
    {
        /// <summary>
        /// 从对象池中获取一个对象。
        /// </summary>
        /// <param name="lifetime">多少秒回收一次</param>
        /// <returns></returns>
        GameObject Get(float lifetime);

        /// <summary>
        /// 将指定的对象回收到对象池。
        /// </summary>
        /// <param name="obj">对象</param>
        void Put(GameObject obj);
    }

    /// <summary>
    /// 对象池模式泛型接口。
    /// </summary>
    /// <remarks>http://en.wikipedia.org/wiki/Object_pool_pattern</remarks>
    /// <typeparam name="T">对象类型</typeparam>
    public interface IObjectPool<T>
    {
        /// <summary>
        /// 从对象池中获取一个对象。
        /// </summary>
        /// <returns>可用的对象</returns>
        T Get();

        /// <summary>
        /// 将指定的对象回收到对象池。
        /// </summary>
        /// <param name="obj">对象</param>
        void Put(T obj);
    }
}