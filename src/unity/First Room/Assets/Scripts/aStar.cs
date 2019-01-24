using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aStar : MonoBehaviour
{
     private GameObject manager;
    private PathFinder pathFinder;
	private Grid grid;
    private BasicWalk walk;
    private bool isDisabled;
    public Transform destination;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        pathFinder = manager.GetComponent<PathFinder>();
		grid = manager.GetComponent<Grid>();
        walk = GetComponent<BasicWalk>();
        isDisabled = walk.isEnabled;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDisabled){
            return;
        }
        if(grid.FinalPath != null && destination != null)
        {
            Node localNode = grid.NodeFromWorldPosition(transform.position);
            Node destinationNode = grid.NodeFromWorldPosition(destination.position);
            if(localNode.gridX == destinationNode.gridX && localNode.gridY == destinationNode.gridY){
                
            }
        }

    }
}
