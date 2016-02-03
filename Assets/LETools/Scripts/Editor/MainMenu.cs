/*****************************
*
*  Author : TheNO.5
*
******************************/

 
using UnityEngine;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    [MenuItem("LET/Create Prefabs By Selected")]
    // [MenuItem("Assets/Create Prefabs", false, 1000)]
    static void CreatePrefabsBySelectedMenu()
    {
        LETools.CreatePrefabs();
    }
    
        [MenuItem("LET/Export Scenes To XML")]
    // [MenuItem("Assets/Create Prefabs", false, 1000)]
    static void ExportScenesToXMLMenu()
    {
        LETools.ExportScenesToXML();
    }
}