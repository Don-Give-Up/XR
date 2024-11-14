using System;
using UnityEngine;

public class MaterKey : MonoBehaviour
{
   public GameObject newsmain;
   public GameObject chatBot;
   public GameObject blur;

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Alpha6))
      {
         newsmain.SetActive(false);
         chatBot.SetActive(false);
         blur.SetActive(false);
      }
   }
}
