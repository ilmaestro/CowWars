using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public Transform player1Spawn;
    public Transform player2Spawn;
    public static GameManager instance;

    private Queue<object[]> movementQueue = new Queue<object[]>();

    void Awake()
    {
        if (instance == null)
            instance = this;
    }


	void Start () 
    {
        PhotonNetwork.ConnectUsingSettings("0.0.1");
        //log level.. for debug purposes..
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
	}

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("no room to join, creating new room..");

        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        if (PhotonNetwork.playerList.Length == 1)
        {
            // player 1
            GameObject player = PhotonNetwork.Instantiate("Player", player1Spawn.position, player1Spawn.rotation, 0);
            PlayerController controller = player.GetComponent<PlayerController>();
            controller.isControllable = true;
        }
        else if (PhotonNetwork.playerList.Length == 2)
        {
            // player 2
            GameObject player = PhotonNetwork.Instantiate("Player", player2Spawn.position, player2Spawn.rotation, 0);
            PlayerController controller = player.GetComponent<PlayerController>();
            controller.isControllable = true;
        }
        else
        {
            Debug.Log("No more player positions allowed.");
        }
        
    }
}
