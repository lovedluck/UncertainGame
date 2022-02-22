using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Framework.Editor
{
    // 准备写一个编辑器的扩展，功能如下：
    // 搞一个EditorWindow，然后列出所有的Excel文件，然后选中Excel文件
    // 然后再选择是把Excel转换成C#、二进制、Json、Xml（多选）
    // 最后点确定按钮，然后完成该功能
    // 这个窗口界面上还可以配置显生成的路径，可以配多个，但是选中功能的必须配置路径
    // 所以还需要在点击确定按钮的时候先判定对应功能的生成路径有没有配置
    public class ExcelConvert : EditorWindow
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
