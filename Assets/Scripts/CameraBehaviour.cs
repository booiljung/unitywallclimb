/*
 * Camera Following
 * 
 * Author: Booil Ted Joung
 * Email : tedfromskyy@gmail.com
 * 
 * */

using UnityEngine;
using System.Collections;

public class CameraBehaviour
	: MonoBehaviour
{
	public GameObject target;
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.position = this.target.transform.position + (-this.target.transform.forward * 7f) + (this.target.transform.up * 4f);
		this.transform.LookAt(this.target.transform.position + this.target.transform.up*1f);
	}
}
