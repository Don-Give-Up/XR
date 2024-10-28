using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class BEGet : MonoBehaviour
{
    string BEServerUrl = "http://125.132.216.190:5678/api/members/1"; // 멤버 ID가 이미 포함된 URL

    private void Start()
    {
        StartCoroutine(SendProblemData());
    }

    // BE 서버에 GET 요청하는 함수
    public IEnumerator SendProblemData()
    {
        // UnityWebRequest를 사용하여 GET 요청
        UnityWebRequest request = UnityWebRequest.Get(BEServerUrl);

        // 서버에 요청을 보냄
        yield return request.SendWebRequest();

        // 응답이 성공적인지 확인
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 서버 응답 데이터를 JSON으로 처리
            string responseData = request.downloadHandler.text;
            Debug.Log("Response: " + responseData);

            // 응답 데이터를 BEData 형식으로 변환
            BEData data = JsonConvert.DeserializeObject<BEData>(responseData);

            // BEData의 member 정보 출력
            foreach (var memberInfo in data.members)
            {
                Debug.Log("Member ID: " + memberInfo.memberId);
                Debug.Log("Member Email: " + memberInfo.memberEmail);
                Debug.Log("Member School: " + memberInfo.memberSchool);
                Debug.Log("Member Name: " + memberInfo.memberName);
                Debug.Log("Member Grade: " + memberInfo.memberGrade);
                Debug.Log("Member Class: " + memberInfo.memberClass);
                Debug.Log("Member Number: " + memberInfo.memberNumber);
            }
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
}


// BEData 클래스 정의
public class BEData
{
    public List<Member> members; // 여러 멤버 정보를 담는 리스트

    public class Member
    {
        public int memberId;
        public string memberEmail;
        public string memberSchool;
        public string memberName;
        public int memberGrade;
        public int memberClass;
        public int memberNumber;
    }
}
