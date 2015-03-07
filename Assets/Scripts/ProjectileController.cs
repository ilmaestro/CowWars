using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour
{

    public float initialVelocity = 20.0f;
    public delegate void Destroyed();
    public Destroyed OnDestroyed;

	void Start () 
    {
        rigidbody.velocity = transform.forward * initialVelocity;
	}

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collided with " + other.transform.name);
        Invoke("DestroyMe", 2.0f);
    }

    void DestroyMe()
    {
        Destroy(this.gameObject);
        if (OnDestroyed != null)
            OnDestroyed();
    }
}
