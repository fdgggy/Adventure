/*******************************************************************
** 文件名:	ResourceCsv.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.20   10:16:28
** 版  本:	1.0
** 描  述:	资源表
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCsvData
{
    public string resID;          //资源物体ID
    public string packID;         //所在资源包ID
    public string relativePath;     //资源相对资源包路径
}

public class ResourceCsv : ISchemeUpdateSink
{
    private Dictionary<string, ResourceCsvData> m_ResourceDic;

    public ResourceCsv()
    {
        m_ResourceDic = new Dictionary<string, ResourceCsvData>();
    }
    private bool m_bIsLoaded = false;
    public bool mIsLoaded { get { return m_bIsLoaded; } set { m_bIsLoaded = value; } }
    public void Clear() { m_ResourceDic.Clear(); }

    public bool OnSchemeLoad(ICsvReader reader)
    {
        int nRowCount = reader.GetRowCount();
        m_ResourceDic.Clear();
        try
        {
            for (int i = 0; i < nRowCount; i++)
            {
                int j = 0;
                ResourceCsvData data = new ResourceCsvData();

                reader.GetData(out data.resID, i, j++, "资源名");
                reader.GetData(out data.packID, i, j++, "资源包名");
                reader.GetData(out data.relativePath, i, j++, "资源相对路径");

                if (m_ResourceDic.ContainsKey(data.resID))
                {
                    Debug.Log("ResourceCsv::OnSchemeLoad 存在相同的资源名称 resID=" + data.resID);
                    continue;
                }
                
                m_ResourceDic.Add(data.resID, data);
            }
        }
        catch (Exception e)
        {
            Debug.Log("ResourceCsv OnSchemeLoad Load Config error.. msg=" + e.Message);
            return false;
        }

        return true;
    }

    // 查找
    public ResourceCsvData Lookup(string resID)
    {
        ResourceCsvData resData;
        bool ret = m_ResourceDic.TryGetValue(resID, out resData);
        if (ret)
            return resData;
        return null;
    }
}