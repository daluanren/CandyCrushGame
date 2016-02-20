using UnityEngine;
using System.Collections;

public class StartPageController : MonoBehaviour
{
    public string nextScene;

    private Transform uiCame;
    private Transform panel_center;
    private Transform btn_start;
    private Transform btn_exit;

    void Awake()
    {
        uiCame = GameObject.Find("UI Root/Camera").transform;
        panel_center = uiCame.FindChild("Anchor_center/Panel_center");
        btn_start = panel_center.FindChild("Btn_start");
        btn_exit = panel_center.FindChild("Btn_exit");

        
        btn_start.gameObject.AddComponent<UIEventListener>();
        UIEventListener.Get(btn_start.gameObject).onClick = ClickStartGame;
        btn_exit.gameObject.AddComponent<UIEventListener>();
        UIEventListener.Get(btn_exit.gameObject).onClick = ClickExitGame;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// 开始游戏
    /// </summary>
    /// <param name="btn"></param>
    void ClickStartGame(GameObject btn)
    {
        Debug.Log("enter scene:" + nextScene);
        SceneManager.instance.StartCoroutineToLoadScene(nextScene);
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    /// <param name="btn"></param>
    void ClickExitGame(GameObject btn)
    {
        Debug.Log("Exit!");
        Application.Quit();
    }
}
