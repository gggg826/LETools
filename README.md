#LAU EXPORT TOOLS

标签： Unity Editor  Tools

![](http://img.blog.csdn.net/20160204013716479)
---

**2016.02.04** 

version:0.1.0   &#8195;&#8195;&#8195;  [DOWNLOAD]()


 - 批量创建Prefab
 - 将Scene导出为XML/JSON/BINARY配置文件
 - 加载配置文件，还原Scene
 
**Tutorial:**

1.打开Tools面板

&emsp;&emsp;点击菜单栏LET->LE Tools Bar

2.创建Prefab

&emsp;&emsp;选中要制作Prefab的gameObject(可多选)，点击面板Create Prefabs By Select按钮，选择保存路径。

3.将Scene导出为配置文件

&emsp;&emsp;将要导出的Scene注册到Build Setting中，点击面板Export Scenes To XML/JSON/BINARY按钮，选择保存路径为 "Assets/StreamingAssets"。

4.还原Scene

&emsp;&emsp;将Scene中所有gameObject删除并新建Empty gameObject,挂载新脚本，Strat方法中调用
 LET.LoadScenes("配置文件名称", "Scene名称", "配置文件类型");
 如：
 
` LET.LoadScenes("Scenes_Config.xml", "Test.unity", "json");`

###**Warning:** 
 - prefabs 保存路径选在任意Resources/Prefabs/文件夹下。
 
 - 配置文件保存在StreamingAssets/文件夹下。
 
