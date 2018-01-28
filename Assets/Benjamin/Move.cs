using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (-7.6f, 4.5f, 0f);
		StartCoroutine (MoveDown ());
	}
	void Update(){
		if (Input.GetKeyUp (KeyCode.Tab)) {
			Start ();
		}
	}
	public IEnumerator MoveDown(){
		while (transform.position.y > -4.5f && !Input.GetKeyDown (KeyCode.Tab)) {
			transform.Translate (0f, -9.0f / 138f * Time.deltaTime, 0f);
			yield return null;
		}
	}
}
