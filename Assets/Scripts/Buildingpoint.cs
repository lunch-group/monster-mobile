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
				GameObject cmgo = GameObject.Find("CommerceManager");
				CommerceManager cm = cmgo.GetComponent<CommerceManager>();

				// List all prefabs of the chosen type.
				string[] buildingTypes = mPrefabManager.GetPrefabNamesByType(mPrefabType);

				string[] priceList = new string[buildingTypes.Length];
				for (int s = 0; s < buildingTypes.Length; ++s)
				{
					priceList[s] = buildingTypes[s] + "\nPrice: " + cm.GetPrice(buildingTypes[s].ToString()) + "g";
				}

				if (buildingTypes != null)
				{
					int numColumns = 2;
					Vector2 rectSize = new Vector2(150 * numColumns, 20 * priceList.Length);
					Rect r = new Rect(v.x - rectSize.x/2, v.y, rectSize.x, rectSize.y);

					int selection = -1;
					selection = GUI.SelectionGrid(r, selection, priceList, numColumns);
					if (selection >= 0)
					{
						mBuildSelection = buildingTypes[selection];


						if (cm.Purchase(mBuildSelection))
						{
							Debug.Log ("BUILDPOINT: Building a " + mBuildSelection);
							mIsReadyToBuild = true;

							// Reset the clicked state, to hide this GUI.
							clickComponent.SetIsClicked(false);
                        }
                        else
                        {
                            // Keep the list up to make another selection.
							mBuildSelection = "";
						}
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
