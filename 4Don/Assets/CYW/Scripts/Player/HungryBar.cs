using System;
using UnityEngine;
using UnityEngine.UI;

public class HungryBar : MonoBehaviour
{
   // 하루는 5분, HungryBar는 자동으로 30초에 0.1만큼 1에서 0까지 줄어듦, 0이 되면 ???
   // 1 레벨 빵 먹으면 0.1 만큼 오르고, 3레벨 빵 먹으면 0.3만큼 오름 . . . 1 5 10 생각 중
   // 빵은 아이템으로 나누고 태그 다 다르게 설정한 뒤 태그로 코드 짜기 => 지금은 모델을 바로 클릭하는 걸로 짜도 나중엔 아이콘 눌러서 실행되게 할 거니까


   public Slider hungrySlider;
   public float decreaseRate = 0.1f; // 1분에 0.1만큼 줄어듦
   private float decreaseTimer;

   void Start()
   {
      hungrySlider.value = 1; // 초기값을 1로 설정
      decreaseTimer = 30f; // 30초
   }

   void Update()
   {
      decreaseTimer -= Time.deltaTime;
      if (decreaseTimer <= 0)
      {
         DecreaseHungry();
         decreaseTimer = 30f; // 타이머 리셋
      }
   }

   public void EatBread(int level)
   {
      float increaseAmount = 0;

      switch (level)
      {
         case 1:
            increaseAmount = 0.1f;
            break;
         case 5:
            increaseAmount = 0.5f;
            break;
         case 10:
            increaseAmount = 1f;
            break;
         default:
            Debug.LogWarning("Invalid bread level!");
            return;
      }

      hungrySlider.value = Mathf.Clamp(hungrySlider.value + increaseAmount, 0, 1);
   }

   private void DecreaseHungry()
   {
      if (hungrySlider.value > 0) // 0 이하로 줄어들지 않도록 체크
      {
         hungrySlider.value = Mathf.Clamp(hungrySlider.value - decreaseRate, 0, 1);
      }
   }
   
}
