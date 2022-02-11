using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.UI
{
    public enum EUIState
    {
        NONE = 0,
        PREOPEN,
        OPEN,
        HIDE,
        CLOSE,
    }

    // 新建一个界面，就要在这里加一个枚举
    public enum UIConfig
    {
        NONE,
        LOGIN,
        REGISTER,
    }
}
