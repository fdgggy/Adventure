/*******************************************************************
** 文件名:	UILogin.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.1.16   17:55:28
** 版  本:	1.0
** 描  述:	UI登录界面
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
public class UILogin : UIBase
{
    private Transform m_transLogo;
    private Transform m_startBtn;

    protected override void Init()
    {
        m_transLogo = transform.Find("BackGround");

        m_startBtn = transform.Find("StartGame");
        UIEvent(m_startBtn.gameObject).onClick = OnEnterGame;
        m_startBtn.gameObject.SetActive(false);
    }
    private void OnDestory()
    {
        UIEvent(m_startBtn.gameObject).onClick -= OnEnterGame;
    }
    public void Play()
    {
        Show();
        Fadein(m_transLogo.gameObject, OnLogoFadein, 1.5f);
    }
    private void OnLogoFadein()
    {
        m_startBtn.gameObject.SetActive(true);
        GameObject obj = m_startBtn.Find("Image").gameObject;
        FadePingPong(obj);
    }
    public void OnEnterGame(GameObject go)
    {
        Fadeout(m_transLogo.gameObject, OnLogoFadeout, 1.5f);
    }
    private void OnLogoFadeout()
    {
        EnterEctypeContext ctx = new EnterEctypeContext();
        ctx.m_EctypeCreateData.type = EctypeUseType.Normal;
        ctx.m_EctypeCreateData.dwEctypeID = 101;
        ctx.m_EctypeCreateData.bGuide = false;
        ctx.m_EctypeCreateData.sceneID = "BattleScene";
        GameMgr.Instance.m_clientMgr.EnterEctype(ctx);

        this.gameObject.SetActive(false);
    }
}
