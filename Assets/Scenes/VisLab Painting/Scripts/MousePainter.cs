using System;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Linq;

public class MousePainter : MonoBehaviour
{
	[SerializeField]
	private Brush brush;

	[SerializeField]
	private bool erase = false;

	InkCanvas[] canvases;

	private bool isPainting = false;
	private HashSet<InkCanvas> currentlyPainted = new HashSet<InkCanvas>();

	private void Start()
	{
		canvases = FindObjectsOfType<InkCanvas>();
	}

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			if (!isPainting)
			{
				// Just started painting a stroke
				isPainting = true;
				Debug.Log("Started drawing");
			}

			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			bool success = true;
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo))
			{
				var paintObject = hitInfo.transform.GetComponent<InkCanvas>();

				if (paintObject != null)
				{
					// If this object has not been drawn on yet
					if (!currentlyPainted.Contains(paintObject))
					{
						// TODO: Might not be needed
					}

					currentlyPainted.Add(paintObject);

					success = erase ? paintObject.Erase(brush, hitInfo) : paintObject.Paint(brush, hitInfo);
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
			Debug.Log("Stopped drawing: " + currentlyPainted.Count);

			foreach (var inkCanvas in currentlyPainted)
			{
				inkCanvas.SaveSnapshot();
			}

			currentlyPainted.Clear();
		}

		if (Input.GetKeyDown(KeyCode.Z))
		{
			foreach (var canvas in canvases)
			{
				canvas.Undo();
			}
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			foreach (var canvas in canvases)
			{
				canvas.Redo();
			}
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

	public Brush GetBrush()
	{
		return brush;
	}

	public void SetErase(bool value)
	{
		erase = value;
	}
}
