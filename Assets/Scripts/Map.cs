using UnityEngine;
using Random = System.Random;

public sealed class Map : MonoBehaviour
{
	[SerializeField] private int _seed = 0;
	[SerializeField] private Vector2Int _size = Vector2Int.one * 100;
	[SerializeField, Range(0, 1)] private float _fill = 0;
	[SerializeField, Min(0)] private int _smoothingIterations = 5;
	[SerializeField, Min(0)] private int _minNeighborsForWall = 4;
	[SerializeField] private bool _gizmo = false;

	public BitArray2D GetDataCopy()
	{
		if (_map == null)
			GenerateRandomMap();

		return _map.Clone();
	}

	private void GenerateRandomMap()
	{
		_map = new BitArray2D(_size.x, _size.y);
		_buffer = _map.Clone();

		GenerateMapBase();
		ApplySmoothing();
	}

	private void GenerateMapBase()
	{
		var random = new Random(_seed);

		for (var x = 0; x < _map.Width; x++)
		{
			for (var y = 0; y < _map.Height; y++)
			{
				_map[x, y] = OnEdge(x, y) || random.NextDouble() < _fill;
			}
		}
	}

	private bool OnEdge(int x, int y) => x == 0 || x == _map.Width - 1 ||
	                                     y == 0 || y == _map.Height - 1;

	private void ApplySmoothing()
	{
		for (var i = 0; i < _smoothingIterations; i++)
		{
			Smooth();
		}
	}

	private void Smooth()
	{
		for (var x = 0; x < _map.Width; x++)
		{
			for (var y = 0; y < _map.Height; y++)
			{
				var wallsCount = GetSurroundingWallCount(x, y);

				if (wallsCount > _minNeighborsForWall)
					_buffer[x, y] = true;
				else if (wallsCount < _minNeighborsForWall)
					_buffer[x, y] = false;
				else
					_buffer[x, y] = _map[x, y];
			}
		}

		(_map, _buffer) = (_buffer, _map);
	}

	private int GetSurroundingWallCount(int x, int y)
	{
		var wallsCount = 0;

		for (var neighborX = x - 1; neighborX <= x + 1; neighborX++)
		{
			for (var neighborY = y - 1; neighborY <= y + 1; neighborY++)
			{
				if (neighborX == x && neighborY == y) continue;

				wallsCount += GetNeighborValue(neighborX, neighborY);
			}
		}

		return wallsCount;
	}

	private int GetNeighborValue(int x, int y) => OutOfRange(x, y) ? 1 : _map[x, y] ? 1 : 0;

	private bool OutOfRange(int x, int y) =>
		x < 0 || x >= _map.Width ||
		y < 0 || y >= _map.Height;

	private BitArray2D _map;
	private BitArray2D _buffer;

	private void OnDrawGizmos()
	{
		if (!_gizmo) return;
		if (_map == null) return;

		for (var x = 0; x < _map.Width; x++)
		{
			for (var y = 0; y < _map.Height; y++)
			{
				Gizmos.color = _map[x, y] ? Color.white : Color.black;
				var position = new Vector3(x, y);
				Gizmos.DrawCube(position, Vector3.one);
			}
		}
	}

	private void OnValidate()
	{
		if (_size.x < 0) _size.x = 0;
		if (_size.y < 0) _size.y = 0;

		GenerateRandomMap();
	}
}