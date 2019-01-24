using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWalk : MonoBehaviour {

	private GameObject manager;
	public float moveForce = 0f;
	private Rigidbody rbody;
	private PathFinder pathFinder;
	private Grid grid;
	public Vector3 moveDir;
	public LayerMask whatIsHittable;
	public float maxDistFromWall = 0f;
	public float idleTime = 3.0f;
	public float walkTime = 4.0f;

	// Use this for initialization

	void Start () {
		Debug.Log("IM IN START");
		manager = GameObject.Find("GameManager");
		rbody = GetComponent<Rigidbody>();
		pathFinder = manager.GetComponent<PathFinder>();
		grid = manager.GetComponent<Grid>();
		moveDir = ChooseDirection();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(walkTime > 0f)
		{
			walkTime -= Time.deltaTime;
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
				WalkTo();
				moveDir = ChooseDirection();
				transform.rotation = Quaternion.LookRotation(moveDir);
			}
		}
		rbody.velocity = moveDir * moveForce;
	}

	Vector3 ChooseDirection()
	{
		Vector3 temp = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
		Vector3 check = new Vector3(0,0,0);
		if(temp == check){
			//Debug.Log("Hit 0,0,0");
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

	void WalkTo()
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
	}
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