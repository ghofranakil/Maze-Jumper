using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public Text countText;
	public Text winText;
	Transform lastCheckpoint;
	private int count;

	void Start () {

		count = 0;
		SetCountText();
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Collectable Object")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
			winText.text = "";
		}

		if (other.gameObject.CompareTag ("Dying Plane")) {
			if (lastCheckpoint == null) {
				SceneManager.LoadScene ("Jumping game");
			} else {
				// go to last checkpoint
				Debug.Log("Respawn at last checkpoint:" + lastCheckpoint.position);
				Rigidbody rb = GetComponent<Rigidbody> ();
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				transform.position = lastCheckpoint.position;
			}

		}

		if (other.gameObject.CompareTag ("Checkpoint")) {
			lastCheckpoint = other.gameObject.transform;
			Debug.Log("Checkpoint passed: " + other.name + " " + lastCheckpoint.position);
		}

		if (other.gameObject.CompareTag ("Winning Collectable Object")) {
			other.gameObject.SetActive (false);
			winText.text = "BRAVO! \nYou just won a useless game";
		}
	}

	void SetCountText () {
		countText.text = "Count: " + count.ToString();
	}
}
