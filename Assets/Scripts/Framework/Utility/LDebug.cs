using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Framework
{
    public class LDebug:Singleton<LDebug>
    {
        private string template = "<color={0}>{1}</color>";
        private string tempWithObj = "<color={0}>{1}</color>, obj: {2}";
        private Dictionary<EDebugGrade, Action<string, object>> _debugDic = new Dictionary<EDebugGrade, Action<string, object>>(5);

        public LDebug()
        {
            _debugDic.Add(EDebugGrade.ERROR, DebugError);
            _debugDic.Add(EDebugGrade.DEBUG, DebugNormal);
            _debugDic.Add(EDebugGrade.INFO, DebugInfo);
            _debugDic.Add(EDebugGrade.TRACE, DebugTrace);
            _debugDic.Add(EDebugGrade.WARN, DebugWarn);
        }

        /// <summary>
        /// 默认日志等级为Debug
        /// </summary>
        private EDebugGrade mDebugGrade = EDebugGrade.DEBUG;

        /// <summary>
        /// 日志默认是开
        /// </summary>
        private EDebugState mDebugState = EDebugState.OPEN;

        public EDebugState DebugState
        {
            get
            {
                return mDebugState;
            }
            set
            {
                mDebugState = value;
            }
        }

        /// <summary>
        /// 对外唯一暴露的接口，一定要传入日志的等级类型
        /// </summary>
        /// <param name="debugGrade"></param>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        public void PrintLog(EDebugGrade debugGrade, string msg, object obj)
        {
            if (mDebugState == EDebugState.CLOSE)
            {
                return;
            }
            mDebugGrade = debugGrade;
            if (_debugDic.Keys.Contains(mDebugGrade))
            {
                _debugDic[mDebugGrade](msg, obj);
            }
        }

        /// <summary>
        /// 重载PrintLog，只需要传入两个参数即可
        /// </summary>
        /// <param name="debugGrade"></param>
        /// <param name="msg"></param>
        public void PrintLog(EDebugGrade debugGrade, string msg)
        {
            if (mDebugState == EDebugState.CLOSE)
            {
                return;
            }
            mDebugGrade = debugGrade;
            if (_debugDic.Keys.Contains(mDebugGrade))
            {
                _debugDic[mDebugGrade](msg, null);
            }
        }

        /// <summary>
        /// TRACE类型日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        private void DebugTrace(string msg, object obj)
        {
            if (obj == null)
            {
                Debug.LogFormat(template, "#63B8FF", msg);
            }
            else
            {
                Debug.LogFormat(tempWithObj, "#63B8FF", msg, obj);
            } 
        }

        /// <summary>
        /// DEBUG类型日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        private void DebugNormal(string msg, object obj)
        {
            if (obj == null)
            {
                Debug.Log(msg);
            }
            else
            {
                Debug.Log("msg: " + msg + " obj:" + obj.ToString());
            }
        }

        /// <summary>
        /// INFO类型日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        private void DebugInfo(string msg, object obj)
        {
            if (obj == null)
            {
                Debug.LogFormat(template, "#FFF68F", msg);
            }
            else
            {
                Debug.LogFormat(tempWithObj, "#FFF68F", msg, obj);
            }
        }

        /// <summary>
        /// WARN类型日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        private void DebugWarn(string msg, object obj)
        {
            if (obj == null)
            {
                Debug.LogWarning(msg);
            }
            else
            {
                Debug.LogWarning("msg: " + msg + " obj:" + obj.ToString());
            }
        }

        /// <summary>
        /// ERROR类型日志
        /// </summary>
        /// <param name="msg">字符信息</param>
        /// <param name="obj">object类型</param>
        private void DebugError(string msg, object obj)
        {
            if (obj == null)
            {
                Debug.LogError(msg);
            }
            else
            {
                Debug.LogError("msg: " + msg + " obj:" + obj.ToString());
            }
        }
    }
}
