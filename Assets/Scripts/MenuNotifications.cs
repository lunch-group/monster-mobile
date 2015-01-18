using UnityEngine;
using System.Collections;


public class MenuNotifications : MonoBehaviour
{
	public Vector2 SizeWidthHeight;
	public Color color = Color.white;
	public GUISkin skin = null;

	private string mText = "";
	public void SetText(string s)
	{
		mText = s;
	}

	void Start ()
	{
	}
	
	void Update ()
	{
	}
	
	void OnGUI()
	{
		if (mText != "")
		{
			Rect r = new Rect(Screen.width / 2 - SizeWidthHeight.x / 2, 10, SizeWidthHeight.x, SizeWidthHeight.y);
			GUI.contentColor = color;
			
			GUI.Box(r, mText);
		}
	}
}