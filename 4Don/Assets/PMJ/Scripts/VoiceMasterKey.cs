using Photon.Voice.Unity;
using UnityEngine;

public class VoiceMasterKey : MonoBehaviour
{
    public Recorder recorder;

    private void Start()
    {
        if (recorder == null)
        {
            recorder = FindObjectOfType<Recorder>();
            if (recorder != null)
            {
                Debug.Log("레코더 찾았다");
                recorder.TransmitEnabled = false; // 마이크 초기 상태 비활성화
            }
            else
            {
                Debug.LogWarning("레코더를 찾을 수 없습니다.");
            }
        }
    }

    private void LateUpdate()
    {
        if (recorder != null && recorder.TransmitEnabled && !Input.GetKey(KeyCode.Tab))
        {
            recorder.TransmitEnabled = false;
            Debug.Log("마이크 초기화: 비활성화 완료");
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Debug.Log("Tab 키 눌림 상태");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (recorder != null)
            {
                recorder.TransmitEnabled = true;
                Debug.Log("마이크 켜짐 ㅎㅎ");
            }
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            if (recorder != null)
            {
                recorder.TransmitEnabled = false;
                Debug.Log("마이크 꺼짐 ㅠㅠ");
            }
        }
    }
}