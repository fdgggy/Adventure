/*******************************************************************
** 文件名:	EctypeTeam.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.28   22:19:28
** 版  本:	1.0
** 描  述:	副本TEAM
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EctypeTeam
{
    protected TeamStatus status { get; set; }   // 队伍状态
    public List<EctypeEntity> m_entityList;     // 战斗成员列表
    protected Ectype m_Ectype;                  // 所属副本对象
    protected EntityCamp m_Camp;                // 队伍阵营

    public int[] counterMax = new int[(int)EntityEctypeStatus.Max];
    private bool m_skillTurn = false;
    public bool SkillTurn 
    {
        get { return m_skillTurn; }
        set 
        { 
            m_skillTurn = value; 
            if (m_skillTurn)
            {
                DoAttack(); 
            }
        }
    }
    public EctypeTeam(Ectype ectype)
    {
        m_Ectype = ectype;
        m_entityList = new List<EctypeEntity>();

        counterMax[(int)EntityEctypeStatus.Alert] = 10;
        counterMax[(int)EntityEctypeStatus.Chase] = 5;
        counterMax[(int)EntityEctypeStatus.Combat] = 10;
    }
    public virtual void DoAttack() { }
    public virtual void OnUpdateEntity()
    {
    }
    public EctypeEntity GetNearestTarget(Entity own)
    {
        EctypeEntity retEntity = null;
        float minDis = 9999999.0f;

        for (int i = 0; i < m_entityList.Count; i++)
        {
            EctypeEntity ectypeEntity = m_entityList[i];
            if (ectypeEntity == null || ectypeEntity.entity == null || ectypeEntity.entity.IsDead())
            {
                continue;
            }

            float dis = Vector3.Distance(own.Position, ectypeEntity.entity.Position);
            if (dis < minDis)
            {
                retEntity = ectypeEntity;
                minDis = dis;
            }
        }

        return retEntity;
    }
}
