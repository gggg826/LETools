/*****************************
*
*  Author : TheNO.5
*
******************************/


using UnityEngine;

public class XmlController : MonoBehaviour 
{
	void Start () 
    {
        LET.LoadScenes("Scenes_Config.xml", "Test");
	}
}