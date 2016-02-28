/*******************************************************************
** 文件名:	EntityMgr.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   13:37:28
** 版  本:	1.0
** 描  述:	技能CSV
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillCsvData
{
    public int Id;
    public string Name;
    public int Level;
    public int Type;
    public int CdTime;
    public int fireEffID;
    public int hitEffID;
    public int Damage;
}
public class SkillCsv : ISchemeUpdateSink
{
    private Dictionary<int, SkillCsvData> m_SkillDic;

    public SkillCsv()
    {
        m_SkillDic = new Dictionary<int, SkillCsvData>();
    }
    private bool m_bIsLoaded = false;
    public bool mIsLoaded { get { return m_bIsLoaded; } set { m_bIsLoaded = value; } }
    public void Clear() { m_SkillDic.Clear(); }
    public bool OnSchemeLoad(ICsvReader reader)
    {
        int nRowCount = reader.GetRowCount();
        m_SkillDic.Clear();
        try
        {
            for (int i = 0; i < nRowCount; i++)
            {
                int j = 0;
                SkillCsvData data = new SkillCsvData();

                reader.GetData(out data.Id, i, j++, "技能ID");
                reader.GetData(out data.Name, i, j++, "技能名称");
                reader.GetData(out data.Level, i, j++, "技能等级");
                reader.GetData(out data.Type, i, j++, "技能类型");
                reader.GetData(out data.CdTime, i, j++, "冷却时间");
                reader.GetData(out data.fireEffID, i, j++, "发射效果Id");
                reader.GetData(out data.hitEffID, i, j++, "受击效果Id");
                reader.GetData(out data.Damage, i, j++, "秒伤");

                if (m_SkillDic.ContainsKey(data.Id))
                {
                    Debug.Log("SkillCsv::OnSchemeLoad 存在相同的名称 skillID=" + data.Id);
                    continue;
                }

                m_SkillDic.Add(data.Id, data);
            }
        }
        catch (Exception e)
        {
            Debug.Log("SkillCsv OnSchemeLoad Load Config error.. msg=" + e.Message);
            return false;
        }

        return true;
    }
    // 查找
    public SkillCsvData Lookup(int skillID)
    {
        SkillCsvData skillData;
        bool ret = m_SkillDic.TryGetValue(skillID, out skillData);
        if (ret)
            return skillData;
        return null;
    }
}
