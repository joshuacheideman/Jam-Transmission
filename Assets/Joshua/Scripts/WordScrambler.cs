using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class WordScrambler : WordPuzzles {
	private Text ScrambleText;
	private string CurString;
	private string scrambledword;
	private List<int> positions;
	private GameObject Rearrange;
	PuzzleManager manag;
	// Use this for initialization
	void Start () {
		manag = GameObject.Find ("PuzzleManager").GetComponent<PuzzleManager> ();
		Rearrange = this.gameObject;
		isCorrect = false;
		AddWordToDictionary ();
		ScrambleText = GameObject.Find ("WordPuzzleText").gameObject.GetComponent<Text> ();
		Answer = GameObject.Find ("AnswerField").gameObject.GetComponent<InputField> ();
		ConfirmationText = GameObject.Find ("ConfirmationText").gameObject.GetComponent<Text> ();
		words.TryGetValue(Random.Range(1,words.Count+1),out CurString);
		Answer.characterLimit = CurString.Length;
		Answer.onEndEdit.AddListener(val =>
			{
				if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
				{
					isCorrect = IsWordCorrect(Answer.text);
					if(isCorrect==false)
						PrintMatchPositions();
					if (isCorrect==true)
					{
						PrintSuccess();
						StartCoroutine(GenerateNewWord());
						StartCoroutine(DeActivateRearrange());
					}
				}
			});
		scrambledword = ScrambleWord (CurString);
	}
	
	// Update is called once per frame
	void Update () {
		ScrambleText.text = scrambledword;
	}
	private string swap(int idx1,int idx2,string word)
	{
		StringBuilder sb = new StringBuilder (word);
		char temp = sb[idx1];
		sb[idx1] = sb[idx2];
		sb[idx2] = temp;
		return sb.ToString();
	}
	private string ScrambleWord (string word)
	{
		string newword = word;
		int wordsize = word.Length;
		for (int i = 0; i < 100; i++) 
		{
			int index1 = Random.Range (0, wordsize);
			int index2 = Random.Range (0, wordsize);
			while (index1 == index2) {
				index2 = Random.Range (0, wordsize);
			}
			newword = swap (index1, index2, newword);
		}
		if (newword == CurString) {
			int index1 = Random.Range (0, wordsize);
			int index2 = Random.Range (0, wordsize);
			while (index1 == index2) {
				index2 = Random.Range (0, wordsize);
			}
			newword = swap (index1, index2, newword);
		}
		return newword;
	}
	private bool IsWordCorrect(string word)
	{
		positions = new List<int> ();
		if (word == CurString)
			return true;
		for (int i = 0; i < word.Length; i++) {
			if (CurString [i] == word [i])
				positions.Add (i);
		}
		return false;
	}
	private void PrintMatchPositions()
	{
		string message = "";
		message = message + "Incorrect! You got "+positions.Count+" matches.";
		ConfirmationText.text = message;
		positions = null;//reset positions
	}
	private void PrintSuccess()
	{
		string message = "";
		message = "You have successfully figured out the word!";
		ConfirmationText.text = message;
	}
	IEnumerator GenerateNewWord()
	{
		yield return new WaitForSeconds (3);
		ConfirmationText.text = "";
		isCorrect = false;
		words.TryGetValue(Random.Range(1,words.Count+1),out CurString);
		Answer.text = "";
		Answer.characterLimit = CurString.Length;

		scrambledword = ScrambleWord (CurString);
	}
	IEnumerator DeActivateRearrange()
	{
		yield return new WaitForSeconds (3);	
		manag.isRearr = false;
		Rearrange.SetActive (false);
	}
}
