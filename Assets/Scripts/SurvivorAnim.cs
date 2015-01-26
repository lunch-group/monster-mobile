using UnityEngine;
using System.Collections;

public class SurvivorAnim : MonoBehaviour 
{
    Animator anim;
	int buildHash = Animator.StringToHash("bool_build");

    void Start ()
    {
        anim = GetComponent<Animator>();
    }


    void Update ()
    {
		float move = Input.GetAxis("Vertical");  // Grab outside input and pass to variable
		anim.SetFloat("float_speed", move); //pass variable to the float parameter in the animator
		
		if(Input.GetKey(KeyCode.Space))
		{
			anim.SetBool(buildHash, true);
			
		}
		
	
    }
}