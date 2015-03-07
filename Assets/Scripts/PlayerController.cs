using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour 
{
    public GameObject turret;
    public bool isPlayersTurn = false;
    public bool isControllable = false;

    public int playerId = 0;

    public float turningSpeed = 5.0f;
    public float rotationSmoothing = 5.0f;

    private float zEulerAngle;
    private float yEulerAngle;
    private float xEulerAngle;
    private float maxZAngle = 90.9f;
    private float minZAngle = 0.0f;
    private float yFlipper = 1.0f;

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
        isPlayersTurn = (PhotonNetwork.player.ID == GameManager.playersTurn);

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
        }
	}
}
