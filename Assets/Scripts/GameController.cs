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
            ArrayList  tmpArr = new ArrayList();
            for (int columnIndex = 0; columnIndex < columnNum; columnIndex++)
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

                tmpArr.Add(theCandy);
            }
            candyArr.Add(tmpArr);
        }

    }

    /// <summary>
    /// 获取二维数组元素
    /// </summary>
    /// <param name="rowIndex">一维索引</param>
    /// <param name="columnIndex">二维索引</param>
    /// <returns></returns>
    private Candy GetCandy(int rowIndex, int columnIndex)
    {
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
    private void SetCandy(int rowIndex, int columnIndex,Candy c)
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
        //testttttttttt====================================================
        Remove(c); return;

        if (crtCandy == null)
        {
            crtCandy = c;
        }
        else
        {
            Exchange(crtCandy, c);
            crtCandy = null;
        }
    }

    //交换Candy
    private void Exchange(Candy c1, Candy c2)
    {
       
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
        c.Dispose();

        int columnIndex = c.columnIndex;
        for (int rowIndex = c.rowIndex + 1; rowIndex < rowNum; rowIndex++)
        {
            Candy c2 = GetCandy(rowIndex, columnIndex);
            c2.rowIndex--;
            c2.UpdatePosition();
            SetCandy(rowIndex - 1, columnIndex, c2);
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
