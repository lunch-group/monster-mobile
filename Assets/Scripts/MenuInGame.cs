using UnityEngine;
using System.Collections;


public class MenuInGame : MonoBehaviour
{
	public Vector2 OffsetXY;
	public Vector2 SizeWidthHeight;
	public Color color = Color.white;
	public GUISkin skin = null;

	public enum INGAME_STATE
	{
		INGAME_STATE_INVALID = -1,
		INGAME_STATE_PREPARE,
		INGAME_STATE_DEFEND,
		INGAME_STATE_OUTCOME
	};
	private INGAME_STATE mState = INGAME_STATE.INGAME_STATE_PREPARE;
	public INGAME_STATE GetState() { return mState; }

	private INGAME_STATE mLastState = INGAME_STATE.INGAME_STATE_INVALID;
	public INGAME_STATE GetLastState() { return mLastState; }

	private string mButtonText = "";


	void Start ()
	{
	
	}
	
	void Update ()
	{
		mLastState = mState;
	}

	void OnGUI()
	{
		Rect r = new Rect(Screen.width - OffsetXY.x - SizeWidthHeight.x, OffsetXY.y, SizeWidthHeight.x, SizeWidthHeight.y);
		GUI.contentColor = color;

		if (GUI.Button (r, mButtonText))
		{
			Debug.Log ("INGAMEMENU: MouseButtonDown - SetNextState...");
			SetNextState();
		}

		switch (mState)
		{
			case INGAME_STATE.INGAME_STATE_PREPARE:
			{
			mButtonText = "PREPARE > DEFEND";
			}
				break;
			
			case INGAME_STATE.INGAME_STATE_DEFEND:
			{
			mButtonText = "DEFEND > RETREAT";
			}
				break;
			
			case INGAME_STATE.INGAME_STATE_OUTCOME:
			{
			mButtonText = "OUTCOME > PREPARE";
				break;
			}
		}
	}

	void SetNextState()
	{
		switch (mState)
		{
			case INGAME_STATE.INGAME_STATE_PREPARE:
				Debug.Log ("INGAMEMENU: State changed from PREPARE to DEFEND");
				mState = INGAME_STATE.INGAME_STATE_DEFEND;
				break;
			case INGAME_STATE.INGAME_STATE_DEFEND:
				Debug.Log ("INGAMEMENU: State changed from DEFEND to PREPARE");
				mState = INGAME_STATE.INGAME_STATE_PREPARE;
				break;
			case INGAME_STATE.INGAME_STATE_OUTCOME:
				Debug.Log ("INGAMEMENU: State changed from OUTCOME to PREPARE");
				mState = INGAME_STATE.INGAME_STATE_PREPARE;
				break;
		}
	}

	public void SetDayComplete()
	{
		mState = INGAME_STATE.INGAME_STATE_OUTCOME;
		mLastState = INGAME_STATE.INGAME_STATE_DEFEND;
	}
}
