﻿using UnityEngine;
using System.Collections;

public class RaycastTest : MonoBehaviour
{	
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				Debug.Log ("MOUSEDOWN: ObjectName: " + hit.collider.name + ", HitPoint: " + hit.point + ", ObjectPos: " + hit.collider.gameObject.transform.position);
			}
		}
	}
}