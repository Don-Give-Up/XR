using UnityEngine;

public class DonDestroy : MonoBehaviour
{
    public static DonDestroy instance; 
    void Awake()
    {
        if (instance == null)
        {
            // 첫 번째 인스턴스가 있을 때만 DontDestroyOnLoad
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 두 번째 인스턴스는 파괴
            Destroy(gameObject);
        }
    }
}

