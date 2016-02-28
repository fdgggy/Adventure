/*******************************************************************
** 文件名:	ResourceCsv.cs
** 版  权:	(C)  2015 - Adventure
** 创建人:	周健
** 日  期:	2015.12.20   10:38:28
** 版  本:	1.0
** 描  述:	Csv读取
** 应  用:  

**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
********************************************************************/

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// 列信息
/// </summary>
class ColInfo
{
	//public int index;        //索引
	public string dataType;  //数据类型
	public string name;      //数据名称
}

public class CsvReader : ICsvReader
{
    // 逗号分割
    private readonly char[] STRING_SPLIT = { ',' };
    // 换行分割
    private readonly string[] FILE_SPLIT = { "\r\n" };

    // 实际数据行数
    private int m_nMaxRowCount = 0;

    // 数据描述数组
    private string[] m_aryHead = null;

    // 数据类型数组
    private string[] m_aryType = null;

    // 数据表
    private ArrayList m_DataTable = null;

	private string m_filePath = null;

	// 实际数据行数
	private int m_nReadingLine = 0;

	//列信息
	private Dictionary<int, ColInfo> m_ColInfo = new Dictionary<int, ColInfo>();

	public string GetFileName()
	{
		return m_filePath;
	}

    /// <summary>
    /// 根据数据内存加载数据
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool LoadByData(string filename,byte[] data)
    {
        int nReadIndex = 0;
        string szLine = "";
        m_filePath = filename;
        Stream mstream = new MemoryStream(data);
        // 创建流读取器，读取数据
        StreamReader reader = new StreamReader(mstream);

        while ((szLine = reader.ReadLine()) != null)
        {
            if (szLine == null || szLine.Length <= 0)
            {
                Debug.LogError("CCSVReader::LoadByContent--此行数据为空，行号 = " + nReadIndex);
                continue;
            }

            if (0 == nReadIndex)        // 数据描述
            {
                if (!SetDesc(szLine))
                {
                    reader.Close();
                    reader.Dispose();

                    return false;
                }
            }
            else if (1 == nReadIndex)   // 数据类型
            {
                if (!SetType(szLine))
                {
                    reader.Close();
                    reader.Dispose();

                    return false;
                }
            }
            else                        //  配置数据
            {
                if (!SetData(szLine, nReadIndex))
                {
                    reader.Close();
                    reader.Dispose();

                    return false;
                }
            }

            ++nReadIndex;
        }

        reader.Close();
        reader.Dispose();

        return true;
    }

    /// <summary>
    /// 根据文件路径加载配置
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>返回加载成功与否</returns>
    public bool LoadByPath(string filePath)
    {
        // 读取配置的时候，前两行不需要读取
        // 第一行是数据描述，第二行是数据类型
        // 数据1  数据2   数据3   数据4     数据5
        // INT   FLOAT   INT64   STRING    BOOL
        int nReadIndex = 0;
        string szLine = "";
		m_filePath = filePath;
        // 创建流读取器，读取数据
        StreamReader reader = new StreamReader(filePath);

        while ((szLine = reader.ReadLine()) != null)
        {
            if (szLine == null || szLine.Length <= 0)
            {
                Debug.LogError("CCSVReader::LoadByContent--此行数据为空，行号 = " + nReadIndex);
                continue;
            }

            if (0 == nReadIndex)        // 数据描述
            {
                if (!SetDesc(szLine))
                {
                    reader.Close();
                    reader.Dispose();

                    return false;
                }
            }
            else if (1 == nReadIndex)   // 数据类型
            {
                if (!SetType(szLine))
                {
                    reader.Close();
                    reader.Dispose();

                    return false;
                }
            }
            else                        //  配置数据
            {
                if (!SetData(szLine))
                {
                    reader.Close();
                    reader.Dispose();

                    return false;
                }
            }

            ++nReadIndex;
        }

        reader.Close();
        reader.Dispose();

        return true;
    }

    /// <summary>
    /// 根据文件内容加载配置
    /// </summary>
    /// <param name="fileContent">文件内容</param>
    /// <returns>返回加载成功与否</returns>
    public bool LoadByContent(string filePath,string fileContent)
    {
        // 读取配置的时候，前两行不需要读取
        // 第一行是数据描述，第二行是数据类型
        // 数据1  数据2   数据3   数据4     数据5
        // INT   FLOAT   INT64   STRING    BOOL

        // 分割字符串
        string[] contexts = fileContent.Split(FILE_SPLIT, StringSplitOptions.RemoveEmptyEntries);

		m_filePath = filePath;

        for (int line = 0; line < contexts.Length; line++)
        {
            if (contexts[line] == null || contexts[line].Length <= 0)
            {
                Debug.LogError("CCSVReader::LoadByContent--此行数据为空，行号 = " + line);
                continue;
            }

            if (0 == line)                              // 数据描述
            {
                if (!SetDesc(contexts[line]))
                {
                    return false;
                }
            }
            else if (1 == line)                         // 数据类型
            {
                if (!SetType(contexts[line]))
                {
                    return false;
                }
            }
            else                                        // 配置数据
            {
                if (!SetData(contexts[line]))
                {
                    return false;
                }
            }
        }

        return true;
    }

