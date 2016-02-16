/*
 * Author :  well pan
 * date : 2016/2/10
 */

using UnityEngine;
using System.Collections;

public class Candy : MonoBehaviour {
    public int rowIndex = 0;
    public int columnIndex = 0;
    public string typeName = null;

    private float basePos = 60.00f;
    private float xOff = -4.0f;
    private float yOff = -3.0f;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    /// <summary>
    ///确定Candy类型，图片 
    /// </summary>
    public void SetType()
    {
        this.GetComponent<UISprite>().spriteName = typeName;
    }

    /// <summary>
    /// 更新Candy位置
    /// </summary>
    public void UpdatePosition()
    {
        this.transform.localPosition = new Vector3(basePos * (columnIndex + xOff), basePos*(rowIndex + yOff), 0.0f);
    }
}
