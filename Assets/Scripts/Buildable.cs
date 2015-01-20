using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour
{
	public float TimeToBuild = 0.0f;

	private float mTimeLeft;
	private bool mIsBuilding;
	public bool IsBuilding() { return mIsBuilding; }
	private bool mIsDone;
	public bool IsDone() { return mIsDone; }
	private bool mShowGui;
	public void ShowGui(bool s) { mShowGui = s; }

	public Material BuildingMaterial = null;
	public Material BuildingMaterialDone = null;


	void Start ()
	{
		Reset ();
	}

	void Reset ()
	{
		mIsBuilding = true;
		mIsDone = false;
		mTimeLeft = TimeToBuild;

		TextMesh tm = GetComponentInChildren<TextMesh>();
		tm.text = "";
	}
	
	void Update ()
	{
		if (mIsBuilding)
		{
			mTimeLeft -= Time.deltaTime;
			if (mTimeLeft <= 0.0f)
			{
				mTimeLeft = 0.0f;
				mIsDone = true;
				mIsBuilding = false;

				//BuildText.renderer.enabled = false;
				Debug.Log ("Finished building " + this.name);
				TextMesh tm = GetComponentInChildren<TextMesh>();
				tm.text = "Building: Done";

				SetMaterial(BuildingMaterialDone);
			}
			else
			{
				SetMaterial(BuildingMaterial);
			}
		}
		else
		{
			TextMesh tm = GetComponentInChildren<TextMesh>();
			tm.renderer.enabled = false;
		}
	}

	void OnGUI ()
	{
		if (mShowGui)
		{
			TextMesh tm = GetComponentInChildren<TextMesh>();
			tm.text = "Building: " + (int)(100*(1-(mTimeLeft/TimeToBuild))) + "%" +
				"\n" + "Time Left: " + (int)mTimeLeft + "s";
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
