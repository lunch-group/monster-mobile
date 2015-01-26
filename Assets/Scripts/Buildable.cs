using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour
{
	public float TimeToBuild = 5.0f;
	public bool IsStatic = false;

	private float mActualTimeToBuild;
	private float mTimeLeft;
	private bool mIsBuilding = false;
	public bool IsBuilding() { return mIsBuilding; }
	private bool mIsDone;
	public bool IsDone() { return mIsDone; }

	public bool Repairable = true;

	private bool mShowBuildGui;
	public void ShowBuildGui(bool s) { mShowBuildGui = s; }

	public Material BuildingMaterial = null;
	public Material BuildingMaterialDone = null;
	public GameObject BuildingParticle = null;
	//private Object mParticle = null;

	private string mName = "";
	private bool mIsSelected = false;

	private bool mIsReadyToRepair = false;
	public bool IsReadyToRepair() { return mIsReadyToRepair; }

	protected string mTypeName = "";
	public void SetTypeName(string s) { mTypeName = s; }


	void Start ()
	{
		if (IsStatic)
		{
			mTimeLeft = 0.0f;
			mIsDone = true;
			mIsBuilding = false;
			mShowBuildGui = false;
			mName = gameObject.name;
		}
		else
		{
			mActualTimeToBuild = TimeToBuild;

			Reset ();

			SetMaterial(BuildingMaterial);
			//mParticle = Instantiate(BuildingParticle, transform.position, Quaternion.identity);
		}
	}

	void Reset ()
	{
		mTimeLeft = mActualTimeToBuild;
		mIsBuilding = true;
		mIsDone = false;

		TextMesh tm = GetComponentInChildren<TextMesh>();
		tm.text = "";
	}

	public void StartBuilding(Transform builder, string type)
	{
		// TODO: Adjust build time based on builder's attributes.
		mTimeLeft = mActualTimeToBuild;

		mName = type;

		mIsBuilding = true;
		mIsDone = false;
	}
	
	void Update ()
	{
		if (mIsBuilding)
		{
			mShowBuildGui = true;

			mTimeLeft -= Time.deltaTime;
			if (mTimeLeft <= 0.0f)
			{
				mTimeLeft = 0.0f;
				mIsDone = true;
				mIsBuilding = false;
				mShowBuildGui = false;

				//BuildText.renderer.enabled = false;
				Debug.Log ("Finished building " + this.name);
				TextMesh tm = GetComponentInChildren<TextMesh>();
				tm.renderer.enabled = false;

				// Remove the effect.
				//Destroy(mParticle);

				SetMaterial(BuildingMaterialDone);
			}
		}
		else if (mIsSelected)
		{
			GameObject selectionUI = GameObject.Find("SelectionText");
			if (selectionUI != null)
			{
				string s = "";

				if (Repairable)
				{
					Health hc = GetComponent<Health>();
					if (hc != null)
					{
						s = mName + "\nDurability: " + hc.CurrentHealth + "/" + hc.MaxHealth;
					}
					else
					{
						s = mName + "\n-No Health Component-";
					}
				}
				else
				{
					s = mName + "\nINDESTRUCTIBLE";
				}

				selectionUI.GetComponent<MenuNotifications>().SetText(s);
			}
		}
	}

	protected void OnGUI ()
	{
		if (mShowBuildGui)
		{
			TextMesh tm = GetComponentInChildren<TextMesh>();
			tm.text = "Building: " + (int)(100*(1-(mTimeLeft/mActualTimeToBuild))) + "%" +
				"\n" + "Time Left: " + (int)mTimeLeft + "s";
		}

		if (mIsSelected)
		{
			// If there is no active player, don't allow this to happen.
			GameObject pm = GameObject.Find("PlayerManager");
			GameObject ap = pm.GetComponent<PlayerManager>().GetActivePlayer();
			if ((ap == null) || (ap.GetComponent<Survivor>().GetState() != Survivor.SurvivorState.REPAIR))
			{
				Clickable[] clickComponents = GetComponentsInChildren<Clickable>();
				foreach (Clickable cc in clickComponents)
				{
					cc.Unclick();
				}
				return;
            }
            else
            {
                bool canRepair = false;
				Survivor playerComp = pm.GetComponent<PlayerManager>().GetActivePlayer().GetComponent<Survivor>();
				foreach (GameObject bgo in playerComp.Buildables)
				{
					if (bgo.name.Equals(mTypeName))
					{
						canRepair = true;
						break;
					}

					if (canRepair)
					{
						break;
					}
				}

				if (canRepair)
				{
					int selection = -1;
					string[] guiOptions;

					// Get 3D position on screen and convert.
					Vector3 v = Camera.main.WorldToScreenPoint(transform.position);
					v = new Vector2(v.x, Screen.height - v.y);

					Vector2 rectSize = new Vector2(200, 30);
					Rect r = new Rect(v.x - rectSize.x/2, v.y, rectSize.x, rectSize.y);

					if (Repairable)
					{
						guiOptions = new string[] { "REPAIR", "DESTROY" };
						selection = GUI.SelectionGrid(r, selection, guiOptions, 2);

						switch (selection)
						{
						case 0: // REPAIR
						{
							mIsReadyToRepair = true;
							Clickable[] clickComponents = GetComponentsInChildren<Clickable>();
							foreach (Clickable cc in clickComponents)
                            {
                                cc.Deselect();
                            }
						}
							break;
						
						case 1: // DESTROY
						{
							Clickable[] clickComponents = GetComponentsInChildren<Clickable>();
							foreach (Clickable cc in clickComponents)
                            {
								cc.Deselect();
							}
							Destroy(gameObject);
						}
							break;

						default:
							break;
						}
					}
					else
					{
						guiOptions = new string[] { "DESTROY" };
						selection = GUI.SelectionGrid(r, selection, guiOptions, 1);
						if (selection == 0)
						{
							Clickable[] clickComponents = GetComponentsInChildren<Clickable>();
							foreach (Clickable cc in clickComponents)
                            {
								cc.Deselect();
							}

							Destroy(gameObject);
						}
					}
				}
				else
				{
					GameObject instructionUI = GameObject.Find("Instructions");
					if (instructionUI != null)
					{
						instructionUI.GetComponent<MenuNotifications>().SetText("Unable to repair this type.");
                    }
                }
            }
        }
    }
    
    public void Repair()
	{
		mIsReadyToRepair = false;

		Health hc = GetComponent<Health>();
		if (hc != null)
		{
			hc.Heal(hc.MaxHealth);
			Debug.Log ("BUILDABLE: Repaired " + this.gameObject.name);
		}
		else
		{
			Debug.Log ("BUILDABLE: Failed to repair building with no Health: " + this.gameObject.name);
		}
	}

	void OnClick()
	{
		mIsSelected = true;
	}
	void OnDeselect()
	{
		mIsSelected = false;

		GameObject selectionUI = GameObject.Find("SelectionText");
		if (selectionUI != null)
		{
			selectionUI.GetComponent<MenuNotifications>().SetText("");
		}
	}
	void OnExitClicked()
	{
		mIsSelected = false;
		
		GameObject selectionUI = GameObject.Find("SelectionText");
		if (selectionUI != null)
		{
			selectionUI.GetComponent<MenuNotifications>().SetText("");
		}
	}

	void SetMaterial(Material mat)
	{
		for (int i=0; i < transform.childCount; ++i)
		{
			Transform child = transform.GetChild(i);
			// Don't change the color of the Text.
			if (child.GetComponent<TextMesh>() == null)
			{
				child.renderer.material = mat;
			}
		}
	}
}
