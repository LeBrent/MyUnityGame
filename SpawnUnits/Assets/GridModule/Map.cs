using UnityEngine;

public class Map : MonoBehaviour
{
	public int SizeX;
	public int SizeY;

	public float TileOffset;

	public Cell Prototype;
	
	public void Start()
	{
		Spawn();
	}

	public void Spawn()
	{
		float xOffset = 0;
		for (int x = 0; x < SizeX; x++)
		{
			float yOffset = 0;
			for (int y = 0; y < SizeY; y++)
			{
				Cell newTile = Instantiate(Prototype);
				newTile.transform.SetParent(transform);
				newTile.transform.localPosition = new Vector3(xOffset, 0, yOffset); // Note that our XY-plane maps to Unity's XZ-plane since Y is the vertical axis.
				newTile.name = string.Format("Cell {0}x{1}", x, y);
				//newTile.name = "Cell " + x + "x" + y;
					
				newTile.X = x;
				newTile.Y = y;
					
				yOffset += TileOffset;
			}
			xOffset += TileOffset;
		}
	}

	[ContextMenu("Test Cell 2,5")]
	public void Test()
	{
		Cell test = GetCell(2, 5);
		if (test != null)
		{
			Debug.Log(test.name, test);
		}
		else
		{
			Debug.Log("Cell 2,5 does not exist!");
		}
	}

	public Cell GetCell(int x, int y)
	{
		if (x >= SizeX || 
		    y >= SizeY ||
		    x < 0 || 
		    y < 0)
		{
			Debug.LogError("Out of bounds!");
			return null;
		}
		
		return transform.GetChild((x * SizeY)+ y).GetComponent<Cell>();	
	}
}
