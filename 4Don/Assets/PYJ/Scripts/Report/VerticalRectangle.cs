using UnityEngine;
using UnityEngine.UI;

public class VerticalRectangle : MonoBehaviour
{
  public Image gaugeImage;
  public float currentAmount = 0f; 
  public float targetAmount = 0.5f;
  public float speed = 2f; 

  void Update()
  {
    currentAmount = Mathf.Lerp(currentAmount, targetAmount, speed * Time.deltaTime);
    gaugeImage.fillAmount = currentAmount; 
  }
  

}
