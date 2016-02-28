/*******************************************************************
** 文件名:	LevelCsv.cs
** 版  权:	(C)  2016 - Adventure
** 创建人:	周健
** 日  期:	2016.2.28   19:30:28
** 版  本:	1.0
** 描  述:	章节
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
public class LevelCsvData
{
    public int levelID;             
    public int chapterID;           
    public int levelNumber;
    public int easyEctypeID;            //简单副本ID
    public int hardEctypeID;            //困难副本ID
    public int eliteEctypeID;           //精英副本ID
}
public class LevelCsv : ISchemeUpdateSink
{
    List<LevelCsvData> levelDic; 
    private Dictionary<int, List<LevelCsvData>> m_ChapterDic;
    public LevelCsv()
    {
        levelDic = new List<LevelCsvData>();
        m_ChapterDic = new Dictionary<int, List<LevelCsvData>>();
    }
    private bool m_bIsLoaded = false;
    public bool mIsLoaded { get { return m_bIsLoaded; } set { m_bIsLoaded = value; } }

    public bool OnSchemeLoad(ICsvReader reader)
    {
        int nRowCount = reader.GetRowCount();
        m_ChapterDic.Clear();
        try
        {
            int chapter = 1;
            for (int i = 0; i < nRowCount; i++)
            {
                int j = 0;

                int iChapter = 0;
                reader.GetData(out iChapter, i, 1, "所属章节");
                
                if (chapter != iChapter)
                {
                    m_ChapterDic.Add(chapter, levelDic);
                    chapter = iChapter;
                    levelDic = new List<LevelCsvData>();
                }

                LevelCsvData data = new LevelCsvData();
                reader.GetData(out data.levelID, i, j++, "关卡ID");
                reader.GetData(out data.chapterID, i, j++, "所属章节");
                reader.GetData(out data.levelNumber, i, j++, "关卡序号");
                reader.GetData(out data.easyEctypeID, i, j++, "简单副本ID");
                reader.GetData(out data.hardEctypeID, i, j++, "困难副本ID");
                reader.GetData(out data.eliteEctypeID, i, j++, "精英副本ID");
                levelDic.Add(data);

                if (i == nRowCount - 1)
                {
                    m_ChapterDic.Add(chapter, levelDic);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log("LevelCsv OnSchemeLoad Load Config error.. msg=" + e.Message);
            return false;
        }

        return true;
    }

    public List<LevelCsvData> Lookup(int chapterID)
    {
        List<LevelCsvData> levels = new List<LevelCsvData>();
        bool ret = m_ChapterDic.TryGetValue(chapterID, out levels);
        if (ret) 
            return levels;
        return null;
    }
    
    public int MaxChapterIndex()
    {
        return m_ChapterDic.Count;
    }
}
