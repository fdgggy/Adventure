/*******************************************************************
** 文件名:	ResMgr.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.19   23:16:28
** 版  本:	1.0
** 描  述:	资源管理器
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleInfo
{
    public AssetBundle m_AssetBundle;
    public int m_ReferencedCount;

    public AssetBundleInfo(AssetBundle assetBundle)
    {
        m_AssetBundle = assetBundle;
        m_ReferencedCount = 0;
    }
}
public class ResMgr
{
    public delegate void ResBackHandle(object obj);
    private IDictionary<string, object> resPool = new Dictionary<string, object>();

    public GameObject LoadResource(string id)
    {
        ResourceCsvData resData = GameMgr.Instance.m_csvMgr.mResourceCsv.Lookup(id);
        string path = resData.relativePath;

        Object obj = Resources.Load(path);
        if (obj != null)
            return GameObject.Instantiate(obj) as GameObject;
        return null;
    }
    public void Destroy(Object obj)
    {
        GameObject.DestroyObject(obj);
    }

    public void LoadPrefab(string prefabName, ResBackHandle callBack)
    {
        object obj = null;
        bool ret = resPool.TryGetValue(prefabName, out obj);
        if (ret)
        {
            if (callBack != null)
            {
                callBack(obj);
            }
        }
        else
        {
            LoadFromDisc(prefabName, (object go) =>
            {
                resPool.Add(prefabName, go);
                GameObject gameObj = go as GameObject;
                gameObj.SetActive(false);
                if (callBack != null)
                {
                    callBack(go);
                }
            });
        }
    }
    /// <summary>
    /// prefabName 先填Asset/...
    /// </summary>
    /// <param name="prefabName"></param>
    /// <param name="callBack"></param>
    private void LoadFromDisc(string prefabName, ResBackHandle callBack)
    {
#if UNITY_EDITOR
        if (!Application.isMobilePlatform)
        {
            Object prefab = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>(prefabName);
            if (callBack != null)
            {
                callBack(prefab);
            }
        }
        else
        {
            LoadFromAssetBundle(prefabName, callBack);
        }
#else
        LoadFromAssetBundle(prefabName, callBack);
#endif
    }

    private void LoadFromAssetBundle(string prefabName, ResBackHandle callBack)
    {

    }
}
