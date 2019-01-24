using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aStar : MonoBehaviour
{
    private GameObject manager;
    private PathFinder pathFinder;
	private Grid grid;
    private BasicWalk walk;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
        pathFinder = manager.GetComponent<PathFinder>();
		grid = manager.GetComponent<Grid>();
        walk = GetComponent<BasicWalk>();
    }

    // Update is called once per frame
    void Update()
    {
        if(walk.isEnabled)
    }
}
