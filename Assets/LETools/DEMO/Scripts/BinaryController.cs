/*****************************
*
*  Author : TheNO.5
*
******************************/

 
using UnityEngine;

public class BinaryController : MonoBehaviour
{
    void Start()
    {
        LET.LoadScenes("Scenes_Config_BINARY.txt", "LETools/DEMO/Scenes/ARPoseTest.unity", "binary");
    }
}