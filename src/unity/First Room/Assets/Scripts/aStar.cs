using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aStar : MonoBehaviour
{
    private GameObject manager;
    private GameObject bed;
    private PathFinder pathFinder;
	private Grid grid;
    private BasicWalk walk;
    public Transform destination;
    private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        pathFinder = manager.GetComponent<PathFinder>();
		grid = manager.GetComponent<Grid>();
        walk = GetComponent<BasicWalk>();
        anim = GetComponent<Animation>();
        anim.wrapMode = WrapMode.Loop;
    }

    // Update is called once per frame
    void Update()
    {
        if(walk.isEnabled)
        {
            return;
        }
        Node current = grid.NodeFromWorldPosition(transform.position);
        Node dest = grid.NodeFromWorldPosition(destination.position);
        pathFinder.FindPath(transform.position, dest.Position);
        Debug.Log("Cat Loc: " + current.gridX + " " + current.gridY);
        Debug.Log("Dest Loc: " + dest.gridX + " " + dest.gridY);
        if(current.gridX == dest.gridX && current.gridY == dest.gridY)
        //if(((current.gridX >= dest.gridX-2) || (current.gridX <= dest.gridX+2)) && current.gridY == dest.gridY)
        {
            walk.isEnabled = true;
            return;
        }
        Node next = grid.FinalPath[0];
        if(current == grid.FinalPath[0]){
            next = grid.FinalPath[1];
        }
        float step = (Time.deltaTime * walk.moveForce) * 3;
        transform.LookAt(next.Position);
        if(anim != null){
			anim.Play("WalkDuplicate");
		}
        transform.position = Vector3.MoveTowards(transform.position, next.Position, step);
    }
}
