using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	public TextAlignment Alignment = TextAlignment.Center;
	public float OffsetX = 0.0f;
	public float OffsetY = 0.0f;
	public float ButtonWidth = 300.0f;
	public float ButtonHeight = 30.0f;
	public float LogoScale = 1.0f;

	public string SceneName = "scene_main";
	
	public Texture2D Logo = null;
	
	void OnGUI()
	{
		Rect r;

		switch (Alignment)
		{
		case TextAlignment.Left:
			r = new Rect(OffsetX, OffsetY, ButtonWidth, ButtonHeight);
			break;

		case TextAlignment.Right:
			r = new Rect(Screen.width - OffsetX, OffsetY, ButtonWidth, ButtonHeight);
			break;

		case TextAlignment.Center: // fall through
		default:
			r = new Rect(Screen.width / 2 - (ButtonWidth / 2), Screen.height / 2 - (ButtonHeight / 2), ButtonWidth, ButtonHeight);
			break;
		}

		
		// draw logo at the top right of the menu
		if (Logo != null)
		{
			Rect l = new Rect(Screen.width / 2.0f - Logo.width * (LogoScale/2.0f), OffsetY + Logo.height * (LogoScale/2.0f),
			                  Logo.width*LogoScale, Logo.height*LogoScale);
			GUI.DrawTexture(l, Logo);
		}
		
		// draw the menu
		if (GUI.Button(r, "Play"))
		{
			Application.LoadLevel(SceneName);
		}

		r.y += ButtonHeight;
		if (GUI.Button(r, "Quit"))
		{
			Application.Quit();
		}
	}
}