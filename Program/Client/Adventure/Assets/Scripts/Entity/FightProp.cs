/*******************************************************************
** 文件名:	FightProp.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.2.1   18:23:28
** 版  本:	1.0
** 描  述:	战斗属性
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;

public delegate void PropFun(PropID propID, float value);
public class FightProp
{
    public static string[] s_PropDes = new string[(int)PropID.Max]
	{
		"无效值",
		"最大生命值",
		"最大能量值",
		"当前生命值",
		"当前能量值",
		"攻击力",
        "攻击力比例",
        "移动速度比例",
        "攻击速度比例",
	};
    private float[] m_nNumProp; // 属性

    private PropFun[] m_PropFuns;

    private Entity m_owner = null;

    public FightProp(Entity owner)
    {
        m_owner = owner;
        m_nNumProp = new float[(int)PropID.Max + 1];
        m_PropFuns = new PropFun[(int)PropID.Max + 1];
    }
    public bool Init()
    {
        InitPropFun();
        return CalFightProp();
    }
    public void InitPropFun()
    {
        m_PropFuns[(int)PropID.Invalid] = new PropFun(NoPropFun);					// 无效值
        m_PropFuns[(int)PropID.MaxHP] = new PropFun(Cover_NoNegative);				// 最大生命值
        m_PropFuns[(int)PropID.MaxEnergy] = new PropFun(Cover_NoNegative);			// 最大能量值
        m_PropFuns[(int)PropID.HP] = new PropFun(ChangeCurHp);						// 当前生命值
        m_PropFuns[(int)PropID.Energy] = new PropFun(ChangeCurEnergy);				// 当前能量值
        m_PropFuns[(int)PropID.Damage] = new PropFun(Increment_NoNegative);			// 物理攻击

        m_PropFuns[(int)PropID.DamageScale] = new PropFun(Cover_NoNegative);		// 攻击力比例
        m_PropFuns[(int)PropID.MovingSpeedScale] = new PropFun(Cover_NoNegative);	// 移动速度比例
        m_PropFuns[(int)PropID.SkillSpeedScale] = new PropFun(Cover_NoNegative);	// 攻击速度比例
    }
    public void SetProp(PropID nID, float value)
    {
        if (nID < PropID.Max)
        {
            if (m_PropFuns[(int)nID] == null)
            {
                return;
            }
            m_PropFuns[(int)nID](nID, value);
        }
    }
    public float GetProp(PropID nID)
    {
        return m_nNumProp[(int)nID];
    }
    public bool CalFightProp()
    {
        if (m_owner.EnType == EntityType.Monster)
        {
            int id = m_owner.ID;
            MonsterCsvData monster = GameMgr.Instance.m_csvMgr.mMonsterCsv.Lookup(id);
            if (monster == null)
            {
                Debug.LogWarning("FightProp::CalFightProp MonsterCsvData == null ID = " + id);
                return false;
            }

            SetProp(PropID.MaxHP, monster.curHP);
            SetProp(PropID.HP, monster.curHP);
            SetProp(PropID.MovingSpeedScale, monster.moveSpeed);

            int skillID = monster.skillID;
            SkillCsvData skill = GameMgr.Instance.m_csvMgr.mSkillCsv.Lookup(skillID);
            if (skill == null)
            {
                Debug.LogWarning("FightProp::CalFightProp SkillCsvData == null ID = " + id);
                return false;
            }

            SetProp(PropID.Damage, skill.Damage);
        }
        else if (m_owner.EnType == EntityType.Hero)
        {

        }

        return true;
    }
    private void NoPropFun(PropID propID, float value){}
    private void Cover_NoNegative(PropID propID, float value)
    {
        if (value < 0.0f)
        {
            m_nNumProp[(int)propID] = 0.0f;
        }
        else
        {
            m_nNumProp[(int)propID] = value;
        }
    }
    private void ChangeCurHp(PropID propID, float value)
    {

        float newValue = m_nNumProp[(int)PropID.HP] + value;

        if (newValue < COMMON_DEF.MIN_FLOAT)
        {
            newValue = 0.0f;
        }
        else if (newValue > m_nNumProp[(int)PropID.MaxHP])
        {
            newValue = m_nNumProp[(int)PropID.MaxHP];
        }

        m_nNumProp[(int)PropID.HP] = newValue;

    }
    private void ChangeCurEnergy(PropID propID, float value)
    {

        float newValue = m_nNumProp[(int)PropID.Energy] + value;

        if (newValue < COMMON_DEF.MIN_FLOAT)
        {
            newValue = 0.0f;
        }
        else if (newValue > m_nNumProp[(int)PropID.MaxEnergy])
        {
            newValue = m_nNumProp[(int)PropID.MaxEnergy];
        }

        m_nNumProp[(int)PropID.Energy] = newValue;
    }
    private void Increment_NoNegative(PropID propID, float value)
    {
        float newValue = m_nNumProp[(int)propID] + value;
        if (newValue < 0.0f)
        {
            m_nNumProp[(int)propID] = 0.0f;
        }
        else
        {
            m_nNumProp[(int)propID] = newValue;
        }
    }
}
