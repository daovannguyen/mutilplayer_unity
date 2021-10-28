using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
//using MPManager;

public class PlayFabManager : MonoBehaviour
{
    public MPManager mp;
    public InputField username;
    public InputField password;
    public InputField email;
    public Button register;
    public Text loginState;


    public GameObject loginUI;


    LoginWithPlayFabRequest loginRequests;
    // Start is called before the first frame update
    void Start()
    {
        email.gameObject.SetActive(false);
        register.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Login()
    {
        loginRequests = new LoginWithPlayFabRequest();
        loginRequests.Username = username.text;
        loginRequests.Password = password.text;
        PlayFabClientAPI.LoginWithPlayFab(loginRequests, result => {
            loginState.text = "Wellcome " + username.text + " Connecting...";
            mp.username = username.text;
            mp.ConnectToMaster();
        }, erorr => {
            loginState.text = "Fail login";
            email.gameObject.SetActive(true);
            register.gameObject.SetActive(true);
        }, null);
    }
    public void Register()
    {
        RegisterPlayFabUserRequest loginRequests = new RegisterPlayFabUserRequest();
        loginRequests.Username = username.text;
        loginRequests.Password = password.text;
        loginRequests.Email = email.text;

        PlayFabClientAPI.RegisterPlayFabUser(loginRequests, result => {
            loginState.text = "Created account!!!";
        }, erorr => {
            loginState.text = "Fail Register";
        }, null);
    }
}
