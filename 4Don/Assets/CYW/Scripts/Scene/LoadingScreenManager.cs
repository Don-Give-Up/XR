using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenManager : MonoBehaviour
{
   public static LoadingScreenManager Instance;  // 싱글톤 인스턴스
   public Slider progressBar;  // 프로그레스 바

   private void Awake()
   {
      // 싱글톤 인스턴스 설정
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
   }

   // 로딩 화면을 표시하고, 씬을 비동기적으로 로드하는 메서드
   public void StartLoading(string sceneName)
   {
      ShowLoadingScreen(true);  // 로딩 UI 활성화
      StartCoroutine(LoadSceneAsync(sceneName));
   }

   private IEnumerator LoadSceneAsync(string sceneName)
   {
      AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

      while (!operation.isDone)
      {
         float progress = Mathf.Clamp01(operation.progress / 0.9f);  // 씬 로딩 진행
         progressBar.value = progress;
         yield return null;
      }

      ShowLoadingScreen(false);  // 로딩 화면 숨기기
   }

   // 로딩 화면 보이기/숨기기
   private void ShowLoadingScreen(bool show)
   {
      gameObject.SetActive(show);
   }
   
}
