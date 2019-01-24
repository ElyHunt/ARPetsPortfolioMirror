using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWalk : MonoBehaviour {

	public float moveForce = 0f;
	private Rigidbody rbody;
	public Vector3 moveDir;
	public LayerMask whatIsHittable;
	public float maxDistFromWall = 0f;
	public float idleTime = 3.0f;
	public float walkTime = 4.0f;

	// Use this for initialization
	void Start () {

		rbody = GetComponent<Rigidbody>();
		moveDir = ChooseDirection();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 velocity;
		if(walkTime > 0f)
		{
			//Debug.Log("Good");
			//Debug.Log("Time1: " + walkTime.ToString());
			walkTime -= Time.deltaTime;
			//Debug.Log("Time2: " + walkTime.ToString());
			//rbody.velocity = moveDir * moveForce;
		}
		else
		{
			//Debug.Log("Bad");
			//walkTime = 5.0f;
			moveDir.Set(0,0,0);
			if(idleTime > 0f)
			{
				idleTime -= Time.deltaTime;
				//rbody.velocity = moveDir * moveForce;
			}
			else{
				walkTime = 4.0f;
				idleTime = 3.0f;
				moveDir = ChooseDirection();
				transform.rotation = Quaternion.LookRotation(moveDir);
			}
		}
		velocity = moveDir * moveForce;
		Debug.Log("Velocity x & z: " + velocity.x.ToString() + " " + velocity.z.ToString());
		rbody.velocity = moveDir * moveForce;
	}

	Vector3 ChooseDirection()
	{
		Vector3 temp = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
		Vector3 check = new Vector3(0,0,0);
		if(temp == check){
			Debug.Log("Hit 0,0,0");
			temp.Set(0,0,1);
		}
		if(moveDir.x >= (temp.x-5) && moveDir.x <= (temp.x+5)){
			return ChooseDirection();
			//temp.Set(-temp.x, 0, temp.z);
		}
		if(moveDir.z >= (temp.z-5) && moveDir.z <= (temp.z+5)){
			//temp.Set(temp.z, 0, -temp.z);
			return ChooseDirection();
		}
		return temp;
	}

	void OnCollisionEnter(Collision col)
	{
		walkTime = 4.0f;
		moveDir = ChooseDirection();
		transform.rotation = Quaternion.LookRotation(moveDir);
	}
}
