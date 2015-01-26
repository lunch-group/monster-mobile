using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour
{
	public bool IsClicked = false;


	public void Unclick()
	{
		IsClicked = false;
        transform.SendMessage("OnUnclicked", SendMessageOptions.DontRequireReceiver);
		transform.parent.SendMessage("OnUnclicked", SendMessageOptions.DontRequireReceiver);
		transform.root.SendMessage("OnUnclicked", SendMessageOptions.DontRequireReceiver);
	}

	public void ExitButtonClicked()
	{
		IsClicked = false;
		transform.SendMessage("OnExitClicked", SendMessageOptions.DontRequireReceiver);
		transform.parent.SendMessage("OnExitClicked", SendMessageOptions.DontRequireReceiver);
		transform.root.SendMessage("OnExitClicked", SendMessageOptions.DontRequireReceiver);
	}
	
	public void OnMouseUpAsButton()
	{		
		// Unclick all other clickables.
		Clickable[] clickableList = FindObjectsOfType(typeof(Clickable)) as Clickable[];
		foreach (Clickable c in clickableList)
		{
			if (c != this)
			{
				c.Unclick();
			}
		}

		IsClicked = true;
		transform.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		transform.parent.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
		transform.root.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);

		Debug.Log ("CLICKABLE: Clicked on " + this.name);
	}
}
