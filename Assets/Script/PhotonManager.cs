using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PhotonManager : Photon.PunBehaviour
{
    static PhotonManager instance = null;
    private void Awake()
    {
        if (null != Instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(this);

        PhotonNetwork.ConnectUsingSettings("Randome Dice 1.0");

    }


    public void MatchingRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        base.OnPhotonRandomJoinFailed(codeAndMsg);
        PhotonNetwork.CreateRoom("TestRoom");

    }
    public bool GameStart()
    {
        if (PhotonNetwork.room != null)
            if (PhotonNetwork.room.PlayerCount == 2)
                return true;
        return false;
    }
    public static PhotonManager Instance
    {
        get
        {
            if (null == instance)
                return null;
            else
                return instance;
        }
    }
}
