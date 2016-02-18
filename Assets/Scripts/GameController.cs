/*
 * Author :  well pan
 * date : 2016/2/10
 */

using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;

public class GameController : MonoBehaviour
{
    //
    public int rowNum;
    public int columnNum;
    public Candy candy;
    public Transform panel_candys;
    public float followSpeed = 0.5f;

    private ArrayList candyArr; //存放CandyArray的数组，二维数组

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        candyArr = new ArrayList();

        for (int rowIndex = 0; rowIndex < rowNum; rowIndex++)
        {
            ArrayList tmpArr = new ArrayList();
            for (int columnIndex = 0; columnIndex < columnNum; columnIndex++)
            {
                Candy theCandy = AddCandy(rowIndex, columnIndex);

                tmpArr.Add(theCandy);
            }
            candyArr.Add(tmpArr);
        }

    }

    /// <summary>
    /// 创建Candy
    /// </summary>
    private Candy AddCandy(int rowIndex, int columnIndex)
    {
        Object obj = Instantiate(candy);
        Candy theCandy = obj as Candy;

        //Set the candy position
        theCandy.transform.parent = panel_candys.transform;
        theCandy.transform.localScale = Vector3.one;
        theCandy.columnIndex = columnIndex;
        theCandy.rowIndex = rowIndex;
        theCandy.UpdatePosition();

        theCandy.gameController = this;

        return theCandy;
    }

    /// <summary>
    /// 获取二维数组元素
    /// </summary>
    /// <param name="rowIndex">一维索引</param>
    /// <param name="columnIndex">二维索引</param>
    /// <returns></returns>
    private Candy GetCandy(int rowIndex, int columnIndex)
    {
        //Debug.Log("In the ArryList, rowIndex:" + rowIndex + ",,,columnIndex :" + columnIndex);
        ArrayList tmpArr = candyArr[rowIndex] as ArrayList;
        Candy c = tmpArr[columnIndex] as Candy;

        return c;
    }

    /// <summary>
    /// 添加二维数组元素
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <param name="columnIndex"></param>
    /// <param name="c"></param>
    private void SetCandy(int rowIndex, int columnIndex, Candy c)
    {
        ArrayList tmpArr = candyArr[rowIndex] as ArrayList;
        tmpArr[columnIndex] = c;
    }

    private Candy crtCandy;     //标记Candy
    /// <summary>
    /// 选择Candy
    /// </summary>
    /// <param name="c">第一次选中的Candy</param>
    public void Select(Candy c)
    {
        //Remove(c); return;

        if (crtCandy == null)
        {
            crtCandy = c;
        }
        else
        {
            Exchange(crtCandy, c);
            crtCandy = null;

            CheckMatches();
            RemoveMatches();
        }
    }

    /// <summary>
    /// 交换Candy
    /// </summary>
    /// <param name="c1">选择1</param>
    /// <param name="c2">选择2</param>
    private void Exchange(Candy c1, Candy c2)
    {
        //Change arrylist
        SetCandy(c1.rowIndex, c1.columnIndex, c2);
        SetCandy(c2.rowIndex, c2.columnIndex, c1);

        int rowIndex = c1.rowIndex;
        c1.rowIndex = c2.rowIndex;
        c2.rowIndex = rowIndex;

        int columnIndex = c1.columnIndex;
        c1.columnIndex = c2.columnIndex;
        c2.columnIndex = columnIndex;
        //Update c1,c2 position
        c1.UpdatePosition();
        c2.UpdatePosition();


    }

    //移除Candy
    private void Remove(Candy c)
    {
        //remove candy
        c.Dispose();

        //candy move down
        int columnIndex = c.columnIndex;
        for (int rowIndex = c.rowIndex + 1; rowIndex < rowNum; rowIndex++)
        {
            Candy c2 = GetCandy(rowIndex, columnIndex);
            c2.rowIndex--;
            c2.TweenPosition(followSpeed);
            SetCandy(rowIndex - 1, columnIndex, c2);
        }

        //Add new candy
        Candy newCandy = AddCandy(rowNum, columnIndex);
        newCandy.UpdatePosition();
        newCandy.rowIndex--;
        newCandy.TweenPosition(followSpeed);
        SetCandy(rowNum - 1, columnIndex, newCandy);
    }

    /// <summary>
    /// 检测有无消除
    /// </summary>
    /// <returns></returns>
    private bool CheckMatches()
    {
        CheckHorizontalMatches();
        CheckVerticalMatches();
        return false;
    }

    //检测水平方向有无消除
    private bool CheckHorizontalMatches()
    {
        for (int rowIndex = rowNum - 1; rowIndex >= 0; rowIndex--)
        {
            for (int columnIndex = 0; columnIndex < columnNum - 2; columnIndex++)
            {
                if (GetCandy(rowIndex, columnIndex).typeName == GetCandy(rowIndex, columnIndex + 1).typeName &&
                    GetCandy(rowIndex, columnIndex).typeName == GetCandy(rowIndex, columnIndex + 2).typeName)
                {
                    Debug.Log(rowIndex + " " + columnIndex + " " + columnIndex + 1 + " " + columnIndex + 2);
                    AddMatches(GetCandy(rowIndex, columnIndex));
                    AddMatches(GetCandy(rowIndex, columnIndex + 1));
                    AddMatches(GetCandy(rowIndex, columnIndex + 2));
                }
            }
        }

        return false;
    }

    //检测垂直方向有无消除
    private bool CheckVerticalMatches()
    {
        for (int columnIndex = columnNum - 1; columnIndex >= 0; columnIndex--)
        {
            for (int rowIndex = 0; rowIndex < rowNum - 2; rowIndex++)
            {
                if (GetCandy(rowIndex, columnIndex).typeName == GetCandy(rowIndex + 1, columnIndex).typeName &&
                    GetCandy(rowIndex, columnIndex).typeName == GetCandy(rowIndex + 2, columnIndex).typeName)
                {
                    Debug.Log(rowIndex + " " + columnIndex + " " + columnIndex + 1 + " " + columnIndex + 2);
                    AddMatches(GetCandy(rowIndex, columnIndex));
                    AddMatches(GetCandy(rowIndex + 1, columnIndex));
                    AddMatches(GetCandy(rowIndex + 2, columnIndex));
                }
            }
        }
            

            return false;
    }


    private ArrayList matchCandys;
    //匹配的Candy
    private void AddMatches(Candy c)
    {
        Debug.Log("rowIndex;" + c.rowIndex + ",columnIndex" + c.columnIndex);
        if (matchCandys == null) matchCandys = new ArrayList();
        int i = matchCandys.IndexOf(c);
        if (i == -1)
        {
            matchCandys.Add(c);
        }
    }

    private void RemoveMatches()
    {
        Candy tmpCandy;
        if (matchCandys != null)
        {
            for (int i = 0; i < matchCandys.Count; i++)
            {
                tmpCandy = matchCandys[i] as Candy;
                Remove(tmpCandy);
            }

            matchCandys = new ArrayList();
        }

    }


    //=================================================================================================
    public string LoadJsonData(string fileName, string firstIndexName, int strNo, string keyName)
    {
        JsonData json;
        string valuePair;
        json = LoadJsonData(fileName);
        valuePair = (string)json[firstIndexName][strNo][keyName];

        return valuePair;
    }

    JsonData LoadJsonData(string fileName)
    {
        string filePath = Application.dataPath + "/Json/JsonText/RoadExpand/" + fileName + ".txt";     //Json路径
        string jsonStr = File.ReadAllText(filePath);
        JsonData jd = JsonMapper.ToObject(jsonStr);
        return jd;
    }
}
