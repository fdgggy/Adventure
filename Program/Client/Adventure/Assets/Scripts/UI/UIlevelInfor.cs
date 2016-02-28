/*******************************************************************
** 文件名:	UIlevelInfor.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.2.28   18:12:28
** 版  本:	1.0
** 描  述:	关卡UI
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using UnityEngine.UI;
public struct LevelInfor
{
    public int chapterID;           //所属章节
    public int levelID;             //所属关卡
    public EctypeUseType useType;   //类型
    public uint ectypeID;            //副本ID
    public bool bGuide;             //是否引导副本
    public string sceneID;          //场景ID
}
public class UIlevelInfor : UIBase
{
    private Text m_tlevel;
    private LevelInfor m_levelInfor;
    public LevelInfor LevelInfor
    {
        get { return m_levelInfor; }
        set { m_levelInfor = value; SetInfor(); }
    }

    private void SetInfor()
    {
        m_tlevel.text = string.Format("{0}-{1}", LevelInfor.chapterID, LevelInfor.levelID);
    }
    protected override void Init()
    {
        Transform trans = this.transform.FindChild("infor");
        if (trans != null)
        {
            m_tlevel = trans.GetComponent<Text>();
        }

        UIEvent(this.gameObject).onClick = OnEnterEctype;
    }
    public void OnEnterEctype(GameObject go)
    {
        EnterEctypeContext ctx = new EnterEctypeContext();
        ctx.m_EctypeCreateData.type = LevelInfor.useType;
        ctx.m_EctypeCreateData.dwEctypeID = LevelInfor.ectypeID;
        ctx.m_EctypeCreateData.bGuide = LevelInfor.bGuide;
        ctx.m_EctypeCreateData.sceneID = LevelInfor.sceneID;
        GameMgr.Instance.m_clientMgr.EnterEctype(ctx);

        GameObject obj = GameMgr.Instance.m_uiMgr.GetActive();
        if (obj)
        {
            obj.SetActive(false);
        }
    }
}
