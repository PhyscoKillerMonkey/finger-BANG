using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public Rigidbody projectile;
	public int speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			Debug.Log("Bang!");
			Rigidbody clone = (Rigidbody) Instantiate (projectile, transform.position, transform.rotation);
			clone.velocity = transform.TransformDirection (new Vector3 (0, 0, speed));
			Destroy (clone.gameObject, 5);
		}
	}
}
