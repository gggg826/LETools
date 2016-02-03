/*****************************
*
*  Author : TheNO.5
*
******************************/


using UnityEngine;
using UnityEditor;
using System.IO;

public class Test : EditorWindow
{
    [MenuItem("LET/Test/SaveFile InProject")]
    static void JustTest()
    {
        EditorUtility.SaveFilePanelInProject("","","","");
    }
    
    
}