using System;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
   public AudioSource audioSource;
   public AudioClip clickSound;

   private void Start()
   {
      audioSource = GetComponent<AudioSource>();
   }

   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;
         
         // 부딪친 지점 받기
         if (Physics.Raycast(ray, out hit))
         {
            if (hit.collider.CompareTag("Button")) // 버튼 태그만 눌렀을 때
            {
               audioSource.PlayOneShot(clickSound);
            }
         }
      }
   }
}
