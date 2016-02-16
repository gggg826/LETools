/*****************************
*
*  Author : TheNO.5
*
******************************/

 
using UnityEngine;
using System.Collections;

public class JSONController : MonoBehaviour
{
    void Start()
    {
        LET.LoadScenes("Scenes_Config_JSON.txt", "Test.unity", "json");
    }
}