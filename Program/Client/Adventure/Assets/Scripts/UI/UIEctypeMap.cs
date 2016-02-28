/*******************************************************************
** 文件名:	UIEctypeMap.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.2.28   20:08:28
** 版  本:	1.0
** 描  述:	副本地图
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using UnityEngine;
using System.Collections;
public class UIEctypeMap : UIBase
{
    private CurChapterInfor m_curChapter;
    public CurChapterInfor CurChapter
    {
        get { return m_curChapter; }
        set
        {
            Show();

            m_curChapter = value;
            SetCurChater();
        }
    }
    protected override void Init()
    {
        GameObject Left = this.transform.FindChild("Left").gameObject;
        GameObject Right = this.transform.FindChild("Right").gameObject;
        UIEvent(Left).onClick = OnLeftEctype;
        UIEvent(Right).onClick = OnRightEctype;

    }
    private void OnLeftEctype(GameObject obj)
    {
        if (m_curChapter.chapter - 1 > 0)
        {
            HideCurChapter();
            m_curChapter.chapter -= 1;
            SetCurChater();
        }
    }
    private void OnRightEctype(GameObject obj)
    {
        int max = GameMgr.Instance.m_csvMgr.mLevelCsv.MaxChapterIndex();
        if (m_curChapter.chapter + 1 < max + 1)
        {
            HideCurChapter();
            m_curChapter.chapter += 1;
            SetCurChater();
        }
    }
    private void HideCurChapter()
    {
        GameObject obj = this.transform.GetChild(CurChapter.chapter - 1).gameObject;
        if (obj != null)
        {
            obj.SetActive(false);
        }
    }
    private void SetCurChater()
    {
        GameObject obj = this.transform.GetChild(CurChapter.chapter - 1).gameObject;
        if (obj != null)
        {
            obj.SetActive(true);

            UIEctypeChapter ectype = obj.GetComponent<UIEctypeChapter>();
            if (ectype == null)
            {
                ectype = obj.AddComponent<UIEctypeChapter>();
            }
            ectype.SetLevel(CurChapter);
        }
    }
}
