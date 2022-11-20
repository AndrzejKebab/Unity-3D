using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public class testing : MonoBehaviour
{
	private Grid grid;

	private void Start()
	{
		grid = new Grid(4, 2, 10f, Vector3.zero);

		HeatMapVisual heatMapVisual = new HeatMapVisual(grid, GetComponent<MeshFilter>()); 
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			grid.SetValue(UtilsClass.GetMouseWorldPositionWithZ(), 56);

		}

		if (Input.GetMouseButtonDown(1))
		{
			Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPositionWithZ()));
		}
	}

	private class HeatMapVisual
	{
		private Grid grid;

		public HeatMapVisual(Grid grid, MeshFilter meshFilter)
		{
			this.grid = grid;

			Vector3[] vertices;
			Vector2[] uv;
			int[] triangles;

			MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out vertices, out uv, out triangles);

			for (int x = 0; x < grid.GetWidth(); x++)
			{
				for(int z = 0; z < grid.GetHeight(); z++)
				{
					int index = x * grid.GetHeight() + z;
					Vector3 baseSize = new Vector3(1, 1) * grid.GetCellSize();
					MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, z), 0f, baseSize, Vector2.zero, Vector2.zero);
						;
				}
			}

			Mesh mesh = new Mesh();
			mesh.vertices = vertices;
			mesh.uv = uv;
			mesh.triangles = triangles;

			meshFilter.mesh = mesh;
		}
	}
}
