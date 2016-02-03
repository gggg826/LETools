/*****************************
*
*  Author : TheNO.5
*
******************************/


using UnityEngine;
using System.IO;
using System.Xml;

public class LET : MonoBehaviour 
{
    static public void LoadScenes(string xmlPath, string sceneName)
    {
        //string path = GetStreamingAssetFilePath(xmlPath);
        string path = Application.streamingAssetsPath + "/" + xmlPath;

        if (!File.Exists(path))
        {
            Debug.LogError("The filePath is not found.");
            return;
        }
        
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(path);
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
        foreach(XmlElement scene in nodeList)
        {
            if(!scene.GetAttribute("Name").Equals("Assets/" + sceneName + ".unity"))
                continue;
            foreach(XmlElement go in scene.ChildNodes)
            {
                string prefab = "Prefabs/" + go.GetAttribute("Name");
                Vector3 mpos = Vector3.zero;
                Vector3 mrot = Vector3.zero;
                Vector3 msca = Vector3.zero;
                
                foreach(XmlElement trans in go.ChildNodes)
                {
                    foreach(XmlElement t in trans.ChildNodes)
                    {
                        if(t.Name == "Position")
                        {
                            foreach(XmlElement pos in t.ChildNodes)
                            {
                                switch (pos.Name)
                                {
                                    case "X":
                                    mpos.x = float.Parse(pos.InnerText);
                                    break;
                                    case "Y":
                                    mpos.y = float.Parse(pos.InnerText);
                                    break;
                                    case "Z":
                                    mpos.z = float.Parse(pos.InnerText);
                                    break;
                                    
                                }
                            }
                        }
                        
                        else if(t.Name == "Rotation")
                        {
                            foreach(XmlElement rot in t.ChildNodes)
                            {
                                switch (rot.Name)
                                {
                                    case "X":
                                    mrot.x = float.Parse(rot.InnerText);
                                    break;
                                    case "Y":
                                    mrot.y = float.Parse(rot.InnerText);
                                    break;
                                    case "Z":
                                    mrot.z = float.Parse(rot.InnerText);
                                    break;
                                    
                                }
                            }
                        }
                        
                        else if(t.Name == "Scale")
                        {
                            foreach(XmlElement sca in t.ChildNodes)
                            {
                                switch (sca.Name)
                                {
                                    case "X":
                                    msca.x = float.Parse(sca.InnerText);
                                    break;
                                    case "Y":
                                    msca.y = float.Parse(sca.InnerText);
                                    break;
                                    case "Z":
                                    msca.z = float.Parse(sca.InnerText);
                                    break;
                                    
                                }
                            }
                        }
                        
                    }
                }
                GameObject gb = (GameObject)Instantiate(Resources.Load(prefab), mpos, Quaternion.Euler(mrot));
                        gb.transform.localScale = msca;
            }
        }
    }

    static string GetStreamingAssetFilePath(string fileName)
    {
        string path;
#if UNITY_EDITOR
        path = path = Application.dataPath + "/StreamingAssets/" + fileName;
#elif UNITY_IPHONE
        path = path = Application.dataPath + "/Raw/" + fileName;
#elif UNITY_ANDROID
        path = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
#endif

        return path;
    }
}