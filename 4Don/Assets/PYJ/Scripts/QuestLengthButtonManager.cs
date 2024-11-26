using UnityEngine;
using UnityEngine.UI;

public class QuestLengthButtonManager : MonoBehaviour
{
    public Button longButton;
    public Button shortButton;

    private QuestTaskTracker questTaskTracker; 
    
    private void Start()
    {
        questTaskTracker = FindObjectOfType<QuestTaskTracker>(); 
        longButton.gameObject.SetActive(true);
        shortButton.gameObject.SetActive(false);
    }

    public void Long()
    {
        questTaskTracker.MakeLongQuest();

        longButton.gameObject.SetActive(false);
        shortButton.gameObject.SetActive(true);
    }

    public void Short()
    {
        questTaskTracker.MakeShortQuest();
        longButton.gameObject.SetActive(true);
        shortButton.gameObject.SetActive(false);

    }
}
