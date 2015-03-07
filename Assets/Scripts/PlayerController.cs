using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour 
{
    public GameObject turret;
    public GameObject projectile;
    public Transform launchPoint;
    public bool isPlayersTurn = true;
    public bool isControllable = false;
    public float turningSpeed = 5.0f;
    public int playerId = 0;
    public float rotationSmoothing = 5.0f;

    private float zEulerAngle;
    private float yEulerAngle;
    private float xEulerAngle;
    private float maxZAngle = 90.9f;
    private float minZAngle = 0.0f;
    private float yFlipper = 1.0f;

    private bool doLaunch = false;

	void Start () 
    {
        //set our min to the starting angle (assuming the prefab is in the min position)
        minZAngle = zEulerAngle = turret.transform.rotation.eulerAngles.z;
        yEulerAngle = turret.transform.rotation.eulerAngles.y;
        xEulerAngle = turret.transform.rotation.eulerAngles.x;
        maxZAngle = minZAngle + 90.0f;
        if (yEulerAngle > float.Epsilon)
            yFlipper = -1.0f;
	}
	
	void Update () 
    {
        if (isPlayersTurn && isControllable)
        {
            //check for player input
            float movement = Input.GetAxis("Horizontal");
            if (Mathf.Abs(movement) > float.Epsilon)
            {
                zEulerAngle = Mathf.Clamp(zEulerAngle - (yFlipper * movement * turningSpeed * Time.deltaTime), minZAngle, maxZAngle);
                //rotate the turret
                turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation, Quaternion.Euler(xEulerAngle, yEulerAngle, zEulerAngle), rotationSmoothing);
            }
            
            if (Input.GetButton("Fire1"))
            {
                //GameManager.instance.LaunchProjectile(playerId);
                //isPlayersTurn = false;
            }
        }

        if (doLaunch)
            LaunchMe();
	}

    public void ReadyForLaunch()
    {
        doLaunch = true;
    }

    public void SetAngle(float angle)
    {
        zEulerAngle = angle;
    }

    void LaunchMe()
    {
        //fire a projectile
        GameObject proj = Instantiate(projectile, launchPoint.position, launchPoint.rotation) as GameObject;
        ProjectileController pCtrllr = proj.GetComponent<ProjectileController>();
        pCtrllr.OnDestroyed += OnProjectileDestroyed;
        doLaunch = false;
    }

    void OnProjectileDestroyed()
    {
        isPlayersTurn = true;
    }
}
