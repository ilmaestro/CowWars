using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static int playersTurn = -1;
    public static GameManager instance;
    
    private static PhotonView ScenePhotonView;
    private int player1Id = -1;
    private int player2Id = -1;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

	void Start () 
    {
        ScenePhotonView = this.GetComponent<PhotonView>();
	}

    public void OnJoinedRoomed()
    {
        Debug.Log("GameManager OnJoinedRoom");
        if (PhotonNetwork.playerList.Length == 1)
        {
            playersTurn = PhotonNetwork.player.ID;
            player1Id = PhotonNetwork.player.ID;
        }
        Debug.Log("OnJoinedRoomed: " + playersTurn);
    }

    public void OnPhotonPlayerConnected(PhotonPlayer player)
    {
        Debug.Log("OnPhotonPlayerConnected: " + player);
        // when new players join, we send "who's it" to let them know
        // only one player will do this: the "master"

        if (PhotonNetwork.isMasterClient)
        {
            SetPlayer(1, player1Id);
            SetPlayer(2, player.ID);
            SetPlayerTurn(player1Id);
        }
    }

    [RPC]
    public void UpdatePlayerTurn(int playerId)
    {
        Debug.Log("UpdatePlayerTurn: " + playerId);
        playersTurn = playerId;
    }
    [RPC]
    public void UpdatePlayer(int playerNum, int playerId)
    {
        Debug.Log("UpdatePlayer " + playerNum + ": " + playerId);
        if (playerNum == 1)
            player1Id = playerId;
        else
            player2Id = playerId;
    }

    public static void SetPlayerTurn(int playerId)
    {
        Debug.Log("SetPlayerTurn: " + playerId);
        ScenePhotonView.RPC("UpdatePlayerTurn", PhotonTargets.All, playerId);
    }

    public static void SetPlayer(int playerNum, int playerId)
    {
        Debug.Log("SetPlayer " + playerNum + ": " + playerId);
        ScenePhotonView.RPC("UpdatePlayer", PhotonTargets.All, playerNum, playerId);
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("OnPhotonPlayerDisconnected: " + player);

        if (PhotonNetwork.isMasterClient)
        {
            if (player.ID == playersTurn)
            {
                // if the player who left was "it", the "master" is the new "it"
                SetPlayerTurn(PhotonNetwork.player.ID);
            }
        }
    }

    public void OnMasterClientSwitched()
    {
        Debug.Log("OnMasterClientSwitched");
    }

    public void EndPlayerTurn(int playerId)
    {
        if (playerId == player1Id)
        {
            SetPlayerTurn(player2Id);
        }
        else
        {
            SetPlayerTurn(player1Id);
        }
    }
}
