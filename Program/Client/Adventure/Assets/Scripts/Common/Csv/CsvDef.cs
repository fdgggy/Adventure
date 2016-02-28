/*******************************************************************
** 文件名:	CsvDef.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.20   10:13:28
** 版  本:	1.0
** 描  述:	配置表读取
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/
public interface ICsvReader
{
	string GetFileName();
	int GetRowCount();
	int GetColCount();
	
	bool GetData(out bool data, int nRowIndex, int nColIndex,string name = "");
	bool GetData(out int data, int nRowIndex, int nColIndex, string name = "");
	bool GetData(out long data, int nRowIndex, int nColIndex, string name = "");
	bool GetData(out float data, int nRowIndex, int nColIndex, string name = "");
	bool GetData(out string data, int nRowIndex, int nColIndex, string name = "");
}

/// <summary>
/// 配置加载回调接口
/// </summary>
public interface ISchemeUpdateSink
{
	/// <summary>
	/// 配置加载回调函数
	/// </summary>
	bool OnSchemeLoad(ICsvReader reader);
	
	/// <summary>
	/// 是否加载完成
	/// </summary>
	bool mIsLoaded { get; set; }
}
