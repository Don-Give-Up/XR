using System;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;

public class QuestButtonManager : MonoBehaviour
{
  private QuestTaskTracker questTaskTracker;
  private void Awake()
  {
    questTaskTracker = FindObjectOfType<QuestTaskTracker>(); 
  }

  public void Clicked()
  {
    questTaskTracker.isClicked = true; 
  }
}
