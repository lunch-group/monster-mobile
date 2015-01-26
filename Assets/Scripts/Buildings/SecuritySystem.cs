using UnityEngine;
using System.Collections;

public class SecuritySystem : MonoBehaviour
{
	void Start ()
	{
		GetComponent<Buildable>().SetTypeName("SecuritySystem");
	}
	
	void Update ()
	{
	}
}
