using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Timeline;

public class CameraToggle : MonoBehaviour
{
    public CinemachineCamera player;
    public CinemachineCamera quiz;

    public void PlayerCamera()
    {
        player.gameObject.SetActive(true);
        quiz.gameObject.SetActive(false);
    }

    public void QuizCamera()
    {
        player.gameObject.SetActive(false);
        quiz.gameObject.SetActive(true);
    }
}
