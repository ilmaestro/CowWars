using UnityEngine;
using System.Collections;

public class LaunchDetector : MonoBehaviour {

    public Transform launchPoint;

    void Update()
    {
        // if this player is not "it", the player can't tag anyone, so don't do anything on collision
        if (PhotonNetwork.player.ID != GameManager.playersTurn)
        {
            return;
        }

        if (Input.GetButton("Fire1"))
        {
            FireProjectile();
            GameManager.instance.EndPlayerTurn(PhotonNetwork.player.ID);
        }
    }

    void FireProjectile()
    {
        //fire a projectile
        GameObject proj = PhotonNetwork.Instantiate("Projectile", launchPoint.position, launchPoint.rotation, 0) as GameObject;
        ProjectileController pCtrllr = proj.GetComponent<ProjectileController>();
        pCtrllr.OnDestroyed += OnProjectileDestroyed;
    }

    void OnProjectileDestroyed()
    {
    }

}
