using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class CaesarCipher : WordPuzzles {
	private string CurString;
	Text CaesarText;
	PuzzleManager manag;
	private GameObject Ciph;
	string CaesarString;
	public int CaesarScore = 0;
	private bool isCorrect;
	private Text ConfirmationText;
	private InputField Answer;
	char[] alphabet = {
		'a',
		'b',
		'c',
		'd',
		'e',
		'f',
		'g',
		'h',
		'i',
		'j',
		'k',
		'l',
		'm',
		'n',
		'o',
		'p',
		'q',
		'r',
		's',
		't',
		'u',
		'v',
		'w',
		'x',
		'y',
		'z'
	};
	// Use this for initialization
	void Start () {
		manag = GameObject.Find ("PuzzleManager").GetComponent<PuzzleManager> ();
		isCorrect = false;
		Ciph = this.gameObject;
		AddWordToDictionary ();
		CaesarText = GameObject.Find ("CipherPuzzleText").gameObject.GetComponent<Text> ();
		Answer = GameObject.Find ("CiphAnswerField").gameObject.GetComponent<InputField> ();
		ConfirmationText = GameObject.Find ("CipherConfirmationText").gameObject.GetComponent<Text> ();
		words.TryGetValue(Random.Range(1,words.Count+1),out CurString);
		MakeCipher (CurString);
		//Debug.Log (CurString+" "+CaesarString+" ");
		Answer.characterLimit = CurString.Length;
		CaesarText.text = CaesarString;
		Answer.onEndEdit.AddListener(val =>
			{
				if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
				{
					isCorrect = IsWordCorrect(Answer.text);
					if(isCorrect==false)
						PrintIncorrect();
					if (isCorrect==true)
					{
						PrintSuccess();
						StartCoroutine(GenerateNewWord());
						StartCoroutine(DeActivateCiph());
					}
				}
			});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void MakeCipher(string word)
	{
		int CaesarTranslate;
		CaesarString = word;
		CaesarTranslate = Random.Range (1, 26);
		//Debug.Log (CaesarTranslate);
		for (int i = 0; i < word.Length; i++) {
			CaesarString= ReplaceCharacter (i, CaesarString,CaesarTranslate);
		}
	}
	string ReplaceCharacter(int index, string word,int offset)
	{
		StringBuilder sb = new StringBuilder (word);
		int j = sb [index];
		int i = offset + sb [index] - 97;
		if (i >= 26)
			i -= 26;
		sb [index] = alphabet [i];
		return sb.ToString();
	}
	private bool IsWordCorrect(string word)
	{
		if (word == CurString)
			return true;
		return false;
	}
	private void PrintIncorrect()
	{
		string message = "";
		message = message + "Incorrect cipher!";
		ConfirmationText.text = message;
	}
	private void PrintSuccess()
	{
		string message = "";
		message = "You have successfully figured out the word!";
		ConfirmationText.text = message;
		CaesarScore++;
		//Debug.Log (CaesarScore);
	}
	IEnumerator GenerateNewWord()
	{
		yield return new WaitForSeconds (2);
		ConfirmationText.text = "";
		isCorrect = false;
		bool dou = false;
		while (!dou) {
			words.TryGetValue(Random.Range(1,words.Count+1),out CurString);
			foreach (char x in CurString)
				if (CurString.IndexOf (x) != CurString.LastIndexOf (x))
					dou = true;
		}

		Answer.text = "";
		Answer.characterLimit = CurString.Length;
		MakeCipher (CurString);
		CaesarText.text = CaesarString;
		//Debug.Log (CurString);
	}
	public IEnumerator DeActivateCiph()
	{
		if(isCorrect==false)
		yield return new WaitForSeconds (0);
		if (isCorrect == true)
			yield return new WaitForSeconds (2);
		manag.isCiph = false;
		Ciph.SetActive (false);
	}
}
