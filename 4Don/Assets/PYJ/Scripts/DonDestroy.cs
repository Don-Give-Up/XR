using UnityEngine;

public class DonDestroy : MonoBehaviour
{
    public static DonDestroy instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 부모 객체에 적용
            // 자식 객체에 대해서도 DontDestroyOnLoad를 적용하려면
            foreach (Transform child in transform)
            {
                DontDestroyOnLoad(child.gameObject);  // 자식 객체에도 적용
            }
        }
        else
        {
            Destroy(gameObject);  // 두 번째 인스턴스는 삭제
        }
    }
}

