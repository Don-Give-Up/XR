using UnityEngine;

public class InventoryItems : MonoBehaviour
{
    private HungryBar hungryBar; // HungryBar 참조

    void Start()
    {
        // HungryBar 컴포넌트 찾기
        hungryBar = FindObjectOfType<HungryBar>();
        if (hungryBar == null)
        {
            Debug.LogError("HungryBar 없다");
        }
    }

    // Bread1 클릭
    public void OnEatBread1()
    {
        hungryBar.EatBread(1);
        Debug.Log("빵1 섭취, 0.1 증가");
    }

    // Bread5 클릭
    public void OnEatBread5()
    {
        hungryBar.EatBread(5);
        Debug.Log("빵5 섭취, 0.5 증가");
    }

    // Bread10 클릭
    public void OnEatBread10()
    {
        hungryBar.EatBread(10);
        Debug.Log("빵10 섭취, 1 증가");
    }
}
