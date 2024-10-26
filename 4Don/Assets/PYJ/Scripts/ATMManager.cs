using System;
using UnityEngine;

public class ATMManager : MonoBehaviour
{
  public static ATMManager Instance;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void ATM() // atm 기 클릭함
  {
    
  }

  public void Save()
  {
    
  }

  public void Stock()
  {
    
  }
  
  





}