    // 设置数据描述
    private bool SetDesc(string head)
    {
        m_aryHead = head.Split(STRING_SPLIT);

        if (m_aryHead.Length <= 0)
        {
            Debug.LogError("CCSVReader::SetDesc--数据描述为空！！！");
            return false;
        }

        return true;
    }

    // 设置数据类型
    private bool SetType(string type)
    {
        m_aryType = type.Split(STRING_SPLIT);
        if (m_aryType.Length != m_aryHead.Length)
        {
            Debug.LogError("CCSVReader::SetType--数据描述和数据类型的列数不相同" + m_filePath);
            return false;
        }

        m_nMaxRowCount = 0;
        m_DataTable = new ArrayList();

        for (int i = 0; i < m_aryType.Length; i++)
        {
            // 统一转成大写
            string dataType = m_aryType[i].ToString().ToUpper().Trim();
            m_aryType[i] = dataType;

            if (dataType == "BOOL")      // bool值
            {
                List<bool> dataList = new List<bool>();
                m_DataTable.Add(dataList);
            }
            else if (dataType == "INT")      // int值
            {
                List<int> dataList = new List<int>();
                m_DataTable.Add(dataList);
            }
            else if (dataType == "INT64")      // int64值
            {
                List<long> dataList = new List<long>();
                m_DataTable.Add(dataList);
            }
            else if (dataType == "FLOAT")      // float值
            {
                List<float> dataList = new List<float>();
                m_DataTable.Add(dataList);
            }
            else   // if (0 == dataType.CompareTo("STRING")) 未知类型和string值统一做string处理
            {
                List<string> dataList = new List<string>();
                m_DataTable.Add(dataList);
            }
        }

        return true;
    }

    // 设置一行数据
    private bool SetData(string data,int lineIndex = 0)
    {
        string[] aryData = data.Split(STRING_SPLIT);
        if (aryData.Length != m_aryType.Length)
        {
            Debug.LogError("CCSVReader::SetData--此行数据列数和数据类型的列数不相同");
            return false;
        }

        for (int i = 0; i < m_aryType.Length; i++)
        {
            if (m_aryType[i] == "BOOL")      // bool值
            {
                List<bool> dataList = m_DataTable[i] as List<bool>;

                //if (aryData[i].Length <= 0)
                if (String.IsNullOrEmpty(aryData[i]))
                    dataList.Add(false);
                else
                {
                    try
                    {
                        dataList.Add(Convert.ToBoolean(aryData[i]));
                    }
                    catch
                    {
                        dataList.Add(false);
                        Debug.LogError("SchemeEngine::SetData Error Convert.ToBoolean failed" + ",filename=" + m_filePath + ",line=" + lineIndex + ",Column=" + i);
                    }
                }
            }
            else if (m_aryType[i] == "INT")      // int值
            {
                List<int> dataList = m_DataTable[i] as List<int>;

                //if (aryData[i].Length <= 0)
                if (String.IsNullOrEmpty(aryData[i]))
                    dataList.Add(0);
                else
                {
                    try
                    {
                        dataList.Add(Convert.ToInt32(aryData[i]));
                    }
                    catch
                    {
                        dataList.Add(0);
                        Debug.LogError("SchemeEngine::SetData Error Convert.ToInt32 failed" + ",filename=" + m_filePath + ",line=" + lineIndex + ",Column="+i);
                    }
                }
            }
            else if (m_aryType[i] == "INT64")      // int64值
            {
                List<long> dataList = m_DataTable[i] as List<long>;
                //if (aryData[i].Length <= 0)
                if (String.IsNullOrEmpty(aryData[i]))
                    dataList.Add(0);
                else
                {
                    try
                    {
                        dataList.Add(Convert.ToInt64(aryData[i]));
                    }
                    catch
                    {
                        dataList.Add(0);
                        Debug.LogError("SchemeEngine::SetData Error Convert.ToInt64 failed" + ",filename=" + m_filePath + ",line=" + lineIndex + ",Column=" + i);
                    }

                }
            }
            else if (m_aryType[i] == "FLOAT")      // float值
            {
                List<float> dataList = m_DataTable[i] as List<float>;

                //if (aryData[i].Length <= 0)
                if (String.IsNullOrEmpty(aryData[i]))
                    dataList.Add(0.0f);
                else
                {
                    try
                    {
                        dataList.Add(Convert.ToSingle(aryData[i]));
                    }
                    catch
                    {
                        dataList.Add(0);
                        Debug.LogError("SchemeEngine::SetData Error Convert.ToSingle failed" + ",filename=" + m_filePath + ",line=" + lineIndex + ",Column=" + i);
                    }
                }
            }
            else   // if (0 == dataType.CompareTo("STRING")) 未知类型和string值统一做string处理
            {
                List<string> dataList = m_DataTable[i] as List<string>;
                dataList.Add(aryData[i]);
            }
        }

        m_nMaxRowCount++;

        return true;
    }

