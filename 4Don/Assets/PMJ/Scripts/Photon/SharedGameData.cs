using UnityEngine;

public class SharedGameData : MonoBehaviour
{
    public static SharedGameData Instance;
    
    public static int ReadyCount { get; private set; }
    public static int GameEndCount { get; private set; }
    
}
