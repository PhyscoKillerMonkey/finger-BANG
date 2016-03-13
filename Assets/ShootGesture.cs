using UnityEngine;
using System.Collections.Generic;
using Leap;

public class ShootGesture : MonoBehaviour {

	Controller c;
	float oldPitch;
	float maxChange;
	public float threshold;
	public Rigidbody projectile;
	public int speedX;
	public int speedY;
	public int speedZ;

	void Start () {
		c = new Controller ();
	}

	void Update () {
		foreach (Hand hand in c.Frame().Hands) {
			float newPitch = hand.Direction.Pitch;
			float change = oldPitch - newPitch;
			maxChange = Mathf.Max (maxChange, change);
			Debug.Log ("Change: " + (oldPitch - newPitch) + " Max: " + maxChange);
			//Debug.Log(hand.Direction);
			oldPitch = newPitch;

			if (change > threshold || Input.GetButtonDown("Fire1")) {
				Rigidbody clone = (Rigidbody)Instantiate (projectile, transform.position, transform.rotation);
				clone.velocity = transform.TransformDirection (new Vector3 (speedX, speedY, speedZ));
				Destroy (clone.gameObject, 5);
			}
		}
	}
}