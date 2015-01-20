using UnityEngine;
using System.Collections;

public class Buildingpoint : MonoBehaviour
{
	public GUISkin Skin = null;
	public GameObject Building = null;


	void Update()
	{
	}

	void OnGUI()
	{
		Clickable clickComponent = GetComponentInParent<Clickable>();
		if (clickComponent && clickComponent.IsClicked())
		{
			GUI.skin = Skin;
			
			// Get 3d position on screen.
			Vector3 v = Camera.main.WorldToScreenPoint(transform.position);
			// Convert to gui coordinates.
			v = new Vector2(v.x, Screen.height - v.y); 

			Buyable buyComponent = Building.GetComponent<Buyable>();
			bool canBuild = false;

			// Create menu for building.
			Vector2 rwh = new Vector2(200, 30);
			Rect r = new Rect(v.x - rwh.x / 2, v.y - rwh.y / 2, rwh.x, rwh.y);
			Vector2 rwh2 = new Vector2(100, 30);
			Rect r2 = new Rect(v.x - rwh2.x / 2, v.y + rwh.y / 2, rwh2.x, rwh2.y);

			if (buyComponent != null)
			{
				GUI.contentColor = buyComponent.IsAffordable() ? Color.green : Color.red;
				if (GUI.Button(r, "Build " + Building.name + "(" + buyComponent.Cost + " gold)"))
				{
					canBuild = buyComponent.Purchase();
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
				if (GUI.Button(r, "Build " + Building.name + "(" + buyComponent.Cost + " gold)"))
				{
					// No buyable component means its free.
					canBuild = true;
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

			if (canBuild)
			{
				// Instantiate the new building.
				Instantiate(Building, transform.position, Quaternion.identity);
				
				// Disable the building point.
				gameObject.SetActive(false);

				clickComponent.SetIsClicked(false);
			}
		}
	}
}