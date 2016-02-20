/*
 * Author :  well pan
 * date : 2016/2/16
 */
using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    [SerializeField]
    private float m_minDuration = 1.5f;


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
    }

    void Update()
    {
       
    }

    public void StartCoroutineToLoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    /// <summary>
    /// 加载指定场景
    /// </summary>
    /// <param name="sceneName">指定场景</param>
    /// <returns></returns>
    public IEnumerator LoadSceneAsync(string sceneName)
    {

        Debug.Log("!!!Start change to scene:" + sceneName);

        yield return Application.LoadLevelAsync(sceneName);

        float endTime = Time.time + m_minDuration;

        Debug.Log("!!!time.time:"+Time.time+",,end time :"+endTime);
        while (Time.time < endTime)
            yield return null;
    }


}
