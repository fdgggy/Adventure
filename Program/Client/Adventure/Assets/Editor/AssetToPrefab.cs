/*******************************************************************
** 文件名:	AssetToPrefab.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.1.16   15:33:28
** 版  本:	1.0
** 描  述:	Asset -> Prefab
** 应  用:  指定路径下Asset制作成Prefab

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
public class AssetToPrefab
{
    virtual public void BuildPrefab(string src, string des, string filetype) { }

    public AssetToPrefab()
    {
        Init();
    }
    private void Init()
    {
        List<string> paths = GetSelectionFoldersPath();
        
        if (paths != null)
        {
            foreach (string path in paths)
            {
                string pathname = path.Substring(path.LastIndexOf("/") + 1);
                string apppath = Application.dataPath + "/Resources/Prefabs";
                string dest = System.IO.Path.Combine(apppath, pathname);

                BuildSelectPrefabs(path, dest);
            }
        }
    }
    /// <summary>
    /// 目录下的资源批量成预制，不支持单个文件制作
    /// </summary>
    /// <param name="source"></param>
    /// <param name="dest"></param>
    private void BuildSelectPrefabs(string source, string dest)
    {
        DirectoryInfo dir = new DirectoryInfo(source);

        if (Directory.Exists(dest) == false)
        {
            Directory.CreateDirectory(dest);
            AssetDatabase.Refresh();
        }

        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string fileName = file.Name;
            string fileType = fileName.Substring(fileName.LastIndexOf("."));
            if (fileType.Equals(".meta") == false)
            {
                string tempsource = file.FullName.Substring(file.FullName.LastIndexOf("Asset"));
                string tempdest = dest.Substring(dest.LastIndexOf("Asset"));

                BuildPrefab(tempsource, tempdest, fileType);
                
                AssetDatabase.Refresh();
            }
        }

        DirectoryInfo[] dirs = dir.GetDirectories();
        if (dirs != null)
        {
            foreach (DirectoryInfo d in dirs)
            {
                string temppath = Path.Combine(dest, d.Name);
                BuildSelectPrefabs(d.FullName, temppath);
            }
        }
    }
    private List<string> GetSelectionFoldersPath()
    {
        List<string> paths = new List<string>();
        UnityEngine.Object[] objs = Selection.objects;
        if (objs.Length > 0)
        {
            foreach (UnityEngine.Object obj in objs)
            {
                string path = AssetDatabase.GetAssetPath(obj.GetInstanceID());

                if (Directory.Exists(path))
                {
                    paths.Add(path);
                }
            }
        }

        if (paths.Count > 0)
            return paths;

        return null;
    }
}
