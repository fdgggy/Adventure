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
using System.Collections.Generic;
using UnityEngine.UI;

public struct CurChapterInfor
{
    public int chapter;
    public int diffDegree;      //0,简单 1,困难 2,精英
    public int level;
}
public class UIEctypeChapter : UIBase
{
    private Text m_tChapter;
    protected override void Init()
    {
        Transform trans = this.transform.GetChild(transform.childCount - 1);
        if (trans != null)
        {
            m_tChapter = trans.GetComponent<Text>();
        }
    }

    public void SetLevel(CurChapterInfor curChapter)
    {
        m_tChapter.text = string.Format("第 {0} 章", curChapter.chapter);

        List<LevelCsvData> leves = GameMgr.Instance.m_csvMgr.mLevelCsv.Lookup(curChapter.chapter);
        
        for (int i = 0; i < leves.Count; i++)
        {
            GameObject obj = this.transform.GetChild(i).gameObject;
            UIlevelInfor infor = obj.GetComponent<UIlevelInfor>();
            if (infor == null)
            {
                infor = obj.AddComponent<UIlevelInfor>();
            }

            int ectypeId = 0;
            if (curChapter.diffDegree == 0)
            {
                ectypeId = leves[i].easyEctypeID;
            }
            else if (curChapter.diffDegree == 1)
            {
                ectypeId = leves[i].hardEctypeID;
            }
            else if (curChapter.diffDegree == 2)
            {
                ectypeId = leves[i].eliteEctypeID;
            }

            LevelInfor levelInfor = new LevelInfor
            {
                chapterID = leves[i].chapterID,
                levelID = leves[i].levelNumber,
                useType = EctypeUseType.Normal,
                ectypeID = (uint)ectypeId,
                bGuide = false,
                sceneID = "BattleScene"
            };

            infor.LevelInfor = levelInfor;
        }

       
    }
}
