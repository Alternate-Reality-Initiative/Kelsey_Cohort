using System;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq;

public class MousePainter : MonoBehaviour
{
	private int currentBrush = 0;

	[SerializeField]
	private int eraserBrushIndex;

	[SerializeField]
	private List<Brush> brushes = new List<Brush>();

	[SerializeField]
	private bool erase = false;

	InkCanvas[] canvases;
	private bool isPainting = false;

	private void Start()
	{
		canvases = FindObjectsOfType<InkCanvas>();
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			bool success = true;
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo))
			{
				var paintObject = hitInfo.transform.GetComponent<InkCanvas>();

				if (paintObject != null)
				{
					if (!isPainting)
					{
						// Just started painting a stroke
						isPainting = true;
						// Debug.Log("Started drawing");
					}

					success = erase ?
						paintObject.Erase(brushes[currentBrush], hitInfo) :
						paintObject.Paint(brushes[currentBrush], hitInfo);
				}
				if (!success)
				{
					Debug.LogError("Failed to paint.");
				}
			}
		}
		else if (isPainting)
		{
			// Stopped painting
			isPainting = false;
			// Debug.Log("Stopped drawing");

			foreach (var inkCanvas in canvases)
			{
				inkCanvas.SaveSnapshot();
			}
		}

		if (Input.GetKeyDown(KeyCode.Z))
		{
			Undo();
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			Redo();
		}
	}

	public void OnGUI()
	{
		if (GUILayout.Button("Reset"))
		{
			foreach (var canvas in FindObjectsOfType<InkCanvas>())
				canvas.ResetPaint();
		}
	}

	public void ChangeBrush(int index)
	{
		if (index < 0 || index >= brushes.Count)
		{
			Debug.LogError("Brush index <" + index + "> out of range");
			return;
		}

		erase = index == eraserBrushIndex;

		currentBrush = index;
	}

	public Brush GetCurrentBrush()
	{
		return brushes[currentBrush];
	}

	public void SetErase(bool value)
	{
		erase = value;
	}

	public void Undo()
	{
		foreach (var canvas in canvases)
		{
			canvas.Undo();
		}
	}

	public void Redo()
	{
		foreach (var canvas in canvases)
		{
			canvas.Redo();
		}
	}
}
