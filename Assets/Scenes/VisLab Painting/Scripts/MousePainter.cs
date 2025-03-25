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
				if (!isPainting)
				{
					// Just started painting a stroke
					isPainting = true;
					// Debug.Log("Started drawing");
				}

				var paintObject = hitInfo.transform.GetComponent<InkCanvas>();

				if (paintObject != null)
				{
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

	public Brush GetBrush()
	{
		return brush;
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
