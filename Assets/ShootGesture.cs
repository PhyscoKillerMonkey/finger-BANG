using UnityEngine;
using System.Collections.Generic;
using Leap;

public class ShootGesture : MonoBehaviour {

	Controller c;
	float oldPitch;
	float maxChange;
	long lastFired = 0;
	public float threshold;
	public int fireRateMS;
	public Rigidbody projectile;
	public int speedX;
	public int speedY;
	public int speedZ;
	public bool isRight;

	void Start () {
		c = new Controller ();
	}

	void Update () {
		foreach (Hand hand in c.Frame().Hands) {
			if (hand.IsRight && isRight) {
				fire (hand);
			} else if (hand.IsLeft && !isRight) {
				fire (hand);
			}
		} 
	}

	void fire(Hand hand) {
		float newPitch = hand.Direction.Pitch;
		float change = oldPitch - newPitch;
		maxChange = Mathf.Max (maxChange, change);
		//Debug.Log ("Change: " + (oldPitch - newPitch) + " Max: " + maxChange);
		//Debug.Log(hand.Direction);
		oldPitch = newPitch;
		long now = c.Now ();
		//Debug.Log (now - lastFired);
		if ((change > threshold || Input.GetButtonDown ("Fire1")) && now - lastFired > fireRateMS) {
			Debug.Log (change);
			lastFired = now;
			Rigidbody clone = (Rigidbody)Instantiate (projectile, transform.position, transform.rotation);
			clone.velocity = transform.TransformDirection (new Vector3 (speedX, speedY, speedZ));
			Destroy (clone.gameObject, 5);
		}
	}
}