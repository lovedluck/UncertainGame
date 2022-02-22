using System.Collections.Generic;
using UnityEngine;

namespace Framework.Pooling
{
    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        private Dictionary<string, ObjectPool> m_PoolDic;
        private Transform m_RootPoolTrans;

        public ObjectPoolManager()
        {
            m_PoolDic = new Dictionary<string, ObjectPool>();
            GameObject go = new GameObject("ObjectPoolManager");
            m_RootPoolTrans = go.transform;
        }

        /// <summary>
        /// 创建一个对象池
        /// </summary>
        /// <typeparam name="T">某个个继承自ObjectPool的对象池</typeparam>
        /// <param name="poolName">对象池名称</param>
        /// <returns></returns>
        public T CreateObjectPool<T>(string poolName) where T : ObjectPool, new()
        {
            if (m_PoolDic.ContainsKey(poolName))
            {
                return m_PoolDic[poolName] as T;
            }

            GameObject obj = new GameObject(poolName);
            obj.transform.SetParent(m_RootPoolTrans);
            T pool = new T();
            pool.Init(poolName, obj.transform);
            m_PoolDic.Add(poolName, pool);
            return pool;
        }

        /// <summary>
        /// 获得某个对象池里面的对象
        /// </summary>
        /// <param name="poolName">对象池</param>
        /// <param name="position">生成的坐标</param>
        /// <param name="lifeTime">定时时间</param>
        /// <returns></returns>
        public GameObject GetGameObject(string poolName, Vector3 position, float lifeTime)
        {
            if (m_PoolDic.ContainsKey(poolName))
            {
                GameObject tempObj = m_PoolDic[poolName].Get(lifeTime);
                if (tempObj != null)
                {
                    tempObj.transform.position = position;
                    tempObj.SetActive(true);
                }
                return tempObj;
            }
            return null;
        }

        /// <summary>
        /// 移除回收某个对象池里面的一个游戏对象
        /// </summary>
        /// <param name="poolName"></param>
        /// <param name="go"></param>
        public void RemoveGameObject(string poolName, GameObject go)
        {
            if (m_PoolDic.ContainsKey(poolName))
            {
                m_PoolDic[poolName].Put(go);
            }
        }

        /// <summary>
        /// 销毁所在父物体
        /// </summary>
        public void Destroy()
        {
            m_PoolDic.Clear();
            GameObject.Destroy(m_RootPoolTrans);
        }
    }
}