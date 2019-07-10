using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node {

	public bool walkable;
	public Vector3 worldPosition;
	public int totalCost = int.MaxValue;
	public int distance = 1;
	public int gridX;
	public int gridY;
	public bool hasFloor;

	

	int heapIndex;

	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
	{
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
		
	}

	public int HeapIndex
	{
		get{
			return heapIndex;
		}
		set{
			heapIndex = value;
		}
	}	
	
	public int CompareTo(Node nodeToCompare)
	{
		return totalCost - nodeToCompare.totalCost;
	}
	
}



