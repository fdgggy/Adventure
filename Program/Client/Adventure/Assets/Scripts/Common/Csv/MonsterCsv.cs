/*******************************************************************
** 文件名:	MonsterCsv.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.1.20   21:40:28
** 版  本:	1.0
** 描  述:	怪物表
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
public class MonsterCsvData
{
    public int monsterID;
    public string monsterName;
    public string headID;
    public string resourceID;
    public int bornEffID;
    public int dieEffID;
    public int hitEffID;
    public int celeEffID;
    public int skillID;
    public int curHP;
    public int curLevel;
    public int curType;
    public float moveSpeed;
}
public class MonsterCsv : ISchemeUpdateSink
{
    private Dictionary<int, MonsterCsvData> m_MonsterDic;

    public MonsterCsv()
    {
        m_MonsterDic = new Dictionary<int, MonsterCsvData>();
    }
    private bool m_bIsLoaded = false;
    public bool mIsLoaded { get { return m_bIsLoaded; } set { m_bIsLoaded = value; } }
    public void Clear() { m_MonsterDic.Clear(); }
    public bool OnSchemeLoad(ICsvReader reader)
    {
        int nRowCount = reader.GetRowCount();
        m_MonsterDic.Clear();

        try
        {
            for (int i = 0; i < nRowCount; i++)
            {
                int j = 0;
                MonsterCsvData data = new MonsterCsvData();

                reader.GetData(out data.monsterID, i, j++, "Id");
                reader.GetData(out data.monsterName, i, j++, "名字");
                reader.GetData(out data.headID, i, j++, "头像ID");
                reader.GetData(out data.resourceID, i, j++, "资源ID");
                reader.GetData(out data.bornEffID, i, j++, "出生效果ID");
                reader.GetData(out data.dieEffID, i, j++, "死亡效果ID");
                reader.GetData(out data.hitEffID, i, j++, "受击表现效果ID");
                reader.GetData(out data.celeEffID, i, j++, "庆祝效果ID");
                reader.GetData(out data.skillID, i, j++, "技能ID");
                reader.GetData(out data.curHP, i, j++, "血量");
                reader.GetData(out data.curLevel, i, j++, "等级");
                reader.GetData(out data.curType, i, j++, "类型");
                reader.GetData(out data.moveSpeed, i, j++, "速度");

                if (m_MonsterDic.ContainsKey(data.monsterID))
                {
                    Debug.Log("MonsterCsv::OnSchemeLoad 存在相同的资源名称 monsterID=" + data.monsterID);
                    continue;
                }

                m_MonsterDic.Add(data.monsterID, data);
            }
        }
        catch (Exception e)
        {
            Debug.Log("MonsterCsv OnSchemeLoad Load Config error.. msg=" + e.Message);
            return false;
        }

        return true;
    }

    // 查找
    public MonsterCsvData Lookup(int Id)
    {
        MonsterCsvData monsterData;
        bool ret = m_MonsterDic.TryGetValue(Id, out monsterData);
        if (ret)
            return monsterData;
        return null;
    }
    public int LookUpType(int Id)
    {
        MonsterCsvData monsterData;
        bool ret = m_MonsterDic.TryGetValue(Id, out monsterData);
        if (ret)
            return monsterData.curType;
        return 0;
    }
    public uint LookUpLevel(int Id)
    {
        MonsterCsvData monsterData;
        bool ret = m_MonsterDic.TryGetValue(Id, out monsterData);
        if (ret)
            return (uint)monsterData.curLevel;
        return 0;
    }
    public string LookUpName(int Id)
    {
        MonsterCsvData monsterData;
        bool ret = m_MonsterDic.TryGetValue(Id, out monsterData);
        if (ret)
            return monsterData.monsterName;
        return null;
    }
    public string LookUpResID(int Id)
    {
        MonsterCsvData monsterData;
        bool ret = m_MonsterDic.TryGetValue(Id, out monsterData);
        if (ret)
            return monsterData.resourceID;
        return null;
    }
    public float LookUpMoveSpeed(int Id)
    {
        MonsterCsvData monsterData;
        bool ret = m_MonsterDic.TryGetValue(Id, out monsterData);
        if (ret)
        {
            return monsterData.moveSpeed;
        }
        return 0f;
    }
}
