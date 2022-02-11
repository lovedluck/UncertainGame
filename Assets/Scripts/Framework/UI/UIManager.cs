using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DicType = System.Collections.Generic.Dictionary<Framework.UI.UIConfig, Framework.UI.UIBase>;

namespace Framework.UI
{
    public class UIManager:Singleton<UIManager>
    {
        protected Transform _transform = null;
        private Dictionary<UIConfig, UIBase> _uiDic = new Dictionary<UIConfig, UIBase>();
        private UIConfig mCurrentOpenUI = UIConfig.NONE;

        /// <summary>
        /// 关闭所有界面
        /// </summary>
        public void CloseAll()
        {
            DicType.ValueCollection vas = _uiDic.Values;
            foreach (var item in vas)
            {
                if (item.State == EUIState.OPEN)
                {
                    item.CloseUI();
                }
            }
        }
        
        /// <summary>
        /// 某个界面是否是处于打开状态
        /// </summary>
        /// <param name="ui">这个界面的枚举编号</param>
        /// <returns></returns>
        public bool IsShow(UIConfig ui)
        {
            if (!_uiDic.Keys.Contains(ui))
            {
                return false;
            }
            bool result = false;
            if (_uiDic[ui].State == EUIState.OPEN)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 展示某个界面
        /// </summary>
        /// <param name="ui"></param>
        public void ShowUI(UIConfig ui)
        {
            if (!_uiDic.Keys.Contains(ui))
            {
                // 通过ui找到对应的gameObject

            }
            else
            {
                _uiDic[ui].OpenUI();
            }
        }

        /// <summary>
        /// 关闭某个界面
        /// </summary>
        /// <param name="ui"></param>
        public void CloseUI(UIConfig ui)
        {
            if (!_uiDic.Keys.Contains(ui))
            {
                return;
            }
            if (_uiDic[ui].State == EUIState.OPEN)
            {
                _uiDic[ui].CloseUI();
            }
        }

        /// <summary>
        /// 创建某个UI界面
        /// </summary>
        /// <typeparam name="T">继承自UIBase的UI脚本</typeparam>
        /// <param name="ui">创建哪个界面</param>
        /// <returns></returns>
        public T CreateUI<T>(UIConfig ui) where T: UIBase
        {
            // 动态加载并且实例化和UI的Prefab，然后获取它身上的脚本
            return null;
        }

        public T FindUI<T>(UIConfig ui) where T:UIBase
        {
            if (!_uiDic.Keys.Contains(ui))
            {
                return null;
            }
            return _uiDic[ui] as T;
        }
    }
}
