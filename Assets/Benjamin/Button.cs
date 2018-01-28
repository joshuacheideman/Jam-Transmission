using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {
		public Button[] buttons;
		private bool SatSelect;
		private bool[] options;
		public bool sat0connect;
		public bool sat1connect;
		private List<int> ButtonSelectX;
		private List<int> ButtonSelect1;
		private List<int> ButtonSelect2;
		void Start(){
				options = new bool[4];
				ButtonSelectX = new List<int> ();
				ButtonSelect1 = new List<int> ();
				ButtonSelect2 = new List<int> ();
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
								buttons [k].interactable = sat0connect;
						else
								buttons [k].interactable = sat1connect;
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
	}
