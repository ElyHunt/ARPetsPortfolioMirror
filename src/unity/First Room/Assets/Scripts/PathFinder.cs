using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.Newtonsoft.Json;

public class PathFinder : MonoBehaviour 
{

	public Grid grid;
	public Transform StartPosition;
	public Transform TargetPosition;

	private void Awake()
	{
		grid = GetComponent<Grid>();
	}

	private void Update()
	{
		//FindPath(StartPosition.position, TargetPosition.position);
	}
	public void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
	{
		//Debug.Log("Start position" + a_StartPos.x + " " + a_StartPos.y + " " + a_StartPos.z);
		//Debug.Log("Target position" + a_TargetPos.x + " " + a_TargetPos.y + " " + a_TargetPos.z);
		Node StartNode = grid.NodeFromWorldPosition(a_StartPos);
		Node TargetNode = grid.NodeFromWorldPosition(a_TargetPos);

		List<Node> OpenList = new List<Node>();
		HashSet<Node> ClosedList = new HashSet<Node>();
		var runCount = 0;
		var MaxRunCount = 1000;

		OpenList.Add(StartNode);
		try{
			while(OpenList.Count > 0)
			{
				if(runCount >= MaxRunCount){
					throw new Exception("Too many runs!");
				}
				Node CurrentNode = OpenList[0];
				

				for(int i = 1; i < OpenList.Count; i++)
				{
					if(OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == 
						CurrentNode.FCost && OpenList[i].hCost < CurrentNode.hCost)
					{
						CurrentNode = OpenList[i];
					}
				}
				OpenList.Remove(CurrentNode);
				ClosedList.Add(CurrentNode);
				
				

				if(CurrentNode == TargetNode)
				{
					GetFinalPath(StartNode, TargetNode);
					break;
				}

				foreach(Node NeighborNode in grid.GetNeighboringNodes(CurrentNode))
				{
					if(!NeighborNode.IsWall || ClosedList.Contains(NeighborNode))
					{
						continue;
					}
					int MoveCost = CurrentNode.gCost + GetManhattanDistance(CurrentNode, NeighborNode);

					if(!OpenList.Contains(NeighborNode) || MoveCost < NeighborNode.FCost)
					//if(MoveCost < NeighborNode.gCost || !OpenList.Contains(NeighborNode))
					{
						NeighborNode.gCost = MoveCost;
						NeighborNode.hCost = GetManhattanDistance(NeighborNode, TargetNode);
						NeighborNode.Parent = CurrentNode;

						if(!OpenList.Contains(NeighborNode))
						{
							OpenList.Add(NeighborNode);
						}
					}
				}
				//runCount++;
			}
		} catch (Exception ex){
			Debug.LogError(ex.Message + ex.StackTrace);
		}
	}

	void GetFinalPath(Node a_StartNode, Node a_EndNode)
	{
		//Debug.Log("Start Node:\n" + a_StartNode.Print());
		//Debug.Log("End Node:\n" + a_EndNode.Print());
		var FinalPathCoords = new List<Vector3>();
		List<Node> FinalPath = new List<Node>();
		Node CurrentNode = a_EndNode;

		while(CurrentNode != a_StartNode )
		{
			FinalPath.Add(CurrentNode);
			//FinalPathCoords.Add(CurrentNode.Position);
			CurrentNode = CurrentNode.Parent;

		}
		FinalPath.Add(CurrentNode);

		FinalPath.Reverse();
		//Debug.Log("Final Path: " + JsonConvert.SerializeObject(FinalPathCoords).ToString());
		// Console.Write("Final Path" + FinalPath.ToString());
		grid.FinalPath = FinalPath;
	}

	int GetManhattanDistance(Node a_nodeA, Node a_nodeB)
	{
		int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
		int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);

		return ix + iy;
	}
}
