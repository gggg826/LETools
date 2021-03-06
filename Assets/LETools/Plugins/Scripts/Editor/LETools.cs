﻿/*****************************
*
*  Author : TheNO.5
*
******************************/


using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Text;
using LitJson;

public class LETools
{
    static public void CreatePrefabs()
    {

        Object[] selectedObjs = Selection.GetFiltered(typeof(GameObject), SelectionMode.DeepAssets);
        if (selectedObjs.Length == 0)
        {
            EditorUtility.DisplayDialog("Please Select GameObject",
                            "The selection must contain at least one GameObject!", "Ok");
            return;
        }

        string savePath = EditorUtility.SaveFolderPanel("Choose Folder To Save Prefabs", Application.dataPath, "");

        foreach (GameObject obj in selectedObjs)
        {
            string name = obj.name;
            Object temp = PrefabUtility.CreateEmptyPrefab(GetSavePath(savePath) + name + ".prefab");
            temp = PrefabUtility.ReplacePrefab(obj, temp);
        }

        AssetDatabase.Refresh();
    }

    static string GetSavePath(string path)
    {
        string[] temp = path.Split('/');
        string savePath = null;
        int index = 0;
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i] == "Assets")
            {
                index = i;
                break;
            }
        }

        for (int j = index; j < temp.Length; j++)
        {
            savePath += (temp[j] + "/");
        }

        return savePath;
    }

    static public void ExportScenesToXML()
    {
        string path = EditorUtility.SaveFilePanel("SaveXML", Application.dataPath, "Scenes_Config_XML", "xml");

        XmlDocument xmlDoc = new XmlDocument();
        XmlDeclaration xmlDec = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        xmlDoc.AppendChild(xmlDec);
        XmlElement root = xmlDoc.CreateElement("root");
        foreach (EditorBuildSettingsScene s in EditorBuildSettings.scenes)
        {
            if (s.enabled)
            {
                EditorApplication.OpenScene(s.path);
                XmlElement scene = xmlDoc.CreateElement("Scenes");
                scene.SetAttribute("Name", s.path);
                foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
                {
                    if (go.transform.parent == null)
                    {
                        XmlElement g = xmlDoc.CreateElement("gameObject");
                        g.SetAttribute("Name", go.name);
                        XmlElement trans = xmlDoc.CreateElement("Transform");

                        XmlElement pos = xmlDoc.CreateElement("Position");
                        XmlElement pos_x = xmlDoc.CreateElement("X");
                        pos_x.InnerText = go.transform.position.x.ToString();
                        XmlElement pos_y = xmlDoc.CreateElement("Y");
                        pos_y.InnerText = go.transform.position.y.ToString();
                        XmlElement pos_z = xmlDoc.CreateElement("Z");
                        pos_z.InnerText = go.transform.position.z.ToString();
                        pos.AppendChild(pos_x);
                        pos.AppendChild(pos_y);
                        pos.AppendChild(pos_z);

                        XmlElement rot = xmlDoc.CreateElement("Rotation");
                        XmlElement rot_x = xmlDoc.CreateElement("X");
                        rot_x.InnerText = go.transform.eulerAngles.x.ToString();
                        XmlElement rot_y = xmlDoc.CreateElement("Y");
                        rot_y.InnerText = go.transform.eulerAngles.y.ToString();
                        XmlElement rot_z = xmlDoc.CreateElement("Z");
                        rot_z.InnerText = go.transform.eulerAngles.z.ToString();
                        rot.AppendChild(rot_x);
                        rot.AppendChild(rot_y);
                        rot.AppendChild(rot_z);

                        XmlElement sca = xmlDoc.CreateElement("Scale");
                        XmlElement sca_x = xmlDoc.CreateElement("X");
                        sca_x.InnerText = go.transform.localScale.x.ToString();
                        XmlElement sca_y = xmlDoc.CreateElement("Y");
                        sca_y.InnerText = go.transform.localScale.y.ToString();
                        XmlElement sca_z = xmlDoc.CreateElement("Z");
                        sca_z.InnerText = go.transform.localScale.z.ToString();
                        sca.AppendChild(sca_x);
                        sca.AppendChild(sca_y);
                        sca.AppendChild(sca_z);

                        trans.AppendChild(pos);
                        trans.AppendChild(rot);
                        trans.AppendChild(sca);
                        g.AppendChild(trans);
                        scene.AppendChild(g);
                    }
                }
                root.AppendChild(scene);
            }
        }
        xmlDoc.AppendChild(root);
        xmlDoc.Save(path);
        AssetDatabase.Refresh();
    }

    static public void ExportScenesToJSON()
    {
        string path = EditorUtility.SaveFilePanel("SaveJason", Application.dataPath, "Scenes_Config_JSON", "txt");
        FileInfo fileInfo = new FileInfo(path);
        StreamWriter sw = fileInfo.CreateText();

        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);
        writer.WriteObjectStart();
        writer.WritePropertyName("root");
        writer.WriteArrayStart();

        foreach (EditorBuildSettingsScene S in EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                EditorApplication.OpenScene(S.path);
                writer.WriteObjectStart();
                writer.WritePropertyName("Scene");
                writer.WriteArrayStart();
                writer.WriteObjectStart();
                writer.WritePropertyName("SceneName");
                writer.Write(S.path);
                writer.WritePropertyName("GameObjects");
                writer.WriteArrayStart();

                foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
                {
                    if (obj.transform.parent == null)
                    {
                        writer.WriteObjectStart();
                        writer.WritePropertyName("GameObjectName");
                        writer.Write(obj.name);

                        writer.WritePropertyName("Position");
                        writer.WriteArrayStart();
                        writer.WriteObjectStart();
                        writer.WritePropertyName("X");
                        writer.Write(obj.transform.position.x.ToString("F5"));
                        writer.WritePropertyName("Y");
                        writer.Write(obj.transform.position.y.ToString("F5"));
                        writer.WritePropertyName("Z");
                        writer.Write(obj.transform.position.z.ToString("F5"));
                        writer.WriteObjectEnd();
                        writer.WriteArrayEnd();

                        writer.WritePropertyName("Rotation");
                        writer.WriteArrayStart();
                        writer.WriteObjectStart();
                        writer.WritePropertyName("X");
                        writer.Write(obj.transform.rotation.eulerAngles.x.ToString("F5"));
                        writer.WritePropertyName("Y");
                        writer.Write(obj.transform.rotation.eulerAngles.y.ToString("F5"));
                        writer.WritePropertyName("Z");
                        writer.Write(obj.transform.rotation.eulerAngles.z.ToString("F5"));
                        writer.WriteObjectEnd();
                        writer.WriteArrayEnd();

                        writer.WritePropertyName("Scale");
                        writer.WriteArrayStart();
                        writer.WriteObjectStart();
                        writer.WritePropertyName("X");
                        writer.Write(obj.transform.localScale.x.ToString("F5"));
                        writer.WritePropertyName("Y");
                        writer.Write(obj.transform.localScale.y.ToString("F5"));
                        writer.WritePropertyName("Z");
                        writer.Write(obj.transform.localScale.z.ToString("F5"));
                        writer.WriteObjectEnd();
                        writer.WriteArrayEnd();

                        writer.WriteObjectEnd();
                    }
                }

                writer.WriteArrayEnd();
                writer.WriteObjectEnd();
                writer.WriteArrayEnd();
                writer.WriteObjectEnd();
            }
        }
        writer.WriteArrayEnd();
        writer.WriteObjectEnd();
        sw.WriteLine(sb.ToString());
        sw.Close();
        sw.Dispose();
        AssetDatabase.Refresh();
    }

    static public void ExportScenesToBINARY()
    {
        string path = EditorUtility.SaveFilePanel("SaveBINARY", Application.dataPath, "Scenes_Config_BINARY", "txt");

        FileStream fs = new FileStream(path, FileMode.Create);
        BinaryWriter bw = new BinaryWriter(fs);
        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                EditorApplication.OpenScene(S.path);

                foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
                {
                    if (obj.transform.parent != null)
                        continue;

                    bw.Write(S.path);
                    bw.Write(obj.name);
                    short posx = (short)(obj.transform.position.x * 100);
                    bw.Write(posx);
                    bw.Write((short)(obj.transform.position.y * 100f));
                    bw.Write((short)(obj.transform.position.z * 100f));
                    bw.Write((short)(obj.transform.rotation.eulerAngles.x * 100f));
                    bw.Write((short)(obj.transform.rotation.eulerAngles.y * 100f));
                    bw.Write((short)(obj.transform.rotation.eulerAngles.z * 100f));
                    bw.Write((short)(obj.transform.localScale.x * 100f));
                    bw.Write((short)(obj.transform.localScale.y * 100f));
                    bw.Write((short)(obj.transform.localScale.z * 100f));
                }
            }
        }

        bw.Flush();
        bw.Close();
        fs.Close();
        AssetDatabase.Refresh();
    }
}