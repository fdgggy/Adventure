/*******************************************************************
** 文件名:	EctypeCsv.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.1.21   13:42:28
** 版  本:	1.0
** 描  述:	副本表
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
public class EctypeCsvData
{
    public int ectypeID;
    public string sceneID;
    public int level;
    public string ectypeName;
    public int monsterID;
}
public class EctypeCsv : ISchemeUpdateSink
{
    private Dictionary<int, EctypeCsvData> m_ectypeDic;

    public EctypeCsv()
    {
        m_ectypeDic = new Dictionary<int, EctypeCsvData>();
    }
    private bool m_bIsLoaded = false;
    public bool mIsLoaded { get { return m_bIsLoaded; } set { m_bIsLoaded = value; } }
    public void Clear() { m_ectypeDic.Clear(); }
    public bool OnSchemeLoad(ICsvReader reader)
    {
        int nRowCount = reader.GetRowCount();
        m_ectypeDic.Clear();

        try
        {
            for (int i = 0; i < nRowCount; i++)
            {
                int j = 0;
                EctypeCsvData data = new EctypeCsvData();

                reader.GetData(out data.ectypeID, i, j++, "副本ID");
                reader.GetData(out data.sceneID, i, j++, "场景ID");
                reader.GetData(out data.level, i, j++, "等级");
                reader.GetData(out data.ectypeName, i, j++, "名称");
                reader.GetData(out data.monsterID, i, j++, "摆怪ID");

                if (m_ectypeDic.ContainsKey(data.ectypeID))
                {
                    Debug.Log("EctypeCsv::OnSchemeLoad 存在相同的资源包名称 ectypeID=" + data.ectypeID);
                    continue;
                }

                m_ectypeDic.Add(data.ectypeID, data);
            }
        }
        catch (Exception e)
        {
            Debug.Log("EctypeCsv OnSchemeLoad Load Config error.. msg=" + e.Message);
            return false;
        }

        return true;
    }
    // 查找
    public EctypeCsvData Lookup(int ID)
    {
        EctypeCsvData ectypeData;
        bool ret = m_ectypeDic.TryGetValue(ID, out ectypeData);
        if (ret)
            return ectypeData;
        return null;
    }
}
