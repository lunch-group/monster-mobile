﻿using UnityEngine;
using System.Collections;


/*****
 * All prefabs should be categorized in sub-folders and then this file should be edited accordingly.
 * To spawn a Prefab by name, call: Spawn(prefabName)
*****/
public class PrefabManager : MonoBehaviour
{
	public enum PrefabType
	{
		INVALID = -1,
		BUILDING,
		OTHER,
		MAX
	};

	public string BuildingPath = "Buildings";
	private ArrayList mPrefabNames = new ArrayList();


	void Start()
	{
		Object[] buildingObjs = Resources.LoadAll(BuildingPath);
		string[] buildingNames = new string[buildingObjs.Length];
		for (int i = 0; i < buildingObjs.Length; ++i)
		{
			buildingNames[i] = buildingObjs[i].name;
		}
		mPrefabNames.Add(buildingNames);
	}

	public string[] GetTypeNames()
	{
		string[] names = new string[(int)PrefabType.MAX];

		for (int i = 0; i < (int)PrefabType.MAX; ++i)
		{
			names[i] = GetTypeName (i);
		}

		return names;
	}

	public string GetTypeName(int index)
	{
		string name = "";

		switch ((PrefabType)index)
		{
			case PrefabType.BUILDING:
				name = "Building";
				break;

			case PrefabType.OTHER:
				name = "Other";
				break;

			default:
				break;
		}

		return name;
	}

	public string[] GetPrefabNamesByType(int type)
	{
		if (mPrefabNames.Count > type)
		{
			return mPrefabNames[type] as string[];
		}
		else
		{
			return null;
		}
	}
	
	public GameObject Spawn(string prefabName, Vector3 pos, Quaternion rot)
	{
		GameObject spawned = null;

		for (int i = 0; (i < mPrefabNames.Count) && (spawned == null); ++i)
		{
			for (int j = 0; j < (mPrefabNames[i] as string[]).Length; ++j)
			{
				if ((mPrefabNames[i] as string[])[j].Equals(prefabName))
				{
					switch ((PrefabType)i)
					{
						case PrefabType.BUILDING:
							Debug.Log ("PREFABMANAGER: Spawn - " + prefabName);
						spawned = Instantiate(Resources.Load(BuildingPath + "/" + prefabName), pos, rot) as GameObject;
						break;
					
						default:
							break;
					}
				}
			}
		}

		return spawned;
	}
}
