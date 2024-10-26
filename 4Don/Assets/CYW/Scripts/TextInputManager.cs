using UnityEngine;
using UnityEngine.UI;


public class TextInputManager : MonoBehaviour
{
    public InputField inputField; // UI에서 만든 InputField
    public Text displayText;       // 입력된 텍스트를 보여줄 Text

    public void ShowInput()
    {
        // 입력된 텍스트를 가져와서 displayText에 표시
        displayText.text = inputField.text;
    }
        
}

