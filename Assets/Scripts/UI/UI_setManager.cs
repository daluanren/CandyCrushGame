using UnityEngine;
using System.Collections;

public class UI_setManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        isHelpShow = false;
        this.transform.FindChild("Anchor_center/Panel_center/Panel_help").position = resoucePos;
    }

    bool isNoVoice = false;
    public void OnVoiceClick(GameObject btn)
    {
        UISprite sprit = btn.GetComponent<UISprite>();
        UIButton uiBtn = btn.GetComponent<UIButton>();

        if (isNoVoice)
        {
            sprit.spriteName = "Window";
            uiBtn.normalSprite = "Window";
            isNoVoice = false;
        }
        else
        {
            sprit.spriteName = "X Mark";
            uiBtn.normalSprite = "X Mark";
            isNoVoice = true;
        }
    }

    bool isHelpShow = false;
    Vector3 resoucePos = new Vector3(0, 700f, 0);
    public void OnHelpClick(GameObject panel_help)
    {

        if (isHelpShow)
        {
            ChangeTweenPosition(panel_help,resoucePos);

            isHelpShow = false;
           
        }
        else
        {
            ChangeTweenPosition(panel_help, Vector3.zero);

            isHelpShow = true;

        }

    }

    void ChangeTweenPosition(GameObject obj, Vector3 to)
    {
        iTween.MoveTo(obj, iTween.Hash
                    (
                        "position", to,
                        "time", 0.5f,
                        "islocal", true
                    )
                );
    }
}
