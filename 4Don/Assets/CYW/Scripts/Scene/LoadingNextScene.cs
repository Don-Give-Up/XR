using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingNextScene : MonoBehaviour
{
    // 다음 씬을 비동기 방식으로 로드하고 싶다.
    // 또한 현재 씬에는 로딩 진행률을 시각적으로 표현하고 싶다.
/*
    public Slider loadingBar;

    private IEnumerator TransitionNextScene(string sceneName)
    {
        // 지정된 씬을 비동기 형식으로 로드한다.
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        // 로드되는 씬의 모습이 화면에 보이지 않게 한다.
        ao.allowSceneActivation = false;

        
        // 로딩이 완료될 때까지 반복해서 씬의 요소들을 로드하고 진행 과정을 화면에 표시한다
        while (!ao.isDone)
        {
            // 로딩 진행률을 슬라이더 바와 텍스트로 표시한다.
            loadingBar.value = ao.progress;
        }
        
        
    }
        */
}
