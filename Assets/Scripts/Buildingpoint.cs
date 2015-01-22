using UnityEngine;
using System.Collections;

public class Buildingpoint : MonoBehaviour
{
	private PrefabManager mPrefabManager = null;
	private GameObject mBuilding = null;
	private bool mIsReadyToBuild = false;
	public bool IsReadyToBuild() { return mIsReadyToBuild; }
	
	private Transform mBuilder = null;

	private int mPrefabType = (int)PrefabManager.PrefabType.INVALID;

	private string mBuildSelection = "";


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
		Clickable clickComponent = GetComponentInParent<Clickable>();
		if (clickComponent && clickComponent.IsClicked())
		{
			GameObject pm = GameObject.Find("PlayerManager");
			if (pm.GetComponent<PlayerManager>().GetActivePlayer() == null)
			{
				clickComponent.SetIsClicked(false);
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
			else if (mIsReadyToBuild == false)
			{
				// List all prefabs of the chosen type.
				string[] buildingTypes = mPrefabManager.GetPrefabNamesByType(mPrefabType);

				if (buildingTypes != null)
				{
					Vector2 rectSize = new Vector2(300, 30);
					Rect r = new Rect(v.x - rectSize.x/2, v.y, rectSize.x, rectSize.y);

					int selection = -1;
					selection = GUI.SelectionGrid(r, selection, buildingTypes, 2);
					if (selection >= 0)
					{
						mIsReadyToBuild = true;
						mBuildSelection = buildingTypes[selection];

						// Reset the clicked state, to hide this GUI.
						clickComponent.SetIsClicked(false);
					}
				}
				else
				{
					mPrefabType = (int)PrefabManager.PrefabType.INVALID;

					// Reset the clicked state, to hide this GUI.
					clickComponent.SetIsClicked(false);
				}
			}
		}
	}

	void OnUnclicked()
	{
		if (!mIsReadyToBuild && mPrefabType != (int)PrefabManager.PrefabType.INVALID)
		{
			mPrefabType = (int)PrefabManager.PrefabType.INVALID;
		}
	}

	private void Buy()
	{
		Clickable clickComponent = GetComponentInParent<Clickable>();
		if (clickComponent && clickComponent.IsClicked())
        {
			// Get 3d position on screen.
			Vector3 v = Camera.main.WorldToScreenPoint(transform.position);
			// Convert to gui coordinates.
			v = new Vector2(v.x, Screen.height - v.y); 

	        Buyable buyComponent = mBuilding.GetComponent<Buyable>();
			
			// Create menu for building.
			Vector2 rwh = new Vector2(200, 30);
			Rect r = new Rect(v.x - rwh.x / 2, v.y - rwh.y / 2, rwh.x, rwh.y);
			Vector2 rwh2 = new Vector2(100, 30);
			Rect r2 = new Rect(v.x - rwh2.x / 2, v.y + rwh.y / 2, rwh2.x, rwh2.y);
			
			if (buyComponent != null)
			{
				GUI.contentColor = buyComponent.IsAffordable() ? Color.green : Color.red;
				if (GUI.Button(r, "Build " + mBuilding.name + "(" + buyComponent.Cost + " gold)"))
				{
					mIsReadyToBuild = buyComponent.Purchase();
				}
				else
				{
					GUI.contentColor = Color.red;
					if (GUI.Button (r2, "Cancel"))
					{
						clickComponent.SetIsClicked(false);
					}
				}
			}
			else
			{
				GUI.contentColor = Color.green;
				if (GUI.Button(r, "Build " + mBuilding.name + "(" + buyComponent.Cost + " gold)"))
				{
					// No buyable component means its free.
	                mIsReadyToBuild = true;
	            }
	            else
	            {
	                GUI.contentColor = Color.red;
	                if (GUI.Button (r2, "Cancel"))
	                {
	                    clickComponent.SetIsClicked(false);
	                }
	            }
	        }
		}
    }
    
    public void Build(Transform builder, GameObject objToBuild)
	{
		mBuilder = builder;
		mIsReadyToBuild = false;

		// Instantiate the new building.
		mBuilding = mPrefabManager.Spawn(mBuildSelection, transform.position, Quaternion.identity);
		mBuildSelection = "";

		Buildable buildComponent = mBuilding.GetComponent<Buildable>();
		buildComponent.StartBuilding(mBuilder);

		// Hide the building point and disable its Clickable component.
		this.renderer.enabled = false;
		Clickable cc = GetComponent<Clickable>();
		cc.enabled = false;
	}
}
