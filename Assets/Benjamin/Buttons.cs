using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {
	int fubar;
	public Button[] buttons;
	private bool SatSelect;
	private bool[] weaknesses;
	private bool[] options;
	public bool sat0connect;
	public bool sat1connect;
	private List<int> ButtonSelectX;
	private List<int> ButtonSelect1;
	private List<int> ButtonSelect2;
	void Start(){
		weaknesses = new bool[4];
		options = new bool[4];
		ButtonSelectX = new List<int> ();
		ButtonSelect1 = new List<int> ();
		ButtonSelect2 = new List<int> ();
		BeginRun ();
	}
	public void adjustSatellite(bool sat){
		SatSelect = sat;
		ButtonSelectX = new List<int> ();
		for (int j = 0; j < 4; j++) {
			if (options [j]) {
				adjustOptions (j);
			}
		}
		if (!sat)
			ButtonSelectX = new List<int> (ButtonSelect1);
		else
			ButtonSelectX = new List<int> (ButtonSelect2);
		foreach (int i in ButtonSelectX) {
			Debug.Log ("ONE IS " + i);
			adjustOptions (i);
		}
		for (int k = 0; k < 4; k++) {
			if (!SatSelect)
				buttons[k].interactable = sat0connect;
			else
				buttons[k].interactable = sat1connect;
		}
	}
	public void adjustOptions(int i){
		options [i] = !options [i];
		if (options [i]) {
			Highlight (buttons [i], 0.2f);
			if (!ButtonSelectX.Contains (i))
				ButtonSelectX.Add (i);
		} else {
			Highlight (buttons [i], -0.2f);
			if (ButtonSelectX.Contains (i))
				ButtonSelectX.Remove (i);
		}
		if (ButtonSelectX.Count > 2) {
			options [ButtonSelectX [0]] = !options [ButtonSelectX [0]];
			Highlight (buttons [ButtonSelectX [0]], -0.2f);
			ButtonSelectX.Remove (ButtonSelectX [0]);
		}
	}
	void Highlight(Button b, float f){
		ColorBlock c3 = new ColorBlock();
		Color c = b.colors.normalColor;
		c.b += f;
		c.g += f;
		c.r += f;
		c3.normalColor = new Color (c.r, c.g, c.b, c.a);
		c.b -= 0.2f;
		c.g -= 0.2f;
		c.r -= 0.2f;
		c3.highlightedColor = new Color (c.r, c.g, c.b, c.a);
		if (f > 0) {
			c3.highlightedColor = new Color (c.r + 0.2f, c.g + 0.2f, c.b + 0.2f, c.a);
			c3.disabledColor = new Color (0.5f, 0.5f, 0.5f, 1f);
		} else
			c3.disabledColor = new Color (0.1f, 0.1f, 0.1f, 1f);
		c.b -= 0.2f;
		c.g -= 0.2f;
		c.r -= 0.2f;
		c3.pressedColor = new Color (c.r, c.g, c.b, c.a);
		c3.colorMultiplier = 1;
		c3.fadeDuration = 0.1f;
		b.colors = c3;
	}
	public void Execute(){
		if (!SatSelect)
			ButtonSelect1 = new List<int> (ButtonSelectX);
		else
			ButtonSelect2 = new List<int> (ButtonSelectX);
	}
	public void BeginRun(){
		StartCoroutine (Timer ());
	}
	public IEnumerator Timer(){
		Startpoint ();
		yield return new WaitForSeconds (23);
		Checkpoint1 ();
		yield return new WaitForSeconds (46);
		Checkpoint2 ();
		yield return new WaitForSeconds (69);
		Endpoint ();
	}
	void Startpoint(){
//		UnityEngine.Random rand = new UnityEngine.Random ();
		weaknesses [UnityEngine.Random.Range(0,4)] = true;
		int i = UnityEngine.Random.Range (0, 4);
		while (weaknesses [i] == true)
			i = UnityEngine.Random.Range (0, 4);
		weaknesses [i] = true;
	}
	void Checkpoint1(){
		sat0connect = false;
		ButtonSelect1.Sort ();
		foreach (int i in ButtonSelect1)
			if (weaknesses [i])
				Debug.Log ("We can " + Decode (i) + " the message with a chance of " + Chance (i) * 100 + " percent!");
			else
				Debug.Log ("The message can't be " + Decode (i) + "d! We'll have to try another way!");
	}
	void Checkpoint2(){
		sat1connect = false;
		ButtonSelect2.Sort ();
		foreach (int i in ButtonSelect2)
			if (weaknesses [i] && UnityEngine.Random.Range (0, 100) < Chance (i) * 100) {
				fubar++;
				Debug.Log ("We were able to successfully " + Decode (i) + " the message!");
			} else
				Debug.Log ("Our " + Decode (i) + " attempt failed!");
	}
	void Endpoint(){
		
	}
	string Decode(int i){
		switch (i) {
		case 0:
			return "Dissipate";
		case 1:
			return "Neutralize";
		case 2:
			return "Scramble";
		case 3:
			return "Rephrase";
		}
		return "";
	}
	float Chance(int i){
		switch (i) {
		case 0:
			return 0.6f;
		case 1:
			return 0.8f;
		case 2:
			return 0.9f;
		case 3:
			return 0.7f;
		}
		return 0f;
	}
}


