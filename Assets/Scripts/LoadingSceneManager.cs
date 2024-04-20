using System.Collections;
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
        //비동기적 연산을 위한 코루틴
        //작업이 진행되는 동안 비동기적으로 진행상황을 체크할 수 있다.
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextSceneIndex);
        //Scene이 준비되면 바로 활성화 - off (진행 바가 다 채워지기 전에 실행되는 것 방지)
        //이때는 95%까지만 로딩되고 더이상 불러들이지 않는다
        op.allowSceneActivation = false; 
        while (!op.isDone)
        {
            yield return null;
            if(op.progress < 0.9f)
            {
                _loadingBar.fillAmount = op.progress;
            }
            else
            {
                //충분히 로딩되면 로딩바를 꽉 채우고 다음 씬을 활성화 - on
                _loadingBar.fillAmount = 1;
                if(_loadingBar.fillAmount >= 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
