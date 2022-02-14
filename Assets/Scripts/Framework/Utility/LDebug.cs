using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework
{
    public class LDebug : Singleton<LDebug>
    {
        private Dictionary<EDebugGrade, Action<object, Object>> _debugDic = new Dictionary<EDebugGrade, Action<object, Object>>(5);

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
        /// 对外暴露的接口，格式化打印日志
        /// </summary>
        /// <param name="debugGrade">日志等级</param>
        /// <param name="msg">例："a is {0}, b is {1}"</param>
        /// <param name="obj">可变参数，根据format的格式传入匹配的参数，例：a, b</param>
        public void PrintLogFormat(string format, params object[] args)
        {
            if (mDebugState == EDebugState.CLOSE)
            {
                return;
            }
            Debug.LogFormat(format, args);
        }

        /// <summary>
        /// 对外暴露的接口
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="debugGrade">不传，默认是DEBUG等级</param>
        /// <param name="context">用于的对象</param>
        public void PrintLog(EDebugGrade debugGrade, object msg,  Object context = null)
        {
            if (mDebugState == EDebugState.CLOSE)
            {
                return;
            }
            mDebugGrade = debugGrade;
            if (_debugDic.Keys.Contains(mDebugGrade))
            {
                _debugDic[mDebugGrade](msg, context);
            }
        }

        /// <summary>
        /// 重载PrintLog，只打印字符串，可以指定字符串的颜色
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        public void PrintLog(string msg, string color)
        {
            if (mDebugState == EDebugState.CLOSE)
            {
                return;
            }
            if (string.IsNullOrEmpty(color))
            {
                color = "yellow";
            }
            Debug.Log(FmtColor(color, msg));
        }

        /// <summary>
        /// 重载PrintLog，只打印字符串
        /// </summary>
        /// <param name="msg"></param>
        public void PrintLog(string msg)
        {
            if (mDebugState == EDebugState.CLOSE)
            {
                return;
            }
            DebugNormal(msg);
        }

        /// <summary>
        /// TRACE类型日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        private void DebugTrace(object message, Object context = null)
        {
            Debug.Log(FmtColor("#63B8FF", message), context);
        }

        /// <summary>
        /// DEBUG类型日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        private void DebugNormal(object message, Object context = null)
        {
            Debug.Log(message, context);
        }

        /// <summary>
        /// INFO类型日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        private void DebugInfo(object message, Object context = null)
        {
            Debug.Log(FmtColor("#FFF68F", message), context);
        }

        /// <summary>
        /// WARN类型日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="obj"></param>
        private void DebugWarn(object message, Object context = null)
        {
            Debug.LogWarning(message, context);
        }

        /// <summary>
        /// ERROR类型日志
        /// </summary>
        /// <param name="msg">字符信息</param>
        /// <param name="obj">object类型</param>
        private void DebugError(object message, Object context = null)
        {
            Debug.LogError(message, context);
        }

        /// <summary>
        /// 格式化颜色日志
        /// </summary>
        /// <param name="color"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private object FmtColor(string color, object obj)
        {
            if (obj is string)
            {
#if !UNITY_EDITOR
                return obj;
#else
                return FmtColor(color, (string)obj);
#endif
            }
            else
            {
#if !UNITY_EDITOR
                return obj  
#else
                return string.Format("<color={0}>{1}</color>", color, obj);
#endif
            }
        }

        private object FmtColor(string color, string msg)
        {
#if !UNITY_EDITOR
            return msg;
#else
            int p = msg.IndexOf('\n');
            if (p >= 0)
            {
                // 可以同时显示两行
                p = msg.IndexOf('\n', p + 1);
            }
            if (p < 0 || p > msg.Length - 1)
            {
                return string.Format("<color={0}>{1}</color>", color, msg);
            }
            if (p > 2 && msg[p - 1] == '\r') p--;
            return string.Format("<color={0}>{1}</color>{2}", color, msg.Substring(0, p), msg.Substring(p));
#endif
        }

        #region 解决日志双击溯源问题
#if UNITY_EDITOR
        [UnityEditor.Callbacks.OnOpenAsset(0)]
        static bool OnOpenAsset(int instanceID, int line)
        {
            string stackTrace = GetLogStackTrace();
            if (!string.IsNullOrEmpty(stackTrace) && stackTrace.Contains("LDebug:Log"))
            {
                // 使用正则表达式匹配at的哪个脚本的哪一行
                var matches = System.Text.RegularExpressions.Regex.Match(stackTrace, @"\(at (.+)\)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                string pathLine = "";
                while(matches.Success)
                {
                    pathLine = matches.Groups[1].Value;
                    if (!pathLine.Contains("LDebug.cs"))
                    {
                        int splitIndex = pathLine.LastIndexOf(":");
                        // 脚本路径
                        string path = pathLine.Substring(0, splitIndex);
                        // 行号(第几行)
                        line = Convert.ToInt32(pathLine.Substring(splitIndex + 1));
                        string fullPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets"));
                        fullPath = fullPath + path;
                        // 跳转到目标代码的特定行
                        UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(fullPath.Replace('/', '\\'), line);
                        break;
                    }
                    matches = matches.NextMatch();
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取当前日志窗口选中的日志的堆栈信息
        /// </summary>
        /// <returns></returns>
        private static string GetLogStackTrace()
        {
            // 通过反射获取ConsoleWindow类
            var ConsoleWindowType = typeof(UnityEditor.EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow");
            // 获取窗口实例
            var fieldInfo = ConsoleWindowType.GetField("ms_ConsoleWindow",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            var consoleInstance = fieldInfo.GetValue(null);
            if (consoleInstance != null)
            {
                object objFocusedWindow = UnityEditor.EditorWindow.focusedWindow as object;
                if (objFocusedWindow == consoleInstance)
                {
                    // 获取m_ActiveText成员
                    fieldInfo = ConsoleWindowType.GetField("m_ActiveText", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    // 获取m_ActiveText的值
                    string activeText = fieldInfo.GetValue(consoleInstance).ToString();
                    return activeText;
                }
            }
            return null;
        }
#endif
#endregion 解决日志双击溯源问题
    }
}
