/*****************************
*
*  Author : TheNO.5
*
******************************/

 
using UnityEngine;

public class JSONController : MonoBehaviour
{
    void Start()
    {
        LET.LoadScenes("Scenes_Config_JSON.txt", "LETools/DEMO/Scenes/ActionCheck.unity", "json");
    }
}