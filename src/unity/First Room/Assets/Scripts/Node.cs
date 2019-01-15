using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Node {
	public int gridX;
	public int gridY;

	public bool IsWall;
	public Vector3 Position;

	public Node Parent;

	public int gCost;
	public int hCost;

	public int FCost { get { return gCost + hCost;} }

	public Node(bool a_IsWall, Vector3 a_Pos, int a_gridX, int a_gridY)
	{
		IsWall = a_IsWall;
		Position = a_Pos;
		gridX = a_gridX;
		gridY = a_gridY;
	}

	public string Print(){
		var sb = new StringBuilder();
		sb.AppendFormat("Node Space Position: {0}, {1}, {2} Node Grid Position: {3}, {4}", Position.x, Position.y, Position.z, gridX, gridY );
		return sb.ToString();
	}
}
