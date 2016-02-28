/*******************************************************************
** 文件名:	EctypeMonsterMgr.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.1.21   13:07:28
** 版  本:	1.0
** 描  述:	副本怪物管理
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
public class EctypeMonsterMgr
{
    private Dictionary<int, EctypeMonsterCsv> m_monsterCsvs;
    public Dictionary<int, EctypeMonsterCsv> MonsterCsvs
    {
        get { return m_monsterCsvs; }
    }
    public EctypeMonsterMgr()
    {
        m_monsterCsvs = new Dictionary<int, EctypeMonsterCsv>();
    }
    public EctypeMonsterCsv Lookup(int ID)
    {
        if (m_monsterCsvs.ContainsKey(ID))
        {
            return m_monsterCsvs[ID];
        }

        EctypeMonsterCsv monster = new EctypeMonsterCsv();
        string path = string.Format("Scp/Monster/{0}", ID);

        GameMgr.Instance.m_csvMgr.LoadSchemeByResPath(path, monster);
        if (monster.mIsLoaded)
        {
            m_monsterCsvs.Add(ID, monster);
            return m_monsterCsvs[ID];
        }

        return null;
    }
}
