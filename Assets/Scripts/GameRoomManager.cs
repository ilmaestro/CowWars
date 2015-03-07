using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameRoomManager : MonoBehaviour {

    private PhotonView myPhotonView;
    public Transform player1Spawn;
    public Transform player2Spawn;

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.0.1");
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joining Random Room...");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        Debug.Log("RoomManager OnJoinedRoom");
        if (PhotonNetwork.playerList.Length == 1)
        {
            Debug.Log("first player joined..");
            // player 1
            GameObject player = PhotonNetwork.Instantiate("Player", player1Spawn.position, player1Spawn.rotation, 0);
            PlayerController controller = player.GetComponent<PlayerController>();
            myPhotonView = player.GetComponent<PhotonView>();
            controller.isControllable = true;
        }
        else if (PhotonNetwork.playerList.Length == 2)
        {
            Debug.Log("second player joined..");
            // player 2
            GameObject player = PhotonNetwork.Instantiate("Player", player2Spawn.position, player2Spawn.rotation, 0);
            PlayerController controller = player.GetComponent<PlayerController>();
            myPhotonView = player.GetComponent<PhotonView>();
            controller.isControllable = true;
        }
        else
        {
            Debug.Log("No more player positions allowed.");
        }
        GameManager.instance.OnJoinedRoomed();
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        //if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
        //{
        //    bool shoutMarco = GameLogic.playerWhoIsIt == PhotonNetwork.player.ID;

        //    if (shoutMarco && GUILayout.Button("Marco!"))
        //    {
        //        myPhotonView.RPC("Marco", PhotonTargets.All);
        //    }
        //    if (!shoutMarco && GUILayout.Button("Polo!"))
        //    {
        //        myPhotonView.RPC("Polo", PhotonTargets.All);
        //    }
        //}
    }
}
