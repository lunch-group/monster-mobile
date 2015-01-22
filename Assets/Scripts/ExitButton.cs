using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour
{
	private bool mShowing = true;
	public void Show(bool s) { mShowing = s; }
	public int size = 30;


	void Start ()
	{
	}
	
	void Update ()
	{
	}

	void OnGUI()
	{
		if (mShowing)
		{
			Rect r = new Rect(0, 0, size, size);
			if (GUI.Button(r, "X"))
			{
				Debug.Log("ExitButton clicked.");

				// Close all clickable GUIs.
				Clickable[] clickableList = FindObjectsOfType(typeof(Clickable)) as Clickable[];
				foreach (Clickable c in clickableList)
				{
					c.Unclick();
				}
			}
		}
	}
}
