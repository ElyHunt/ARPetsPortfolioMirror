using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWalk : MonoBehaviour {

	public float moveForce = 0f;
	private Rigidbody rbody;
	public Vector3 moveDir;
	public LayerMask whatIsHittable;
	public float maxDistFromWall = 0f;
	public float idleTime = 0f;

	// Use this for initialization
	void Start () {

		rbody = GetComponent<Rigidbody>();
		moveDir = ChooseDirection();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		rbody.velocity = moveDir * moveForce;

		/*if(Physics.Raycast(transform.position, transform.forward, maxDistFromWall, whatIsHittable))
		{
			StartCoroutine(Wait());
			moveDir = ChooseDirection();
			transform.rotation = Quaternion.LookRotation(moveDir);
		} */
	}

	Vector3 ChooseDirection()
	{
		System.Random ran = new System.Random();
		Vector3 temp = new Vector3(ran.Next(-5,5), 0, ran.Next(-5,5));
		Vector3 check = new Vector3(0,0,0);
		if(temp == check){
			Debug.Log("Hit 0,0,0");
			temp.Set(0,0,1);
		}
		if(moveDir.x == temp.x){
			temp.Set(-temp.x, 0, temp.z);
		}
		if(moveDir.z == temp.z){
			temp.Set(temp.z, 0, -temp.z);
		}
		return temp;
	}

	void OnCollisionEnter(Collision col)
	{

		moveDir = ChooseDirection();
		transform.rotation = Quaternion.LookRotation(moveDir);

	}

	IEnumerator Wait(){
		yield return new WaitForSeconds(5);
		Debug.Log("Done");
	}
}
