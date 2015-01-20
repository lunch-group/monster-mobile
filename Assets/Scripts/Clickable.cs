using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour
{
	bool mIsClicked = false;
	public bool IsClicked()
	{
		return mIsClicked;
	}
	public void SetIsClicked(bool c)
	{
		mIsClicked = c;
	}
	
	public void OnMouseUpAsButton()
	{		
		// Close all other clickables' GUIs.
		Clickable[] clickableList = FindObjectsOfType(typeof(Clickable)) as Clickable[];
		foreach (Clickable c in clickableList)
		{
			c.SetIsClicked(false);
		}

		mIsClicked = true;
		Debug.Log ("CLICKABLE: Clicked on " + this.name);
	}
}
