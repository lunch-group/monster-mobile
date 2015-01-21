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
	private string[] mStateStrings = new string[] { "IDLE", "MOVE", "BUILD", "REPAIR", "PATROL", "SPECIAL" };

	public enum SurvivorAttackType
	{
		INVALID = -1,
		MELEE,
		RANGED
	};
	public SurvivorAttackType AttackType;

	public GameObject instructionMenu = null;
	private MenuNotifications mInstructionsMenu = null;

	public int MaxHealth = 10;

	// Movement.
	private Vector3 mDestination;
	public float Speed = 1.0f;
	private bool mIsPendingMove = false;
	private bool mIsMoving = false;

	// Building.
	private bool mIsPendingBuild = false;
	private Buildingpoint mBuildingPoint = null;

	virtual protected void Start ()
	{
		if (instructionMenu != null)
		{
			mInstructionsMenu = instructionMenu.GetComponent<MenuNotifications>();
		}

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
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit))
					{
						mDestination = hit.point;
						mDestination.y = transform.position.y;
						Debug.Log("SURVIVOR: MOVE: Set destination to (" + mDestination + ")");

						mIsPendingMove = false;
						mIsMoving = true;
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
					mIsPendingBuild = false;
					mDestination = mBuildingPoint.transform.position;

					// Offset to the nearest corner of the object.
					mDestination.x += (transform.position.x > mDestination.x) ? 1.0f : -1.0f;
					mDestination.y = transform.position.y;
					mDestination.z += (transform.position.z > mDestination.z) ? 1.0f : -1.0f;
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

	virtual protected void OnGUI()
	{
		// Check all body parts for a Clickable component.
		Clickable[] clickers = GetComponentsInChildren<Clickable>() as Clickable[];
		foreach (Clickable c in clickers)
		{
			if (c.IsClicked())
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
					SetState((SurvivorState)selection);
					c.SetIsClicked(false);
				}
			}
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
			break;

		case SurvivorState.MOVE:
			mIsPendingBuild = false;

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
		
		case SurvivorState.REPAIR:	// fall through
		case SurvivorState.PATROL:	// fall through
        case SurvivorState.SPECIAL:
			state = SurvivorState.IDLE;
			s = "State not implemented.";
			break;
		
		default:
			Debug.LogError("SURVIVOR: SetState - INVALID STATE");
			break;
		}

		if (mInstructionsMenu != null)
		{
			mInstructionsMenu.SetText(s);
		}

		mState = state;
	}
}
