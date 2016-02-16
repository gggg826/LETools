/*****************************
*
*  Author : TheNO.5
*
******************************/


using UnityEngine;
using UnityEditor;

public class ToolsBar : EditorWindow
{

    
    static public void InitBar()
    {
        EditorWindow.GetWindow(typeof(ToolsBar));
    }

    private Texture logo;

    void Awake()
    {
        logo = Resources.Load<Texture>("LOGO");
    }


    void OnGUI()
    {
        float logoW = Screen.width - 20;
        float logoH = (Screen.width - 20) / 448f * 170f;

        GUI.DrawTexture(new Rect(10, 20, logoW, logoH), logo);
        
        if (GUI.Button(new Rect(10, logoH + 25, 200, 20), "Create Prefabs By Select"))
        {
            LETools.CreatePrefabs();
        }
        
                if (GUI.Button(new Rect(10, logoH + 50, 200, 20), "Export Scenes To XML"))
        {
            LETools.ExportScenesToXML();
        }

        if (GUI.Button(new Rect(10, logoH + 75, 200, 20), "Export Scenes To JSON"))
        {
            LETools.ExportScenesToJSON();
        }

        if (GUI.Button(new Rect(10, logoH + 100, 200, 20), "Export Scenes To BINARY"))
        {
            LETools.ExportScenesToBINARY();
        }

        if (GUI.Button(new Rect(10, logoH + 125, 200, 20), "Close"))
        {
            Close();
        }
    }

    //Rect GetRectByIndex(int id)
    //{
    //    return new Rect(0, 0, 0, 0);
    //}
}












// //输入文字的内容
// private string text;
// //选择贴图的对象
// private Texture texture;

// public void Awake()
// {
//     //在资源中读取一张贴图
//     texture = Resources.Load("1") as Texture;
// }

// //绘制窗口时调用
// void OnGUI()
// {
//     //输入框控件
//     text = EditorGUILayout.TextField("输入文字:", text);

//     if (GUILayout.Button("打开通知", GUILayout.Width(200)))
//     {
//         //打开一个通知栏
//         this.ShowNotification(new GUIContent("This is a Notification"));
//     }

//     if (GUILayout.Button("关闭通知", GUILayout.Width(200)))
//     {
//         //关闭通知栏
//         this.RemoveNotification();
//     }


//     if (GUILayout.Button("Create Prefabs By Select", GUILayout.Width(200)))
//     {
//         LET.CreatePrefabs();
//     }

//     //文本框显示鼠标在窗口的位置
//     EditorGUILayout.LabelField("鼠标在窗口的位置", Event.current.mousePosition.ToString());

//     //选择贴图
//     texture = EditorGUILayout.ObjectField("添加贴图", texture, typeof(Texture), true) as Texture;

//     if (GUILayout.Button("关闭窗口", GUILayout.Width(200)))
//     {
//         //关闭窗口
//         this.Close();
//     }

// }

// //更新
// void Update()
// {

// }

// void OnFocus()
// {
//     Debug.Log("当窗口获得焦点时调用一次");
// }

// void OnLostFocus()
// {
//     Debug.Log("当窗口丢失焦点时调用一次");
// }

// void OnHierarchyChange()
// {
//     Debug.Log("当Hierarchy视图中的任何对象发生改变时调用一次");
// }

// void OnProjectChange()
// {
//     Debug.Log("当Project视图中的资源发生改变时调用一次");
// }

// void OnInspectorUpdate()
// {
//     //Debug.Log("窗口面板的更新");
//     //这里开启窗口的重绘，不然窗口信息不会刷新
//     this.Repaint();
// }

// void OnSelectionChange()
// {
//     //当窗口出去开启状态，并且在Hierarchy视图中选择某游戏对象时调用
//     foreach (Transform t in Selection.transforms)
//     {
//         //有可能是多选，这里开启一个循环打印选中游戏对象的名称
//         Debug.Log("OnSelectionChange" + t.name);
//     }
// }

// void OnDestroy()
// {
//     Debug.Log("当窗口关闭时调用");
// }