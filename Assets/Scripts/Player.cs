using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public GUISkin skin = null;
	
	public static int gold = 3; // start gold
	
	void OnGUI()
	{
		GUI.skin = skin;
		
		// draw gold
		GUI.Label(new Rect(0, Screen.height - 60, 100, 60), "Gold: " + gold);
	}
}