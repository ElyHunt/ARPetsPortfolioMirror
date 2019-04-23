using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWalk : MonoBehaviour {

	public float moveForce = 0f;
	public Rigidbody rbody;
	public Vector3 moveDir;
	public LayerMask whatIsHittable;
	public float maxDistFromWall = 0f;
	public float idleTime = 3.0f;
	public float walkTime = 4.0f;
	public bool isEnabled = true;
	public Animator anim;

	// Use this for initialization

	void Start () {
		rbody = GetComponent<Rigidbody>();
		moveDir = ChooseDirection();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isEnabled)
		{
			moveDir = ChooseDirection();
			return;
		}
		transform.rotation = Quaternion.LookRotation(moveDir);
		if(walkTime > 0f)
		{
			walkTime -= Time.deltaTime;
		}
		else
		{
			moveDir.Set(0,0,0);
			if(idleTime > 0f)
			{
				idleTime -= Time.deltaTime;
			}
			else{
				walkTime = 4.0f;
				idleTime = 3.0f;
				//This started the feed method during testing, not used now
				//isEnabled = false;
				moveDir = ChooseDirection();
				return;
			}
		}
		//Debug.Log(moveForce + " and vector: " + moveDir.x + " " + moveDir.y + " " + moveDir.z);
		if(anim != null){
			anim.SetFloat("speed", (moveDir.x + moveDir.z) * moveForce);
			if(moveDir.x + moveDir.z == 0){
				anim.SetBool("luck", false);
			}
		}
		rbody.velocity = moveDir * moveForce;
	}

	Vector3 ChooseDirection()
	{
		Vector3 temp = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
		Vector3 check = new Vector3(0,0,0);
		if(temp == check){
			temp.Set(0,0,1);
		}
		if(moveDir.x >= (temp.x-5) && moveDir.x <= (temp.x+5)){
			return ChooseDirection();
		}
		if(moveDir.z >= (temp.z-5) && moveDir.z <= (temp.z+5)){
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

	/*void WalkTo()
	{
		if(pathFinder != null)
		{
			pathFinder.FindPath(pathFinder.StartPosition.position, pathFinder.TargetPosition.position);
		}

		for(int i = 0; i < grid.FinalPath.Count; i++)
		{
			moveDir.Set(grid.FinalPath[i].Position.x, 0, grid.FinalPath[i].Position.z);
			transform.rotation = Quaternion.LookRotation(moveDir);
			transform.position = Vector3.Lerp(transform.position, moveDir, moveForce * Time.deltaTime);
		}
		
		//StartCoroutine(AsyncWalk());
	}*/
	/*IEnumerator AsyncWalk(){
		if(pathFinder != null)
		{
			pathFinder.FindPath(pathFinder.StartPosition.position, pathFinder.TargetPosition.position);
		}
		int i = 0;
		if( i < grid.FinalPath.Count)
		{
			moveDir.Set(grid.FinalPath[i].Position.x, 0, grid.FinalPath[i].Position.z);
			transform.rotation = Quaternion.LookRotation(moveDir);
			transform.position = Vector3.Lerp(transform.position, moveDir, moveForce);
			yield return new WaitForSeconds(5.0f);
			i++;
		}
	}*/
}