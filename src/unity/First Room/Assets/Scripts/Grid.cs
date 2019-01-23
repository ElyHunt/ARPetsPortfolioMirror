using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.Newtonsoft.Json;

public class Grid : MonoBehaviour {

	public Transform StartPosition;
	public LayerMask WallMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public float Distance;

	Node[,] grid;
	public List<Node> FinalPath;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	private void Awake(){
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		CreateGrid();

	}
	void CreateGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
		for(int y=0; y < gridSizeY; y++){
			for(int x = 0; x < gridSizeX; x++)
			{
				Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool Wall = true;

				if(Physics.CheckSphere(worldPoint, nodeRadius, WallMask))
				{
					Wall = false;
				}

				grid[x, y] = new Node(Wall, worldPoint, x, y);
			}

		}
	}

	public Node NodeFromWorldPosition(Vector3 a_WorldPosition){
		//There is a bug in this implementation, will break algorithm if radius is not .2
		//Hence the + 3 and + 2 at the end for a dirty fix
		float xpoint = ((a_WorldPosition.x + gridWorldSize.x /2) / gridWorldSize.x);
		float ypoint = ((a_WorldPosition.z + gridWorldSize.y /2) / gridWorldSize.y);

		xpoint = Mathf.Clamp01(xpoint);
		ypoint = Mathf.Clamp01(ypoint);

		int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
		int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

		return grid[x + 3, y + 2];
	}

	public List<Node> GetNeighboringNodes(Node a_Node)
	{
		
		//New Version, could be risky...
		List<Node> NeighborList = new List<Node>();
		for(int x = -1; x <= 1; x++)
		{
			for(int y = -1; y <=1; y++)
			{
				if(x == 0 && y == 0)
				{
					continue;
				}

				int checkX = a_Node.gridX + x;
				int checkY = a_Node.gridY + y;

				if(checkX >=0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					NeighborList.Add(grid[checkX, checkY]);
				}
			}
		}
		//Debug.Log("Neighbor List" + JsonConvert.SerializeObject(NeighborList).ToString());

		return NeighborList;
	}

	public int Count()
	{
		Debug.Log("In Count");
		int temp = 0;
		foreach(Node node in grid)
		{
			Debug.Log("In Loop");
			if(FinalPath != null)
			{
				if(FinalPath.Contains(node))
					temp++;
			}
		}
		return temp;
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

		if(grid != null)
		{
			foreach(Node node in grid)
			{
				if(node.IsWall){
					Gizmos.color = Color.white;
				}
				else
				{
					Gizmos.color = Color.yellow;
				}

				if(FinalPath != null)
				{
					if(FinalPath.Contains(node))
					{
						Gizmos.color = Color.red;
					}
				}

				Gizmos.DrawCube(node.Position, Vector3.one *(nodeDiameter));
			}
		}
	}
}
