using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    public static int _nextSceneIndex;
    [SerializeField] Image _loadingBar;
    public static void LoadScene(int SceneIndex)
    {
        _nextSceneIndex = SceneIndex;
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        _loadingBar.fillAmount = 0; 
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextSceneIndex);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if(op.progress < 0.9f)
            {
                //_loadingBar.fillAmount = op.progress;
                _loadingBar.fillAmount = Mathf.Lerp(_loadingBar.fillAmount, op.progress, timer);
                if(_loadingBar.fillAmount >= op.progress)
                {
                    timer = 0.0f;
                }
            }
            else
            {
                //_loadingBar.fillAmount = op.progress;
                _loadingBar.fillAmount = Mathf.Lerp(_loadingBar.fillAmount, 1f, timer);
                if(_loadingBar.fillAmount >= 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
