/*****************************
*
*  Author : TheNO.5
*
******************************/


using UnityEngine;
using System.IO;
using System.Xml;
using LitJson;

public class LET : MonoBehaviour
{
    /// <summary>
    /// 加载场景文件(xml,json,binary)
    /// </summary>
    static public void LoadScenes(string configPath, string sceneName, string style)
    {
        //string path = GetStreamingAssetFilePath(configPath);
        string path = Application.streamingAssetsPath + "/" + configPath;
        if (!File.Exists(path))
        {
            Debug.LogError("The filePath is not found.");
            return;
        }

        if (style == "xml")
            LoadScenesByXML(path, sceneName);
        else if (style == "json")
            LoadScenesByJSON(path, sceneName);
        else if (style == "binary")
            LoadScenesByBINARY(path, sceneName);
    }

    static void LoadScenesByXML(string path, string sceneName)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(path);
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;
        foreach (XmlElement scene in nodeList)
        {
            if (!scene.GetAttribute("Name").Equals("Assets/" + sceneName))
                continue;
            foreach (XmlElement go in scene.ChildNodes)
            {
                string prefab = "Prefabs/" + go.GetAttribute("Name");
                Vector3 mpos = Vector3.zero;
                Vector3 mrot = Vector3.zero;
                Vector3 msca = Vector3.zero;

                foreach (XmlElement trans in go.ChildNodes)
                {
                    foreach (XmlElement t in trans.ChildNodes)
                    {
                        if (t.Name == "Position")
                        {
                            foreach (XmlElement pos in t.ChildNodes)
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

                        else if (t.Name == "Rotation")
                        {
                            foreach (XmlElement rot in t.ChildNodes)
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

                        else if (t.Name == "Scale")
                        {
                            foreach (XmlElement sca in t.ChildNodes)
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

    static void LoadScenesByJSON(string path, string sceneName)
    {
        StreamReader sr = File.OpenText(path);
        string str = sr.ReadToEnd();
        JsonData jd = JsonMapper.ToObject(str);
        JsonData root = jd["root"];

        for (int i = 0; i < root.Count; i++)
        {
            JsonData senseArray = root[i]["Scene"];

            for (int j = 0; j < senseArray.Count; j++)
            {
                string scenename = (string)senseArray[j]["SceneName"];
                if (!scenename.Equals("Assets/" + sceneName))
                {
                    continue;
                }
                JsonData gameObjects = senseArray[j]["GameObjects"];

                for (int k = 0; k < gameObjects.Count; k++)
                {
                    string objectName = (string)gameObjects[k]["GameObjectName"];
                    string prefab = "Prefabs/" + objectName;
                    Vector3 pos = Vector3.zero;
                    Vector3 rot = Vector3.zero;
                    Vector3 sca = Vector3.zero;

                    JsonData position = gameObjects[k]["Position"];
                    JsonData rotation = gameObjects[k]["Rotation"];
                    JsonData scale = gameObjects[k]["Scale"];

                    pos.x = float.Parse((string)position[0]["X"]);
                    pos.y = float.Parse((string)position[0]["Y"]);
                    pos.z = float.Parse((string)position[0]["Z"]);

                    rot.x = float.Parse((string)rotation[0]["X"]);
                    rot.y = float.Parse((string)rotation[0]["Y"]);
                    rot.z = float.Parse((string)rotation[0]["Z"]);

                    sca.x = float.Parse((string)scale[0]["X"]);
                    sca.y = float.Parse((string)scale[0]["Y"]);
                    sca.z = float.Parse((string)scale[0]["Z"]);

                    GameObject ob = (GameObject)Instantiate(Resources.Load(prefab), pos, Quaternion.Euler(rot));
                    ob.transform.localScale = sca;
                }
            }
        }
    }

    static void LoadScenesByBINARY(string path, string _sceneName)
    {
        FileStream fs = new FileStream(path, FileMode.Open);
        BinaryReader br = new BinaryReader(fs);
        int index = 0;
        byte[] tempall = br.ReadBytes((int)fs.Length);
        while (true)
        {
            if (index >= tempall.Length)
            {
                break;
            }

            int sceneLength = tempall[index];
            byte[] sceneName = new byte[sceneLength];
            index += 1;
            System.Array.Copy(tempall, index, sceneName, 0, sceneName.Length);
            string scenename = System.Text.Encoding.Default.GetString(sceneName);
            
            int objectLength = tempall[index + sceneName.Length];
            byte[] objectName = new byte[objectLength];

            index += sceneName.Length + 1;
            System.Array.Copy(tempall, index, objectName, 0, objectName.Length);
            string prefabName = System.Text.Encoding.Default.GetString(objectName);

            index += objectName.Length;
            byte[] posx = new byte[2];
            System.Array.Copy(tempall, index, posx, 0, posx.Length);
            float x = System.BitConverter.ToInt16(posx, 0) / 100.0f;

            index += posx.Length;
            byte[] posy = new byte[2];
            System.Array.Copy(tempall, index, posy, 0, posy.Length);
            float y = System.BitConverter.ToInt16(posy, 0) / 100.0f;

            index += posy.Length;
            byte[] posz = new byte[2];
            System.Array.Copy(tempall, index, posz, 0, posz.Length);
            float z = System.BitConverter.ToInt16(posz, 0) / 100.0f;

            index += posz.Length;
            byte[] rotx = new byte[2];
            System.Array.Copy(tempall, index, rotx, 0, rotx.Length);
            float rx = System.BitConverter.ToInt16(rotx, 0) / 100.0f;

            index += rotx.Length;
            byte[] roty = new byte[2];
            System.Array.Copy(tempall, index, roty, 0, roty.Length);
            float ry = System.BitConverter.ToInt16(roty, 0) / 100.0f;

            index += roty.Length;
            byte[] rotz = new byte[2];
            System.Array.Copy(tempall, index, rotz, 0, rotz.Length);
            float rz = System.BitConverter.ToInt16(rotz, 0) / 100.0f;

            index += rotz.Length;
            byte[] scax = new byte[2];
            System.Array.Copy(tempall, index, scax, 0, scax.Length);
            float sx = System.BitConverter.ToInt16(scax, 0) / 100.0f;

            index += scax.Length;
            byte[] scay = new byte[2];
            System.Array.Copy(tempall, index, scay, 0, scay.Length);
            float sy = System.BitConverter.ToInt16(scay, 0) / 100.0f;

            index += scay.Length;
            byte[] scaz = new byte[2];
            System.Array.Copy(tempall, index, scaz, 0, scaz.Length);
            float sz = System.BitConverter.ToInt16(scaz, 0) / 100.0f;

            index += scaz.Length;

            if (scenename.Equals("Assets/" + _sceneName))
            {
                string prefab = "Prefabs/" + prefabName;
                Vector3 pos = new Vector3(x, y, z);
                Vector3 rot = new Vector3(rx, ry, rz);
                Vector3 sca = new Vector3(sx, sy, sz);
                GameObject ob = (GameObject)Instantiate(Resources.Load(prefab), pos, Quaternion.Euler(rot));
                ob.transform.localScale = sca;
            }
        }
    }



    //    static string GetStreamingAssetFilePath(string fileName)
    //    {
    //        string path;
    //#if UNITY_EDITOR
    //        path = path = Application.dataPath + "/StreamingAssets/" + fileName;
    //#elif UNITY_IPHONE
    //        path = path = Application.dataPath + "/Raw/" + fileName;
    //#elif UNITY_ANDROID
    //        path = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
    //#endif

    //        return path;
    //    }
}