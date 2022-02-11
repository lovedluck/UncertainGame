using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class Singleton<T> where T : class, new()
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }
                return _instance;
            }
        }

        public Singleton()
        {
            if (_instance != null)
            {
                LDebug.Instance.PrintLog(EDebugGrade.INFO, "实例化成功: "+typeof(T).ToString());
            }
            else
            {
                LDebug.Instance.PrintLog(EDebugGrade.ERROR, typeof(T).ToString()+"实例化失败!");
            }
        }

    }
}