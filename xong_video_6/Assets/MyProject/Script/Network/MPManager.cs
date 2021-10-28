using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;

public class MPManager : Photon.MonoBehaviour
{
    public GameObject buttonJoin;
    public PlayFabManager auth;

    public string GameVersion;
    public Text connectState;

    public GameObject[] DisableConnected;
    public GameObject[] EnableConnected;

    public string username;


    private List<GameObject> spawnPoints = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            spawnPoints.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        connectState.text = PhotonNetwork.connectionStateDetailed.ToString();
    }

    public void ConnectToMaster()
    {
        PhotonNetwork.ConnectUsingSettings(GameVersion);
    }    

    public virtual void OnConnectedToMaster()
    {
        foreach (GameObject disalbe in DisableConnected)
        {
            disalbe.SetActive(false);
        }
        foreach (GameObject enable in EnableConnected)
        {
            enable.SetActive(true);
        }
    }    

    public void CreateOrJoin()
    {
        PhotonNetwork.JoinRandomRoom();
        buttonJoin.SetActive(false);
    }    

    public virtual void OnPhotonRandomJoinFailed()
    {
        RoomOptions rm = new RoomOptions
        {
            MaxPlayers = 4,
            IsVisible = true
        };
        int rndID = Random.Range(0, 3000);
        PhotonNetwork.CreateRoom("Default: " + rndID, rm, TypedLobby.Default);
    }    
    public virtual void OnJoinedRoom()
    {
        Vector3 pos = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
        GameObject player = PhotonNetwork.Instantiate("n_Player", pos, Quaternion.identity, 0);
        player.GetComponent<Player>().username = username;
    }

}
