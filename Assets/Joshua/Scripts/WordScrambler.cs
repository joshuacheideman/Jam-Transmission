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

		words.Add (91, "rescued");
		words.Add (92, "secured");
		//words with at least three different anagram configurations
		words.Add (7, "lots");
		words.Add (8, "slots");
		words.Add (9, "lost");

		words.Add (10, "kids");
		words.Add (11, "skid");
		words.Add (12, "disk");

		words.Add (21, "evil");
		words.Add (22, "live");
		words.Add (23, "vile");

		words.Add (28, "kale");
		words.Add (29, "lake");
		words.Add (30, "leak");

		words.Add (31, "list");
		words.Add (32, "slit");
		words.Add (33, "silt");

		words.Add (34, "meat");
		words.Add (35, "team");
		words.Add (36, "tame");

		words.Add (37, "pale");
		words.Add (38, "leap");
		words.Add (39, "plea");

		words.Add (45, "scar");
		words.Add (46, "cars");
		words.Add (47, "arcs");

		words.Add (48, "pools");
		words.Add (49, "loops");
		words.Add (50, "spool");

		words.Add (51, "relay");
		words.Add (52, "early");
		words.Add (53, "layer");

		words.Add (54, "reset");
		words.Add (55, "steer");
		words.Add (56, "trees");

		words.Add (57, "saint");
		words.Add (58, "stain");
		words.Add (59, "satin");

		words.Add (72, "caller");
		words.Add (73, "recall");
		words.Add (74, "cellar");

		words.Add (75, "remain");
		words.Add (76, "airmen");
		words.Add (77, "marine");

		words.Add (78, "plates");
		words.Add (79, "petals");
		words.Add (80, "staple");

		words.Add (85, "burden");
		words.Add (86, "burned");
		words.Add (87, "unbred");

		words.Add (88, "present");
		words.Add (89, "repents");
		words.Add (90, "serpent");

		words.Add (93, "won");
		words.Add (94, "own");
		words.Add (95, "now");
		//words with at least four different anagram configurations
		words.Add(13, "east");
		words.Add (14, "eats");
		words.Add (15, "seat");
		words.Add (16, "teas");

		words.Add (17, "edit");
		words.Add (18, "diet");
		words.Add (19, "tide");
		words.Add (20, "tide");

		words.Add (24, "inks");
		words.Add (25, "skin");
		words.Add (26, "sink");
		words.Add (27, "kins");

		words.Add (60, "resin");
		words.Add (61, "siren");
		words.Add (62, "rinse");
		words.Add (63, "risen");

		words.Add (64, "slime");
		words.Add (65, "limes");
		words.Add (66, "miles");
		words.Add (67, "smile");

		words.Add (68, "trace");
		words.Add (69, "cater");
		words.Add (70, "crate");
		words.Add (71, "react");

		words.Add (81, "reared");
		words.Add (82, "reread");
		words.Add (83, "reader");
		words.Add (84, "dearer");

		//words with at least five different anagram configuations
		words.Add(40, "pots");
		words.Add (41, "post");
		words.Add (42, "spot");
		words.Add (43, "stop");
		words.Add (44, "tops");
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
