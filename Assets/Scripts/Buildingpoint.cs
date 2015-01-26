using UnityEngine;
using System.Collections;

public class Buildingpoint : MonoBehaviour
{
	private PrefabManager mPrefabManager = null;
	private GameObject mBuilding = null;
	private bool mIsReadyToBuild = false;
	public bool IsReadyToBuild() { return mIsReadyToBuild; }
    private bool mIsPendingBuild = false;
    public void SetPendingBuild()
    {
        mIsReadyToBuild = false;
        mIsPendingBuild = true;
    }
	
	private Transform mBuilder = null;

	private int mPrefabType = (int)PrefabManager.PrefabType.INVALID;

	private string mBuildSelection = "";

	private bool mIsClicked = false;


	void Start ()
	{
		GameObject[] managers = GameObject.FindGameObjectsWithTag("Manager");
		foreach (GameObject go in managers)
		{
			if (go.GetComponent<PrefabManager>())
			{
				mPrefabManager = go.GetComponent<PrefabManager>();
			}
		}
	}
	
	void Update ()
	{
	}

	void OnGUI()
	{
		if (mIsClicked)
		{
			Clickable clickComponent = GetComponentInParent<Clickable>();

			// If there is no active player in the Build state, don't allow this to be built.
			GameObject pm = GameObject.Find("PlayerManager");
			GameObject ap = pm.GetComponent<PlayerManager>().GetActivePlayer();
			if (ap == null)
			{
				clickComponent.Unclick();
				return;
			}
			else if (ap.GetComponent<Survivor>().GetState() != Survivor.SurvivorState.BUILD)
			{
				clickComponent.Unclick();
                return;
			}

			// Get 3d position on screen.
			Vector3 v = Camera.main.WorldToScreenPoint(transform.position);
			// Convert to gui coordinates.
			v = new Vector2(v.x, Screen.height - v.y); 

			GUI.contentColor = Color.yellow;

			if (mPrefabType == (int)PrefabManager.PrefabType.INVALID)
			{
				// List all types of prefabs.
				string[] prefabTypeNames = mPrefabManager.GetTypeNames();
				Vector2 rectSize = new Vector2(200, 30);
				Rect r = new Rect(v.x - rectSize.x/2, v.y, rectSize.x, rectSize.y);

				int selection = -1;
				selection = GUI.SelectionGrid(r, selection, prefabTypeNames, 2);
				if (selection >= 0)
				{
					mPrefabType = selection;
                }
			}
			else if (mIsReadyToBuild == false && mIsPendingBuild == false)
			{
				GameObject cmgo = GameObject.Find("CommerceManager");
				CommerceManager cm = cmgo.GetComponent<CommerceManager>();

				// List all prefabs of the chosen type.
				string[] buildingTypes = mPrefabManager.GetPrefabNamesByType(mPrefabType);

				if ((buildingTypes != null) && (buildingTypes.Length > 0))
				{
					string[] priceList = new string[buildingTypes.Length];
					for (int s = 0; s < buildingTypes.Length; ++s)
					{
	                    // Check if this player type is allowed to build this thing.
	                    bool playerCanBuildThis = false;
	                    Survivor playerComp = pm.GetComponent<PlayerManager>().GetActivePlayer().GetComponent<Survivor>();
	                    foreach (GameObject bgo in playerComp.Buildables)
	                    {
	                        if (bgo.name.Equals(buildingTypes[s]))
	                        {
	                            playerCanBuildThis = true;
	                            break;
	                        }
	                    }
	                    
	                    if (playerCanBuildThis)
	                    {
	                        priceList[s] = buildingTypes[s] + "\nPrice: " + cm.GetPrice(buildingTypes[s].ToString()) + "g";
	                    }
	                    else
	                    {
	                        priceList[s] = buildingTypes[s] + "\nUNAVAILABLE";
	                    }
					}
                
					int numColumns = 2;
					Vector2 rectSize = new Vector2(150 * numColumns, 20 * priceList.Length);
					Rect r = new Rect(v.x - rectSize.x/2, v.y, rectSize.x, rectSize.y);

					int selection = -1;
					selection = GUI.SelectionGrid(r, selection, priceList, numColumns);
					if (selection >= 0)
					{
                        if (priceList[selection].Contains("Price:"))
						{
                            mBuildSelection = buildingTypes[selection];
    						if (cm.Purchase(mBuildSelection))
    						{
    							Debug.Log ("BUILDPOINT: Building a " + mBuildSelection);
    							mIsReadyToBuild = true;

    							// Reset the clicked state, to hide this GUI.
								mIsClicked = false;
    							clickComponent.IsClicked = false;
                            }
                            else
                            {
                                GameObject instructionUI = GameObject.Find("Instructions");
                                if (instructionUI != null)
                                {
                                    instructionUI.GetComponent<MenuNotifications>().SetText("Could not afford building.");
                                }
                                
                                // Keep the list up to make another selection.
    							mBuildSelection = "";
    						}
                        }
						else
						{
							GameObject instructionUI = GameObject.Find("Instructions");
							if (instructionUI != null)
							{
								instructionUI.GetComponent<MenuNotifications>().SetText("Unable to build this.");
							}
						}
					}
				}					
				else
				{
                	clickComponent.Unclick();
					GameObject instructionUI = GameObject.Find("Instructions");
					if (instructionUI != null)
					{
						instructionUI.GetComponent<MenuNotifications>().SetText("BUILDINGPOINT:\nNo items found for type: " + (PrefabManager.PrefabType)mPrefabType);
					}
				}
            }
        }
    }
    
    void OnClick()
    {
		mIsClicked = true;
	}
	void OnUnclicked()
	{
		mIsClicked = false;
		if (!mIsReadyToBuild && mPrefabType != (int)PrefabManager.PrefabType.INVALID)
		{
			mPrefabType = (int)PrefabManager.PrefabType.INVALID;
		}
	}
	void OnExitClicked()
	{
		mIsClicked = false;
		if (!mIsReadyToBuild && mPrefabType != (int)PrefabManager.PrefabType.INVALID)
		{
			mPrefabType = (int)PrefabManager.PrefabType.INVALID;
		}
	}

    public void Build(Transform builder, GameObject objToBuild)
	{
		mBuilder = builder;
		mIsPendingBuild = false;

		// Instantiate the new building.
		mBuilding = mPrefabManager.Spawn(mBuildSelection, transform.position, Quaternion.identity);

		Buildable buildComponent = mBuilding.GetComponent<Buildable>();
		buildComponent.StartBuilding(mBuilder, mBuildSelection);

		mBuildSelection = "";

		// Hide the building point and disable its Clickable component.
		this.renderer.enabled = false;
		Clickable cc = GetComponent<Clickable>();
		cc.enabled = false;
	}
}
