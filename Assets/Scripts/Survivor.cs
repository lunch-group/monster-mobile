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
	private string[] mStateStrings = new string[] { "IDLE", "MOVE", "BUILD", "REPAIR", "PATROL", "SPECIAL" };
	protected SurvivorState mState;
	public SurvivorState GetState() { return mState; }

	public enum SurvivorAttackType
	{
		INVALID = -1,
		MELEE,
		RANGED
	};
	public SurvivorAttackType AttackType;

	public int MaxHealth = 10;

	// Movement.
	private Vector3 mDestination;
	public float Speed = 1.0f;
	private bool mIsPendingMove = false;
	private bool mIsMoving = false;

	// Building.
	private bool mIsPendingBuild = false;
	private Buildingpoint mBuildingPoint = null;

	// Repairing.
	private bool mIsPendingRepair = false;
	private Buildable mRepairPoint = null;

	private PlayerManager mPlayerManager = null;

	public int AttackRadius = 10;
    
    [Tooltip("List of building types this player can build.")]
    public GameObject[] Buildables;

	private bool mIsClicked = false;

	public string PlayerName;


	virtual protected void Start ()
	{
		GameObject[] managers = GameObject.FindGameObjectsWithTag("Manager");
		foreach (GameObject go in managers)
		{
			if (go.GetComponent<PlayerManager>())
			{
				mPlayerManager = go.GetComponent<PlayerManager>();
			}
		}

		PlayerName = this.gameObject.name;

		mState = SurvivorState.IDLE;
	}
	
	virtual protected void Update ()
	{
		switch (mState)
		{
		case SurvivorState.IDLE:
			break;

		case SurvivorState.MOVE:
			// Already moving? Keep moving.
			if (mIsMoving)
			{
				MoveToDestination();

				if (transform.position.Equals(mDestination))
				{
					SetState(SurvivorState.IDLE);
				}
			}

			// Update destination as needed.
			if (mIsPendingMove)
			{
				if (Input.GetMouseButtonDown(0))
				{
					// Don't count clicks on the ExitButton.
					ExitButton eb = GameObject.Find("ExitButton").GetComponent<ExitButton>();
					if (Input.mousePosition.x > eb.size && Input.mousePosition.y > eb.size)
					{
						Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
						RaycastHit hit;
						if (Physics.Raycast(ray, out hit))
						{
							mDestination = hit.point;
							mDestination.y = transform.position.y;
							Debug.Log("SURVIVOR: MOVE: Set destination to (" + mDestination + ")");

							mIsPendingMove = false;
							mIsMoving = true;

							// Set this player as inactive to prevent being able to build while moving.
							mPlayerManager.SetActivePlayer(null);
						}
					}
					else
					{
						Debug.Log("player move clicked on X");
					}
				}
				else if (Input.GetMouseButtonDown(1))
				{
					SetState(SurvivorState.IDLE);
				}
			}
			break;
		
		case SurvivorState.BUILD:
			if (mIsPendingBuild)
			{
				// Building points disappear after use, so assume these are all unused.
				GameObject[] bpgos = GameObject.FindGameObjectsWithTag("BuildPoint");
				ArrayList bpsReady = new ArrayList();
				foreach (GameObject bpgo in bpgos)
				{
					Buildingpoint bp = bpgo.GetComponent<Buildingpoint>();
					if (bp && bp.IsReadyToBuild())
					{
						bpsReady.Add(bpgo);
					}
				}

				if (bpsReady.Count > 0)
				{
					mBuildingPoint = (bpsReady[0] as GameObject).GetComponent<Buildingpoint>();
					mBuildingPoint.SetPendingBuild();
                    mIsPendingBuild = false;

					mDestination = GetNearestCorner(mBuildingPoint.transform.position);

					mPlayerManager.SetActivePlayer(null);
				}
			}
			else
			{
				MoveToDestination();
				if (transform.position.Equals(mDestination))
				{
					mBuildingPoint.Build(this.transform, null);
					SetState(SurvivorState.IDLE);
				}
			}
			break;
		
		case SurvivorState.REPAIR:
		{
			if (mIsPendingRepair)
			{
				GameObject[] bgos = GameObject.FindGameObjectsWithTag("Building");
				foreach (GameObject bgo in bgos)
				{
					Buildable bc = bgo.GetComponent<Buildable>();
					if (bc && bc.Repairable)
					{
						if (bc.IsReadyToRepair())
						{
							mIsPendingRepair = false;
							mRepairPoint = bc;
							mDestination = GetNearestCorner(mRepairPoint.transform.position);
						}
					}
				}
			}
			else
			{
				MoveToDestination();
				if (transform.position.Equals(mDestination))
				{
					mRepairPoint.Repair();
					mRepairPoint = null;

					SetState(SurvivorState.IDLE);
				}
			}
		}
			break;
		
		case SurvivorState.PATROL:
			break;

		case SurvivorState.SPECIAL:
			break;

		default:
			break;
		}
	}

	void MoveToDestination()
	{
		float stepSize = Time.deltaTime * Speed;
		transform.position = Vector3.MoveTowards(transform.position, mDestination, stepSize);
	}

	GameObject GetClosestObject(ArrayList objs)
	{
		GameObject closest = null;

		if (objs.Count > 0)
		{
			closest = objs[0] as GameObject;
			float leastDistance = Vector3.Distance(transform.position, closest.transform.position);

			for (int i = 1; i < objs.Count; ++i)
			{
				GameObject go = objs[i] as GameObject;
				float distance = Vector3.Distance(transform.position, go.transform.position);
				if (distance < leastDistance)
				{
					closest = go;
					leastDistance = distance;
				}
			}
		}

		return closest;
	}

	// Offset to the nearest corner of the object.
	Vector3 GetNearestCorner(Vector3 dest)
	{
		Vector3 result = dest;

		result.x += (transform.position.x > result.x) ? 1.0f : -1.0f;
		result.y = transform.position.y;
		result.z += (transform.position.z > result.z) ? 1.0f : -1.0f;

		return result;
	}

	virtual protected void OnGUI()
	{
		// Don't allow updates in certain states.
		if (mState == SurvivorState.BUILD)
		{
			return;
		}

		if (mIsClicked)
		{
			GUI.contentColor = Color.yellow;

			// Get GUI coordinates from 3D position on screen.
			Vector3 v = Camera.main.WorldToScreenPoint(transform.position);
			v = new Vector2(v.x, Screen.height - v.y);

			int gridColumns = 2;
			Vector2 rectSize = new Vector2(150, 30 * mStateStrings.Length / gridColumns);
			Rect r = new Rect(v.x - rectSize.x/2, v.y, rectSize.x, rectSize.y);

			int selection = -1;
			selection = GUI.SelectionGrid(r, selection, mStateStrings, gridColumns);
			if (selection >= 0)
			{
				mPlayerManager.SetActivePlayer(gameObject);
				SetState((SurvivorState)selection);

				// Unclick this player.
				mIsClicked = false;
				Clickable[] clickers = GetComponentsInChildren<Clickable>() as Clickable[];
				foreach (Clickable cc in clickers)
				{
					cc.IsClicked = false;
				}
			}
		}
	}

	void OnClick()
	{
		mIsClicked = true;

		GameObject selectionUI = GameObject.Find("SelectionText");
		if (selectionUI != null)
		{
			string s = PlayerName + "\n";

			Health hc = GetComponent<Health>();
			if (hc != null)
			{
				s += "Health: " + hc.CurrentHealth + "/" + hc.MaxHealth;
			}
			
			selectionUI.GetComponent<MenuNotifications>().SetText(s);
		}
	}
	void OnUnclicked()
	{
		mIsClicked = false;
	}
	void OnExitClicked()
	{
		mIsClicked = false;

		if (mState == SurvivorState.MOVE && mIsPendingMove)
		{
			mIsPendingMove = false;
			SetState(SurvivorState.IDLE);
			mPlayerManager.SetActivePlayer(null);
		}
		if (mState == SurvivorState.BUILD && mIsPendingBuild)
		{
			mIsPendingBuild = false;
			SetState(SurvivorState.IDLE);
			mPlayerManager.SetActivePlayer(null);
		}
		else if (mState == SurvivorState.REPAIR && mIsPendingRepair)
		{
			mIsPendingRepair = false;
			SetState(SurvivorState.IDLE);
			mPlayerManager.SetActivePlayer(null);
		}
	}

	virtual protected void SetState(SurvivorState state)
	{
		// Don't bother changing to the same state.
		if (state == mState)
		{
			return;
		}

		Debug.Log("SURVIVOR: State changed from " + mState + " to " + state);

		// Instruction text, set based on state.
		string s = "";

		switch (state)
		{
		case SurvivorState.IDLE:
			mPlayerManager.SetActivePlayer(null);
			break;

		case SurvivorState.MOVE:
			mIsPendingBuild = false;
			mIsPendingRepair = false;

			// Forget the last destination.
			mIsMoving = false;
			mDestination = transform.position;

			mIsPendingMove = true;

			s = "Click anywhere to move.";
			break;

		case SurvivorState.BUILD:
			GameObject[] bpgos = GameObject.FindGameObjectsWithTag("BuildPoint");
			if (bpgos.Length > 0)
			{
				mIsPendingMove = false;
				mIsMoving = false;
				mIsPendingBuild = true;

				s = "Click on a BuildPoint\nto construct something.";
			}
			else
			{
				s = "Nothing left to build!";
				state = mState;
			}
			break;
		
		case SurvivorState.REPAIR:
		{
			mIsPendingMove = false;
			mIsMoving = false;
			mIsPendingBuild = false;

			mIsPendingRepair = true;
			s = "Click on a Building\nto repair it.";
		}
			break;

		case SurvivorState.PATROL:	// fall through
        case SurvivorState.SPECIAL:
			mPlayerManager.SetActivePlayer(null);
			state = SurvivorState.IDLE;
			s = "State not implemented.";
			break;
		
		default:
			Debug.LogError("SURVIVOR: SetState - INVALID STATE");
			break;
		}

		GameObject instructionUI = GameObject.Find("Instructions");
		if (instructionUI != null)
		{
			instructionUI.GetComponent<MenuNotifications>().SetText(s);
		}

		mState = state;
	}
}
