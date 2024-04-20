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
        //�񵿱��� ������ ���� �ڷ�ƾ
        //�۾��� ����Ǵ� ���� �񵿱������� �����Ȳ�� üũ�� �� �ִ�.
        AsyncOperation op = SceneManager.LoadSceneAsync(_nextSceneIndex);
        //Scene�� �غ�Ǹ� �ٷ� Ȱ��ȭ - off (���� �ٰ� �� ä������ ���� ����Ǵ� �� ����)
        //�̶��� 95%������ �ε��ǰ� ���̻� �ҷ������� �ʴ´�
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
                //����� �ε��Ǹ� �ε��ٸ� �� ä��� ���� ���� Ȱ��ȭ - on
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
