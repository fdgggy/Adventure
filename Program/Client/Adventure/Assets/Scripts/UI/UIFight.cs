/*******************************************************************
** 文件名:	UIFight.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.2.2   11:31:28
** 版  本:	1.0
** 描  述:	战斗UI
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIFight : UIBase
{
    private Text m_tAttackName;
    private Text m_tDeffendName;

    private UIBlood m_Attacker;
    private UIBlood m_Deffend;
    protected override void Init()
    {
        Transform attackerName = this.transform.Find("Attacker/Name/TxtInfor");
        if (attackerName != null)
        {
            m_tAttackName = attackerName.GetComponent<Text>();
        }
        Transform attackerBlood = this.transform.Find("Attacker/Blood");
        m_Attacker = UIBase.Get<UIBlood>(attackerBlood);

        Transform deffendName = this.transform.Find("Deffender/Name/TxtInfor");
        if (deffendName != null)
        {
            m_tDeffendName = deffendName.GetComponent<Text>();
        }
        Transform deffendBlood = this.transform.Find("Deffender/Blood");
        m_Deffend = UIBase.Get<UIBlood>(deffendBlood);
    }
    public bool CreateAttacker(Entity attacker)
    {
        if (attacker == null)
        {
            return false;
        }
      
        Show();
        
        m_tAttackName.text = attacker.Name;
        m_Attacker.Owner = attacker;
        m_Attacker.SetCurHpValue(attacker.GetProp(PropID.MaxHP));
        
        return true;
    }
    public bool CreateDeffender(Entity deffender)
    {
        if (deffender == null)
        {
            return false;
        }
        
        Show();
        
        m_tDeffendName.text = deffender.Name;
        m_Deffend.Owner = deffender;
        m_Deffend.SetCurHpValue(deffender.GetProp(PropID.MaxHP));

        return true;
    }
}
