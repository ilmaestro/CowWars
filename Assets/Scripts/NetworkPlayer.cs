using UnityEngine;
using System.Collections;

public class NetworkPlayer : Photon.MonoBehaviour {

    public Transform turret;

    private Quaternion correctRotation;

    void Update()
    {
        if (!photonView.isMine)
        {
            //smooth out the other player movement
            turret.rotation = Quaternion.Lerp(turret.rotation, correctRotation, Time.deltaTime * 5);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //send our data
            stream.SendNext(turret.rotation);
        }
        else
        {
            //network player, receive data
            correctRotation = (Quaternion)stream.ReceiveNext();
        }
        
    }

}
