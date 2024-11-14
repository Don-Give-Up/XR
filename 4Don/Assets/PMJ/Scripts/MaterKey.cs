using System;
using UnityEngine;

public class MaterKey : MonoBehaviour
{
   public GameObject newsmain;
   public GameObject chatBot;
   public GameObject blur;
   private PersonalFinancialManager money;


   private void Start()
   {
      // PersonalFinancialManager 싱글톤 인스턴스를 가져옵니다.
      money = PersonalFinancialManager.Instance;
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Alpha6))
      {
         newsmain.SetActive(false);
         chatBot.SetActive(false);
         blur.SetActive(false);
      }

      if (Input.GetKeyDown(KeyCode.Alpha1))
      {
         if (money != null)
         {
            // InputMoney 메서드를 통해 1키를 눌렀을 때 돈이 추가되도록 호출
            money.InputMoney(8 * 8590);
         }
      }
   }
}