using System.Collections;
using UnityEngine;

namespace Framework.Pooling
{
    public class ObjectInfo : MonoBehaviour
    {
        public float lifeTime = 0;
        public string poolName;

        private WaitForSeconds m_WaitTime;
        private void Awake()
        {
            if (lifeTime > 0)
            {
                m_WaitTime = new WaitForSeconds(lifeTime);
            }
        }

        private void OnEnable()
        {
            if (lifeTime > 0)
            {
                StartCoroutine(CountDown(lifeTime));
            }
        }

        IEnumerator CountDown(float lifeTime)
        {
            yield return m_WaitTime;
            ObjectPoolManager.Instance.RemoveGameObject(poolName, gameObject);
        }
    }
}