/*******************************************************************
** 文件名:	EctypeMonsterCsv.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.1.21   13:07:28
** 版  本:	1.0
** 描  述:	副本怪物表
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
public class EctypeMonsterCsvData
{
    public int number;
    public int monsterID;
}
public class EctypeMonsterCsv : ISchemeUpdateSink
{
    private Dictionary<int, EctypeMonsterCsvData> m_ectypeMonsterDic;
    public Dictionary<int, EctypeMonsterCsvData> EctypeMonsterDic
    {
        get { return m_ectypeMonsterDic; }
    }
    public EctypeMonsterCsv()
    {
        m_ectypeMonsterDic = new Dictionary<int, EctypeMonsterCsvData>();
    }
    private bool m_bIsLoaded = false;
    public bool mIsLoaded { get { return m_bIsLoaded; } set { m_bIsLoaded = value; } }
    public void Clear() { m_ectypeMonsterDic.Clear(); }
    public bool OnSchemeLoad(ICsvReader reader)
    {
        int nRowCount = reader.GetRowCount();
        m_ectypeMonsterDic.Clear();

        try
        {
            for (int i = 0; i < nRowCount; i++)
            {
                int j = 0;
                EctypeMonsterCsvData data = new EctypeMonsterCsvData();

                reader.GetData(out data.number, i, j++, "序号");
                reader.GetData(out data.monsterID, i, j++, "怪物ID");

                if (m_ectypeMonsterDic.ContainsKey(data.number))
                {
                    Debug.Log("EctypeMonsterCsv::OnSchemeLoad 存在相同的资源包名称 number=" + data.number);
                    continue;
                }

                m_ectypeMonsterDic.Add(data.number, data);
            }
        }
        catch (Exception e)
        {
            Debug.Log("EctypeMonsterCsv OnSchemeLoad Load Config error.. msg=" + e.Message);
            return false;
        }

        return true;
    }
    // 查找
    public EctypeMonsterCsvData Lookup(int number)
    {
        EctypeMonsterCsvData monsterData;
        bool ret = m_ectypeMonsterDic.TryGetValue(number, out monsterData);
        if (ret)
            return monsterData;
        return null;
    }
}
