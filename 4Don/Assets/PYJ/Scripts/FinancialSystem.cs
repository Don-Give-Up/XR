using System;
using System.Collections.Generic;
using UnityEngine;

public class FinancialSystem : MonoBehaviour
{
   [Serializable]
   public struct Finance
   {
      public int year; //년도 
      public float price; //물가
      public int salary; //월급 
      public float rate; //금리 
      public int breadPrice; //빵가격
   }

   public static FinancialSystem Instance;
   public static Dictionary< int, Finance> finances = new Dictionary<int, Finance>(); // 년도에 해당하는 값을 받아온다. 
}
