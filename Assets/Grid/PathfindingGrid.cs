using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathfindingGrid : MonoBehaviour {

	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public Node[,] grid;
	public float nodeRadius;
	public GameObject cubePrefab;
	public bool OnlyDisplayPathGizmos;
	//public Transform player;

	float nodeDiameter;
	int gridSizeX;
	int gridSizeY;
	List<GameObject> gridLines;



	public List<Node> path;

	public int MaxSize
	{
		get {
			return gridSizeX * gridSizeY;
		}
	}

	private void Start() {
		foreach (GameObject line in gridLines)
		{
			if(line != null){

				SceneManager.MoveGameObjectToScene(line,SceneManager.GetSceneByName(GameManager.instance.currentGameId));
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube (transform.position, new Vector3 (gridWorldSize.x, gridWorldSize.y, 1));

		if(OnlyDisplayPathGizmos)
		{
			if(path != null)
			{
				foreach (Node n in path)
				{
					Gizmos.color = Color.black;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f) - Vector3.up * nodeRadius);
				}
			}
		}else{
			if (grid != null) 
			{
	//			Node playeNode = GridFromWorldPoint (player.position);
				foreach (Node n in grid) 
				{
					Gizmos.color = (n.walkable) ? Color.white : Color.red;
					//if (playeNode == n) {
					//	Gizmos.color = Color.magenta;
					//}
 					if (path != null)
					{
						if (path.Contains(n))
						{

							Gizmos.color = Color.black;
	
						}
					} 
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f) - Vector3.forward * nodeRadius);
				}
			}

		}

	}

	// Use this for initialization
	void Awake () 
	{	

		gridLines = new List<GameObject>();
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		CreateGrid ();

	}

	public Node GridFromWorldPoint(Vector3 worldPosition)
	{
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;

		percentX = Mathf.Clamp01 (percentX);
		percentY = Mathf.Clamp01 (percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid [x, y];
	}


	public List<Node> GetNeighbours(Node node)
	{
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0) continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					if((checkX == node.gridX && checkY != node.gridY) || (checkX != node.gridX && checkY == node.gridY))
					{
						neighbours.Add(grid[checkX, checkY]);
					}
					
				}
			}
		}

		return neighbours;
	}

	void CreateGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];

		Vector3 gridBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++) 
		{
			for (int y = 0; y < gridSizeY; y++) 
			{
				GameObject current = null;
				Vector3 worldPoint = gridBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere (worldPoint, nodeRadius, unwalkableMask));
				grid [x, y] = new Node (walkable, worldPoint, x, y);
				if(x == 0){
					Vector3 linePoint = worldPoint - (Vector3.up * -.5f) + (Vector3.right * 5);
					current = Instantiate(cubePrefab, linePoint, Quaternion.identity);
					gridLines.Add(current);
				}
				if(y==0){
					Vector3 linePointY =  worldPoint - (Vector3.up * -5) + (Vector3.right * -.5f);
					current = Instantiate(cubePrefab, linePointY, Quaternion.identity);

					current.transform.Rotate(0,0,90);
					gridLines.Add(current);
				}


				
				
					
			}
		}
	}

}


