using UnityEngine;

namespace Framework.UI
{
    public abstract class UIBase: MonoBehaviour
    {
        protected bool mInitialized = false;
        protected EUIState mState = EUIState.NONE;

        public EUIState State
        {
            get { return mState; }
        }

        protected void Init()
        {
            mInitialized = true;
        }
        public virtual void OpenUI()
        {
            mState = EUIState.OPEN;
        }
        public virtual void CloseUI()
        {
            mState = EUIState.CLOSE;
        }
        public virtual void Preopen()
        {
            mState = EUIState.PREOPEN;
        }
        public virtual void HideUI()
        {
            mState = EUIState.HIDE;
        }
    }
}
