using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class WordScrambler : MonoBehaviour {
	private Text ScrambleText;
	private Dictionary<int,string> words;
	private string CurString;
	private string scrambledword;
	private InputField Answer;
	private bool isCorrect;
	private Text ConfirmationText;
	private List<int> positions;
	void AddWordToDictionary()
	{
		words= new Dictionary<int,string>();
		//words with two different anagram configurations
		words.Add (1, "act");
		words.Add (2, "cat");

		words.Add (3, "coat");
		words.Add (4, "taco");

		words.Add (5, "idea");
		words.Add (6, "aide");

		words.Add (90, "rescued");
		words.Add (91, "secured");
		//words with at least three different anagram configurations
		words.Add (7, "lots");
		words.Add (8, "slots");
		words.Add (9, "lost");

		words.Add (10, "kids");
		words.Add (11, "skid");
		words.Add (12, "disk");

		words.Add (20, "evil");
		words.Add (21, "live");
		words.Add (22, "vile");

		words.Add (27, "kale");
		words.Add (28, "lake");
		words.Add (29, "leak");

		words.Add (30, "list");
		words.Add (31, "slit");
		words.Add (32, "silt");

		words.Add (33, "meat");
		words.Add (34, "team");
		words.Add (35, "tame");

		words.Add (36, "pale");
		words.Add (37, "leap");
		words.Add (38, "plea");

		words.Add (44, "scar");
		words.Add (45, "cars");
		words.Add (46, "arcs");

		words.Add (47, "pools");
		words.Add (48, "loops");
		words.Add (49, "spool");

		words.Add (50, "relay");
		words.Add (51, "early");
		words.Add (52, "layer");

		words.Add (53, "reset");
		words.Add (54, "steer");
		words.Add (55, "trees");

		words.Add (56, "saint");
		words.Add (57, "stain");
		words.Add (58, "satin");

		words.Add (71, "caller");
		words.Add (72, "recall");
		words.Add (73, "cellar");

		words.Add (74, "remain");
		words.Add (75, "airmen");
		words.Add (76, "marine");

		words.Add (77, "plates");
		words.Add (78, "petals");
		words.Add (79, "staple");

		words.Add (84, "burden");
		words.Add (85, "burned");
		words.Add (86, "unbred");

		words.Add (87, "present");
		words.Add (88, "repents");
		words.Add (89, "serpent");

		words.Add (92, "won");
		words.Add (93, "own");
		words.Add (94, "now");
		//words with at least four different anagram configurations
		words.Add(13, "east");
		words.Add (14, "eats");
		words.Add (15, "seat");
		words.Add (16, "teas");

		words.Add (17, "edit");
		words.Add (18, "diet");
		words.Add (19, "tide");

		words.Add (23, "inks");
		words.Add (24, "skin");
		words.Add (25, "sink");
		words.Add (26, "kins");

		words.Add (59, "resin");
		words.Add (60, "siren");
		words.Add (61, "rinse");
		words.Add (62, "risen");

		words.Add (63, "slime");
		words.Add (64, "limes");
		words.Add (65, "miles");
		words.Add (66, "smile");

		words.Add (67, "trace");
		words.Add (68, "cater");
		words.Add (69, "crate");
		words.Add (70, "react");

		words.Add (80, "reared");
		words.Add (81, "reread");
		words.Add (82, "reader");
		words.Add (83, "dearer");

		//words with at least five different anagram configuations
		words.Add(39, "pots");
		words.Add (40, "post");
		words.Add (41, "spot");
		words.Add (42, "stop");
		words.Add (43, "tops");
	}
	// Use this for initialization
	void Start () {
		isCorrect = false;
		AddWordToDictionary ();
		ScrambleText = GameObject.Find ("WordPuzzleText").gameObject.GetComponent<Text> ();
		Answer = GameObject.Find ("AnswerField").gameObject.GetComponent<InputField> ();
		ConfirmationText = GameObject.Find ("ConfirmationText").gameObject.GetComponent<Text> ();
		words.TryGetValue(Random.Range(1,words.Count+1),out CurString);
		//Debug.Log (CurString);
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
					}
				}
			});
		scrambledword = ScrambleWord (CurString);
		//Debug.Log (scrambledword);
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
		//Debug.Log (wordsize);
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
		yield return new WaitForSeconds (5);
		ConfirmationText.text = "";
		isCorrect = false;
		words.TryGetValue(Random.Range(1,words.Count+1),out CurString);
		//Debug.Log (CurString);
		Answer.text = "";
		Answer.characterLimit = CurString.Length;
		scrambledword = ScrambleWord (CurString);
		//Debug.Log (scrambledword);
	}
}
