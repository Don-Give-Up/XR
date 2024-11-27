using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class VerticalRectangle : MonoBehaviour
{
  public Image gaugeImage;
  public float currentAmount; 
  public float targetAmount;
  public float speed; 
  private bool isUpdating = false;

  // Update 메서드는 매 프레임마다 호출됩니다.
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.R) && !isUpdating)
    {
      // 비동기 작업이 진행 중이지 않다면 비동기 메서드를 호출
      currentAmount = 0f;
      targetAmount = 1f; 
      UpdateGaugeAsync();
    }
  }

  // 비동기적으로 Lerp 작업을 완료할 때까지 기다림
  private async void UpdateGaugeAsync()
  {
    isUpdating = true;

    // 시작값과 목표값의 차이를 계산하여 완료까지의 시간 예측
    float startAmount = currentAmount;
    float targetAmount = this.targetAmount;

    float elapsedTime = 0f; // 경과 시간

    // 원하는 값을 채우기 위해 Lerp를 비동기적으로 진행
    while (!Mathf.Approximately(currentAmount, targetAmount))
    {
      // Lerp를 Time.deltaTime을 기준으로 계산
      elapsedTime += Time.deltaTime;
      currentAmount = Mathf.Lerp(startAmount, targetAmount, elapsedTime * speed);

      // 목표값을 초과하지 않도록 보정
      currentAmount = Mathf.Clamp01(currentAmount);

      // FillAmount를 업데이트
      gaugeImage.fillAmount = currentAmount;

      // 프레임을 기다리기 위해 잠시 대기 (1 프레임 기다림)
      await UniTask.DelayFrame(1);
    }

    // 작업 완료 후 마지막으로 정확히 목표값에 맞추기
    currentAmount = targetAmount;
    gaugeImage.fillAmount = currentAmount;

    isUpdating = false;
  }
}