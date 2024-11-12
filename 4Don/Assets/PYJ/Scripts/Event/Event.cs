using System;
using UnityEngine;

public class Events : MonoBehaviour
{
   public EventType eventType;

   public GameObject buyStock;

   public static Events instance;

   private void Awake()
   {
       if (instance == null)
       {
           instance = this;
       }
       else
       {
           Destroy(gameObject);
       }
   }

   public void EventOccure(EventType? eventType)
   {
       switch (eventType)
       {
           case EventType.none: break;
           case EventType.deposit : break;
           case EventType.withdrawal: break;
           case EventType.buyStock:
               buyStock.SetActive(true);
               break;
           case EventType.sellStock: break;
           case EventType.purchase: break;
           case EventType.sale: break;
           
       }
   }
   
}

public enum EventType
{
    none,
    deposit,
    withdrawal, 
    buyStock, 
    sellStock,
    purchase, 
    sale,
    
}
