/*****************************
*
*  Author : TheNO.5
*
******************************/

 
using UnityEngine;
using System.Collections;

public class BinaryController : MonoBehaviour
{
    void Start()
    {
        LET.LoadScenes("Scenes_Config_BINARY.txt", "Test.unity", "binary");
    }
}