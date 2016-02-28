/*******************************************************************
** 文件名:	SceneCsv.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.1.20   11:01:28
** 版  本:	1.0
** 描  述:	场景表
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneCsvData
{
    public string sceneID;
    public string sceneName;
    public string resourceId;
    public string musicId;
}
public class SceneCsv : ISchemeUpdateSink
{
    private Dictionary<string, SceneCsvData> m_SceneDic;

    public SceneCsv()
    {
        m_SceneDic = new Dictionary<string, SceneCsvData>();
    }
    private bool m_bIsLoaded = false;
    public bool mIsLoaded { get { return m_bIsLoaded; } set { m_bIsLoaded = value; } }
    public void Clear() { m_SceneDic.Clear(); }
    public bool OnSchemeLoad(ICsvReader reader)
    {
        int nRowCount = reader.GetRowCount();
        m_SceneDic.Clear();
        try
        {
            for (int i = 0; i < nRowCount; i++)
            {
                int j = 0;
                SceneCsvData data = new SceneCsvData();

                reader.GetData(out data.sceneID, i, j++, "场景ID");
                reader.GetData(out data.sceneName, i, j++, "场景名称");
                reader.GetData(out data.resourceId, i, j++, "场景资源ID");
                reader.GetData(out data.musicId, i, j++, "场景背景音乐ID");

                if (m_SceneDic.ContainsKey(data.sceneID))
                {
                    Debug.Log("SceneCsv::OnSchemeLoad 存在相同的资源包名称 sceneID=" + data.sceneID);
                    continue;
                }

                m_SceneDic.Add(data.sceneID, data);
            }
        }
        catch (Exception e)
        {
            Debug.Log("SceneCsv OnSchemeLoad Load Config error.. msg=" + e.Message);
            return false;
        }

        return true;
    }
    // 查找
    public SceneCsvData Lookup(string sceneID)
    {
        SceneCsvData sceneData;
        bool ret = m_SceneDic.TryGetValue(sceneID, out sceneData);
        if (ret)
            return sceneData;
        return null;
    }
}
