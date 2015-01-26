using UnityEngine;
using System.Collections;


public class MenuNotifications : MonoBehaviour
{
	public Vector2 SizeWidthHeight;
	public float OffsetY = 10;
	public bool OffsetFromTop = true;
	public Color TextColor = Color.white;
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
		if ((mTimeShown < Timeout) || (Timeout == -1.0f))
		{
			mTimeShown += Time.deltaTime;

			if (mText != "")
			{
				GUI.contentColor = TextColor;

				Rect r = new Rect(Screen.width / 2 - SizeWidthHeight.x / 2, 0, SizeWidthHeight.x, SizeWidthHeight.y);
				r.y = (OffsetFromTop) ? OffsetY : (Screen.height - OffsetY);
				
				GUI.Box(r, mText);
			}
		}
	}
}