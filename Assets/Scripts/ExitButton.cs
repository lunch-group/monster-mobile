using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour
{
	private bool mShowing;
	public void Show(bool s) { mShowing = s; }


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
			Rect r = new Rect(0, 0, 30, 30);
			if (GUI.Button(r, "X"))
			{
				// Close all GUIs

			}
		}
	}
}
