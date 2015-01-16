using UnityEngine;
using System.Collections;

public class Buildingpoint : MonoBehaviour
{
	public GUISkin skin = null;
	
	bool gui = false;
	
	// Tower prefab
	public SniperTower towerPrefab = null;
	
	void OnGUI()
	{    
		if (gui)
		{
			GUI.skin = skin;
			
			// get 3d position on screen        
			Vector3 v = Camera.main.WorldToScreenPoint(transform.position);
			
			// convert to gui coordinates
			v = new Vector2(v.x, Screen.height - v.y); 
			
			// creation menu for tower
			int width = 200;
			int height = 40;
			Rect r = new Rect(v.x - width / 2, v.y - height / 2, width, height);
			GUI.contentColor = (Player.gold >= towerPrefab.buildPrice ? Color.green : Color.red);
			GUI.Box(r, "Build " + towerPrefab.name + "(" + towerPrefab.buildPrice + " gold)");
			
			// mouse not down anymore and mouse over the box? then build the tower                
			if (Event.current.type == EventType.MouseUp && 
			    r.Contains(Event.current.mousePosition) &&
			    Player.gold >= towerPrefab.buildPrice)
			{
				// decrease gold
				Player.gold -= towerPrefab.buildPrice;
				
				// instantiate
				Instantiate(towerPrefab, transform.position, Quaternion.identity);
				
				// disable gameobject
				gameObject.SetActive(false);
			}
		}
	}
	
	public void OnMouseDown()
	{
		gui = true;
	}
	
	
	public void OnMouseUp()
	{
		gui = false;
	}
}