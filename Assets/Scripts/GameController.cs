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
    public int rowNum;                 
    public int columnNum;   
    public Candy candy;
    public Transform panel_candys;

    private string[] candyTypeNames;

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        candyTypeNames = new string[]{"Normal_red","Normal_blue"};

        for (int rowIndex = 0; rowIndex < rowNum; rowIndex++)
        {
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

                int randomNum = Random.Range(0, candyTypeNames.Length);
                theCandy.typeName = candyTypeNames[randomNum];
                theCandy.SetType();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

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
