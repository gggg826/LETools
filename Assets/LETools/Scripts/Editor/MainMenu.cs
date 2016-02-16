/*****************************
*
*  Author : TheNO.5
*
******************************/

 
using UnityEngine;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    // [MenuItem("Assets/Create Prefabs", false, 1000)]
    [MenuItem("LET/LE Tools Bar")]
    static void OpenBar()
    {
        ToolsBar.InitBar();
    }

    [MenuItem("LET/Create Prefabs By Selected")]
    static void CreatePrefabsBySelectedMenu()
    {
        LETools.CreatePrefabs();
    }
    
        [MenuItem("LET/Export/Export Scenes To XML")]
    static void ExportScenesToXMLMenu()
    {
        LETools.ExportScenesToXML();
    }

    [MenuItem("LET/Export/Export Scenes To JSON")]
    static void ExportScenesToJsonMenu()
    {
        LETools.ExportScenesToJSON();
    }

    [MenuItem("LET/Export/Export Scenes To Binary")]
    static void ExportScenesToBinary()
    {
        LETools.ExportScenesToBINARY();
    }

    [MenuItem("LET/Test(测试用)")]
    static void TestMenu()
    {
        Test.JustTest();
    }
}