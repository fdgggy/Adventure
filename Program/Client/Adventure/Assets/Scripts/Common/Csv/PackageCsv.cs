/*******************************************************************
** 文件名:	ResourceCsv.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   3:13:28
** 版  本:	1.0
** 描  述:	资源包表
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

public class PackageCsvData
{
    public string packageID;
    public int version;
}
public class PackageCsv : ISchemeUpdateSink
{
    private Dictionary<string, PackageCsvData> m_PackageDic;

    public PackageCsv()
    {
        m_PackageDic = new Dictionary<string, PackageCsvData>();
    }
    private bool m_bIsLoaded = false;
    public bool mIsLoaded { get { return m_bIsLoaded; } set { m_bIsLoaded = value; } }
    public void Clear() { m_PackageDic.Clear(); }
    public bool OnSchemeLoad(ICsvReader reader)
    {
        int nRowCount = reader.GetRowCount();
        m_PackageDic.Clear();
        string tempStr = "";
        try
        {
            for (int i = 0; i < nRowCount; i++)
            {
                int j = 0;
                PackageCsvData data = new PackageCsvData();

                reader.GetData(out tempStr, i, j++, "资源包Id");
                //去首尾空格 变小写
                data.packageID = tempStr.Trim().ToLower();
                reader.GetData(out data.version, i, j++, "资源包版本");

                if (m_PackageDic.ContainsKey(data.packageID))
                {
                    Debug.Log("PackageCsv::OnSchemeLoad 存在相同的资源包名称 packageID=" + data.packageID);
                    continue;
                }

                m_PackageDic.Add(data.packageID, data);
            }
        }
        catch (Exception e)
        {
            Debug.Log("PackageCsv OnSchemeLoad Load Config error.. msg=" + e.Message);
            return false;
        }

        return true;
    }
    // 查找
    public PackageCsvData Lookup(string packageID)
    {
        PackageCsvData packData;
        bool ret = m_PackageDic.TryGetValue(packageID, out packData);
        if (ret)
            return packData;
        return null;
    }
}
