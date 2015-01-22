using UnityEngine;
using System.Collections;


public class MenuNotifications : MonoBehaviour
{
	public Vector2 SizeWidthHeight;
	public float OffsetY = 10;
	public Color color = Color.white;
	public GUISkin skin = null;
	public float Timeout = 5.0f;
	private float mTimeShown = 0.0f;

	private string mText = "";
	public void SetText(string s)
	{
		mText = s;
		mTimeShown = 0.0f;
	}

	void Start ()
	{
	}
	
	void Update ()
	{
	}
	
	void OnGUI()
	{
		if (mTimeShown < Timeout)
		{
			mTimeShown += Time.deltaTime;

			if (mText != "")
			{
				Rect r = new Rect(Screen.width / 2 - SizeWidthHeight.x / 2, OffsetY, SizeWidthHeight.x, SizeWidthHeight.y);
				GUI.contentColor = color;
				
				GUI.Box(r, mText);
			}
		}
	}
}