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
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        pathFinder = manager.GetComponent<PathFinder>();
		grid = manager.GetComponent<Grid>();
        walk = GetComponent<BasicWalk>();
        bed = GameObject.Find("RFAIPP_Bed_2");
        destination = bed.transform;
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
        if(current.gridX == dest.gridX && current.gridY == dest.gridY)
        {
            walk.isEnabled = true;
            return;
        }
        Node next = null;
        for(int i = 0; i < grid.FinalPath.Count; i++)
        {
            if(current.gridX == grid.FinalPath[i].gridX && current.gridY == grid.FinalPath[i].gridY)
            {
                next = grid.FinalPath[i+1];
            }
        }
        if(next == null)
        {
            next = grid.FinalPath[0];
        }
        walk.moveDir = Vector3.RotateTowards(transform.position,next.Position, walk.moveForce * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(walk.moveDir);
        walk.rbody.velocity = walk.moveDir * walk.moveForce;
    }
}
