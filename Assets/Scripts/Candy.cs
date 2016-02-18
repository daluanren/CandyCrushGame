/*
 * Author :  well pan
 * date : 2016/2/10
 */

using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour
{
    public int rowIndex = 0;
    public int columnIndex = 0;
    public string typeName = null;
    public GameController gameController;

    private float basePos = 60.00f;
    private float xOff = -4.0f;
    private float yOff = -3.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        gameController.Select(this);
    }

    /// <summary>
    ///确定Candy类型，图片 
    /// </summary>
    public void SetRangeBg()
    {
       
        if (string.IsNullOrEmpty(typeName))
        {
            string[] candyTypeNames = new string[] { "Normal_red", "Normal_blue", "Normal_orange", "Normal_green", "Normal_yellow", "Wrap_blue", "Wrap_green", "Wrap_orange", "Wrap_purple", "Wrap_red", "Wrap_yellow" };
            int randomNum = Random.Range(0, candyTypeNames.Length);
            typeName = candyTypeNames[randomNum];
            this.GetComponent<UISprite>().spriteName = typeName;
        }

    }

    /// <summary>
    /// 根据行列索引更新Candy位置
    /// </summary>
    public void UpdatePosition()
    {
        SetRangeBg();

        this.transform.localPosition = new Vector3(basePos * (columnIndex + xOff), basePos * (rowIndex + yOff), 0.0f);
    }

    /// <summary>
    /// 缓动更新 Candy位置
    /// </summary>
    public void TweenPosition(float followSpeed)
    {
        SetRangeBg();

        //Debug.Log("row:" + rowIndex + ",,,column" + columnIndex);
        //Debug.Log("x:" + basePos * (columnIndex + xOff));
        //Debug.Log("y:" + basePos * (rowIndex + yOff));
        iTween.MoveTo(this.gameObject, iTween.Hash
            (
                "x", basePos * (columnIndex + xOff),
                "y", basePos * (rowIndex + yOff),
                "time", followSpeed,
                "islocal",true
            )
            );
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Dispose()
    {
        gameController = null;  //销毁与GameController间的关联
        typeName = null;
        Destroy(this.gameObject);
    }
}
