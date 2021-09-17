using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MatchingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonManager.Instance.OnJoinedLobby();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonManager.Instance.GameStart())
        {
            SceneManager.LoadScene("InGame");
        }
    }
}
