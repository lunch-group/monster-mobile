using UnityEngine;
using System.Collections;

public class Survivor : MonoBehaviour
{
	public enum SurvivorState
	{
		INVALID = -1,
		IDLE,
		MOVE,
		BUILD,
		REPAIR,
		PATROL,
		SPECIAL
	};
	protected SurvivorState mState;

	public enum SurvivorAttackType
	{
		INVALID = -1,
		MELEE,
		RANGED
	};
	public SurvivorAttackType AttackType;

	public int MaxHealth = 10;

	public GUISkin Skin = null;


	virtual protected void Start ()
	{
		mState = SurvivorState.IDLE;
	}
	
	virtual protected void Update ()
	{
	}

	virtual protected void OnGUI()
	{
		Clickable[] clickers = GetComponentsInChildren<Clickable>() as Clickable[];
		foreach (Clickable c in clickers)
		{
			if (c.IsClicked())
			{
				GUI.skin = Skin;

				// Get GUI coordinates from 3D position on screen.
				Vector3 v = Camera.main.WorldToScreenPoint(transform.position);
				v = new Vector2(v.x, Screen.height - v.y);

				GUI.contentColor = Color.yellow;
				Vector2 rectSize = new Vector2(100, 30);
				if (GUI.Button (new Rect(v.x - rectSize.x/2, v.y - rectSize.y * 2, rectSize.x, rectSize.y), "MOVE"))
				{
					SetState(SurvivorState.MOVE);
					c.SetIsClicked(false);
				}
				else if (GUI.Button (new Rect(v.x - rectSize.x/2, v.y - rectSize.y, rectSize.x, rectSize.y), "BUILD"))
				{
					SetState(SurvivorState.BUILD);
					c.SetIsClicked(false);
				}
				else if (GUI.Button (new Rect(v.x - rectSize.x/2, v.y, rectSize.x, rectSize.y), "REPAIR"))
				{
					SetState(SurvivorState.REPAIR);
					c.SetIsClicked(false);
				}
				else if (GUI.Button (new Rect(v.x - rectSize.x/2, v.y + rectSize.y, rectSize.x, rectSize.y), "PATROL"))
				{
					SetState(SurvivorState.PATROL);
					c.SetIsClicked(false);
				}
				else
				{
					GUI.contentColor = Color.red;
					rectSize = new Vector2(100, 30);
					if (GUI.Button (new Rect(v.x - rectSize.x/2, v.y + rectSize.y * 2, rectSize.x, rectSize.y), "CANCEL"))
					{
						c.SetIsClicked(false);
					}	
				}
			}
		}
	}

	virtual protected void SetState(SurvivorState state)
	{
		Debug.Log("SURVIVOR: State changed from " + mState + " to " + state);
		mState = state;
	}
}
