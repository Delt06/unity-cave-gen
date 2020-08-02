using UnityEngine;
using static UnityEngine.Vector3;

public static class Squares
{
	public static int GetConfiguration(bool leftTop, bool rightTop, bool rightBottom, bool leftBottom) =>
		(leftTop ? 1 << 3 : 0) + (rightTop ? 1 << 2 : 0) +
		(rightBottom ? 1 << 1 : 0) + (leftBottom ? 1 : 0);
	
	public static MeshBuilder AddVerticesAndTriangles(this MeshBuilder builder, int configuration)
	{
		switch (configuration)
		{
			case 0b0001: return builder.AddTriangle(zero, up * 0.5f, right * 0.5f);

			case 0b0010: return builder.AddTriangle(right * 0.5f, new Vector3(1f, 0.5f), right);

			case 0b0011:
				return builder.AddTriangle(zero, up * 0.5f, new Vector3(1f, 0.5f))
					.AddTriangle(zero, new Vector3(1f, 0.5f), right);

			case 0b0100:
				return builder.AddTriangle(new Vector3(1f, 1f), new Vector3(1f, 0.5f),
					new Vector3(0.5f, 1f));

			case 0b0101:
				return builder.AddTriangle(up * 0.5f, right * 0.5f, zero)
					.AddTriangle(new Vector3(0.5f, 1f), new Vector3(1f, 1f), new Vector3(1f, 0.5f));

			case 0b0110:
				return builder.AddTriangle(new Vector3(0.5f, 0f), new Vector3(1f, 1f), right)
					.AddTriangle(new Vector3(0.5f, 0f), new Vector3(0.5f, 1f), new Vector3(1f, 1f));

			case 0b0111:
				return builder.AddTriangle(zero, new Vector3(0f, 0.5f), new Vector3(0.5f, 1f))
					.AddTriangle(zero, new Vector3(0.5f, 1f), new Vector3(1f, 1f))
					.AddTriangle(zero, new Vector3(1f, 1f), right);

			case 0b1000: return builder.AddTriangle(up, new Vector3(0.5f, 1f), up * 0.5f);

			case 0b1001:
				return builder.AddTriangle(zero, new Vector3(0.5f, 1f), new Vector3(0.5f, 0f))
					.AddTriangle(zero, up, new Vector3(0.5f, 1f));

			case 0b1010:
				return builder.AddTriangle(new Vector3(0.5f, 0f), new Vector3(1f, 0.5f), right)
					.AddTriangle(new Vector3(0f, 0.5f), up, new Vector3(0.5f, 1f));

			case 0b1011:
				return builder.AddTriangle(zero, up, right)
					.AddTriangle(up, new Vector3(1f, 0.5f), right)
					.AddTriangle(up, new Vector3(0.5f, 1f), new Vector3(1f, 0.5f));

			case 0b1100:
				return builder.AddTriangle(up, new Vector3(1f, 1f), new Vector3(0f, 0.5f))
					.AddTriangle(new Vector3(0f, 0.5f), new Vector3(1f, 1f), new Vector3(1f, 0.5f));

			case 0b1101:
				return builder.AddTriangle(zero, up, new Vector3(1f, 1f))
					.AddTriangle(zero, new Vector3(1f, 1f), new Vector3(1f, 0.5f))
					.AddTriangle(zero, new Vector3(1f, 0.5f), new Vector3(0.5f, 0f));

			case 0b1110:
				return builder.AddTriangle(new Vector3(0f, 0.5f), right, new Vector3(0.5f, 0f))
					.AddTriangle(new Vector3(0f, 0.5f), up, right)
					.AddTriangle(up, new Vector3(1f, 1f), right);

			case 0b1111:
				return builder.AddTriangle(zero, new Vector3(1f, 1f), right)
					.AddTriangle(zero, up, new Vector3(1f, 1f));

			default: return builder;
		}
	}
		
}