    /******************************实现CSV读取器接口方法****************************/
    public int GetRowCount()
    {
        return m_nMaxRowCount;
    }

    public int GetColCount()
    {
        return m_aryType.Length;
    }

    public bool GetData(out bool data, int nRowIndex, int nColIndex,string name = "")
    {
        if (nRowIndex >= m_nMaxRowCount || nColIndex >= m_aryType.Length)
        {
            data = false;
            return false;
        }

        List<bool> dataList = m_DataTable[nColIndex] as List<bool>;
		data = dataList[nRowIndex];

		// 校验
		AddColCheckInfo("bool", nRowIndex, nColIndex, name);

        return true;
    }

	public bool GetData(out int data, int nRowIndex, int nColIndex, string name = "")
    {
        if (nRowIndex >= m_nMaxRowCount || nColIndex >= m_aryType.Length)
        {
            data = 0;
            return false;
        }

        List<int> dataList = m_DataTable[nColIndex] as List<int>;
		data = dataList[nRowIndex];

		// 校验
		AddColCheckInfo("int", nRowIndex, nColIndex, name);

        return true;
    }

	public bool GetData(out long data, int nRowIndex, int nColIndex, string name = "")
    {
        if (nRowIndex >= m_nMaxRowCount || nColIndex >= m_aryType.Length)
        {
            data = 0;
            return false;
        }

        List<long> dataList = m_DataTable[nColIndex] as List<long>;
		data = dataList[nRowIndex];

		// 校验
		AddColCheckInfo("long", nRowIndex, nColIndex, name);

        return true;
    }

	public bool GetData(out float data, int nRowIndex, int nColIndex, string name = "")
    {
        if (nRowIndex >= m_nMaxRowCount || nColIndex >= m_aryType.Length)
        {
            data = 0.00f;
            return false;
        }

        List<float> dataList = m_DataTable[nColIndex] as List<float>;
        data = dataList[nRowIndex];

		// 校验
		AddColCheckInfo("float", nRowIndex, nColIndex, name);

        return true;
    }

	public bool GetData(out string data, int nRowIndex, int nColIndex, string name = "")
    {
        if (nRowIndex >= m_nMaxRowCount || nColIndex >= m_aryType.Length)
        {
            data = "";
            return false;
        }

        List<string> dataList = m_DataTable[nColIndex] as List<string>;
        data = dataList[nRowIndex];

		// 校验
		AddColCheckInfo("string", nRowIndex, nColIndex, name);

        return true;
    }

	private void AddColCheckInfo(string typename, int nRowIndex, int nColIndex, string name = "")
	{
		if (nRowIndex == 0)
		{
			ColInfo info = new ColInfo();
			info.dataType = typename;
			info.name = name;
			m_ColInfo[nColIndex] = info;
			m_nReadingLine = 1;
		}
		else if (m_nReadingLine==1 && nRowIndex == 1)
		{
			m_nReadingLine = 0;

			//TRACE.TraceLn("文件名:" + m_filePath);
			int nNoNameCount = 0;
			for (int i = 0; i < m_aryHead.Length; i++)
			{
				string strName = m_aryHead[i];
				if (strName.Length <= 0)
					nNoNameCount++;
				string strType = m_aryType[i];
				if (i < m_aryType.Length)
				{
					strType = m_aryType[i];
				}
				string strCheckName = "";
				string strCheckType = "";
				if (m_ColInfo.ContainsKey(i))
				{
					strCheckName = m_ColInfo[i].name;
					strCheckType = m_ColInfo[i].dataType;
					//TRACE.TraceLn(">" + i + "[" + strName + ":" + strType + "] = [" + strCheckName + ":" + strCheckType + "]");
				}
			}
			if (nNoNameCount > 1)
			{
				Debug.LogError("严重错误!有" + nNoNameCount + "个列名无效，编码格式不是UTF8，文件名:" + m_filePath);
			}
		}

	}
    /**********************************************************************/
}
