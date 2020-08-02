using UnityEngine;

[RequireComponent(typeof(Map)), RequireComponent(typeof(MeshFilter)), ExecuteAlways]
public sealed class MapMesh : MonoBehaviour
{
	[ExecuteAlways]
	private void Update()
	{
		if (!Application.isPlaying) 
			GenerateMesh();
	}

	private void Start()
	{
		GenerateMesh();
	}

	private void GenerateMesh()
	{
		_builder.Clear();

		_map = GetComponent<Map>().GetDataCopy();

		for (var x = 0; x < _map.Width - 1; x++)
		{
			for (var y = 0; y < _map.Height - 1; y++)
			{
				var configuration = Squares.GetConfiguration(_map[x, y + 1], _map[x + 1, y + 1], _map[x + 1, y], _map[x, y]);
				var leftBottom = new Vector3(x, y);
				
				_builder.WithOriginAt(leftBottom)
					.AddVerticesAndTriangles(configuration);
			}
		}

		var meshFilter = GetComponent<MeshFilter>();
		meshFilter.sharedMesh = _builder.ToMesh();
	}

	private readonly MeshBuilder _builder = new MeshBuilder();
	private BitArray2D _map;
}