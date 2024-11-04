using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[System.Serializable]
public class LoginManager : MonoBehaviour
{
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public TMP_InputField nameField;
    public TMP_InputField schoolField;
    public TMP_InputField birthdayField;
    public TMP_InputField nicknameField;
    public TMP_InputField roleField;
    public TMP_InputField gradeField;
    public TMP_InputField classField;
    
    public LoginRequest GetSignUp()
    {
        LoginRequest req = new LoginRequest()
        {
            memberEmail = emailField.text,
            memberPassword = passwordField.text,
            memberName = nameField.text,
            memberSchool = schoolField.text,
            memberBirthday = birthdayField.text,
            memberNickname = nicknameField.text,
            memberRole = roleField.text,
            memberGrade = int.Parse(gradeField.text),
            memberClass = int.Parse(classField.text),
        };
        return req;
    }
    
    public LoginData GetLogin()
    {
        LoginData req = new LoginData()
        {
            memberEmail = emailField.text,
            memberPassword = passwordField.text,
           
        };
        return req;
    }
    
}

[System.Serializable]
public class LoginResponse
{
    public int memberId;
    public string memberEmail;
    public string memberPassword;
    public string memberName;
    public string memberSchool;
    public string memberBirthday;
    public string memberNickname;
    public string memberRole;
    public int memberGrade;
    public int memberClass;
}

[System.Serializable]
public class LoginRequest
{
    public string memberEmail;
    public string memberPassword;
    public string memberName;
    public string memberSchool;
    public string memberBirthday;
    public string memberNickname;
    public string memberRole;
    public int memberGrade;
    public int memberClass;
}

[System.Serializable]
public class LoginData
{
    public string memberEmail;
    public string memberPassword;
}


