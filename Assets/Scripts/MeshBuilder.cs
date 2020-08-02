using System.Collections.Generic;
using UnityEngine;

public class MeshBuilder
{
	public Vector3 Origin { get; set; }

	public MeshBuilder WithOriginAt(Vector3 origin)
	{
		Origin = origin;
		return this;
	}

	public MeshBuilder AddTriangle(Vector3 offset0, Vector3 offset1, Vector3 offset2)
	{
		var verticesOnStart = _vertices.Count;

		_vertices.Add(Origin + offset0);
		_vertices.Add(Origin + offset1);
		_vertices.Add(Origin + offset2);

		_triangles.Add(verticesOnStart);
		_triangles.Add(verticesOnStart + 1);
		_triangles.Add(verticesOnStart + 2);

		_uvs.Add(offset0);
		_uvs.Add(offset1);
		_uvs.Add(offset2);

		return this;
	}

	public Mesh ToMesh()
	{
		var mesh = new Mesh();
		mesh.SetVertices(_vertices);
		mesh.SetTriangles(_triangles, 0);
		mesh.SetUVs(0, _uvs);

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		mesh.RecalculateTangents();

		return mesh;
	}

	public MeshBuilder Clear()
	{
		_vertices.Clear();
		_triangles.Clear();
		_uvs.Clear();
		Origin = default;

		return this;
	}

	private readonly List<Vector3> _vertices = new List<Vector3>();
	private readonly List<int> _triangles = new List<int>();
	private readonly List<Vector2> _uvs = new List<Vector2>();
}