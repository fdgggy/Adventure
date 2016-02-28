/*******************************************************************
** 文件名:	GameUtil.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.20   10:57:28
** 版  本:	1.0
** 描  述:	游戏杂项
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
using System;
using UnityEngine;
public static class GameUtil
{
	/// <summary>
	/// 是否是移动端
	/// </summary>
	public static bool IsMobilePlatform()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ||
		    Application.platform == RuntimePlatform.WP8Player || Application.platform == RuntimePlatform.BlackBerryPlayer)
		{
			return true;
		}

		return false;
	
	}
    public static string GetUserRoot()
    {
        string userRoot;
        if (IsMobilePlatform())
        {
            userRoot = Application.persistentDataPath + "/User/";
        }
        else
        {
            userRoot = Application.dataPath + "/User/";
        }

        try
        {
            if (!System.IO.Directory.Exists(userRoot))
            {
                System.IO.Directory.CreateDirectory(userRoot);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("GameUtil::GetUserRoot--创建用户根目录失败！ " + e.Message);
        }
        return userRoot;
    }
    /// <summary>
    /// 获取StreamingAssets路径
    /// </summary>
    /// <returns></returns>
    public static string GetStreamingAssetsPath()
    {
        string path;
#if UNITY_ANDROID  
		path = "jar:file://" + Application.dataPath + "!/assets/";  
#elif UNITY_IPHONE  
		path = Application.dataPath + "/Raw/";  
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        path = "file://" + Application.dataPath + "/StreamingAssets/";
#else  
		path = string.Empty;  
#endif
        return path;
    }

    public static string GetSceneAssetsPath()
    {
        return GetStreamingAssetsPath() + "scene/" + GetSceneAssetSubFolderName() + "/";
    }

    public static string GetSceneAssetSubFolderName()
    {
        string folderName;
#if UNITY_ANDROID  
		folderName = "Andriod";
#elif UNITY_IPHONE  
		folderName = "Iphone";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        folderName = "Windows";
#else  
		path = "Mac";
#endif
        return folderName;
    }
}

