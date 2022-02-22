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

    public class ExcelConvertWindow : EditorWindow
    {
        private string title = "Excel文件格式转换";
        private string SpannedFileName = "";             // 生成文件的文件名
        private string PathExcel = "";                   // 所有的Excel文件的路径
        private string SavePath = "";                    // 转换后文件保存的路径

        private bool bToCSsharp;                         // 是否选中生成C#
        private bool bToBinary;                          // 是否选中生成二进制文件
        private bool bToJSON;                            // 是否选中生成JSON文件
        private bool bToXML;                             // 是否选中生成XML文件
        private bool groupEnabled;                       //区域开关

        public ExcelConvertWindow()
        {
            this.titleContent = new GUIContent("Excel文件格式转换");
        }

        // % = ctrl, & = Alt, # = shift
        [MenuItem("LTools/文件转换/Excel转其他格式  %T")]
        private static void ShowWindow()
        {
            Rect rect = new Rect(Screen.width/2, Screen.height/2, 375, 475);
            EditorWindow.GetWindowWithRect(typeof(ExcelConvertWindow), rect);
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Space(15);
            GUI.skin.label.fontSize = 24;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            // 绘制小标题为粗体
            GUILayout.Label(title);
            EditorGUILayout.EndVertical();

            //GUILayout.Space(20);
            //GUILayout.Label("Time: "+ System.DateTime.Now);

            GUILayout.Space(15);
            GUILayout.Label("Excel Path", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(PathExcel, GUILayout.ExpandWidth(false));
            if (GUILayout.Button("Browse", GUILayout.ExpandWidth(false)))
            {
                PathExcel = EditorUtility.SaveFolderPanel("Path to Excel", PathExcel, Application.dataPath);
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(15);
            GUI.skin.toggle.fontStyle = FontStyle.BoldAndItalic;
            //groupEnabled = EditorGUILayout.BeginToggleGroup("选择生成文件的格式", groupEnabled);
            EditorGUILayout.BeginHorizontal();
            bToCSsharp = EditorGUILayout.Toggle("C#", bToCSsharp);
            bToBinary = EditorGUILayout.Toggle("二进制", bToBinary);
            bToJSON = EditorGUILayout.Toggle("Json", bToJSON);
            bToXML = EditorGUILayout.Toggle("Xml", bToXML);
            if(bToCSsharp)
            {

            }
            EditorGUILayout.EndHorizontal();
            //EditorGUILayout.EndToggleGroup();

            // 设置生成文件的文件名
            GUILayout.Space(15);
            SpannedFileName = EditorGUILayout.TextField("文件名称", SpannedFileName);

            GUILayout.Space(30);
            GUI.skin.button.alignment = TextAnchor.MiddleCenter;
            if (GUILayout.Button("开始转换", GUILayout.Width(115), GUILayout.Height(50)))
            {
                //StartConvert();
            }
        }
    }
}
