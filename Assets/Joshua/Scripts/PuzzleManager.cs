﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PuzzleManager : MonoBehaviour {
	GameObject Rearrange;
	Button RearrangeButton;
	GameObject CaesarCiph;
	Button CipherButton;
	public bool isRearr;
	public bool isCiph;
	CaesarCipher ciph;
	WordScrambler scramble;
	// Use this for initialization
	void Start () {
		isRearr = false;
		isCiph = false;
		ciph = GameObject.Find ("CaesarCipher").GetComponent<CaesarCipher>();
		scramble = GameObject.Find ("WordPuzzle").GetComponent<WordScrambler>();
		Rearrange = GameObject.Find ("WordPuzzle");
		CaesarCiph = GameObject.Find ("CaesarCipher");
		RearrangeButton = GameObject.Find ("Rearrange").GetComponent<Button> ();
		CipherButton = GameObject.Find ("CaesarCiph").GetComponent<Button> ();
		Rearrange.SetActive (false);
		CaesarCiph.SetActive (false);
	}
	void Update()
	{
		if (isRearr) 
			RearrangeButton.interactable = false;
		else if(isRearr==false)
			RearrangeButton.interactable = true;
		if (isCiph)
			CipherButton.interactable = false;
		else if (isCiph==false)
			CipherButton.interactable = true;
	}
	public void ActivateRearrange()
	{
		Rearrange.SetActive (true);
		isRearr = true;
		if(ciph.gameObject.activeSelf)
			ciph.StartCoroutine (ciph.DeActivateCiph());
	}
	public void ActivateCipher()
	{
		CaesarCiph.SetActive (true);
		isCiph = true;
		if(scramble.gameObject.activeSelf)
			scramble.StartCoroutine (scramble.DeActivateRearrange());
	}
}
