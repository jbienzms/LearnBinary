﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	#region Inspector Fields
	[Tooltip("The controller for game mode")]
	public LearningController LearningController;

	[Tooltip("The controller for game mode")]
	public GameController GameController;

	[Tooltip("The TapToPlace used to place the whole scene.")]
	public TapToPlaceEx ScenePlacement;

	[Tooltip("The collider used to represent the whole scene.")]
	public Collider SceneCollider;
	#endregion // Inspector Fields

	#region Behavior Overrides
	void OnDisable()
	{
		// Unsubscribe from events
		ScenePlacement.PlacingStopped -= ScenePlacement_PlacingStopped;
	}

	void OnEnable()
	{
		// Subscribe to events
		ScenePlacement.PlacingStopped += ScenePlacement_PlacingStopped;
	}

	void Start()
	{
		// Validate components
		if (ScenePlacement == null)
		{
			Debug.LogErrorFormat("The {0} inspector field is not set and is required. {1} did not load completely.", "ScenePlacement", this.GetType().Name);
			Destroy(this);
			return;
		}
		if (SceneCollider == null)
		{
			Debug.LogErrorFormat("The {0} inspector field is not set and is required. {1} did not load completely.", "SceneCollider", this.GetType().Name);
			Destroy(this);
			return;
		}

		// If not running in the editor, start placing
		if (!Application.isEditor)
		{
			SceneCollider.enabled = true;
			ScenePlacement.enabled = true;
			ScenePlacement.IsBeingPlaced = true;
		}
		else
		{
			// Start default mode
			StartDefault();
		}
	}
	#endregion // Behavior Overrides

	#region Public Methods
	public void StartDefault()
	{
		// StartLearning();
		StartGame();
	}

	public void StartGame()
	{
		LearningController.enabled = false;
		GameController.enabled = true;
	}

	public void StartLearning()
	{
		GameController.enabled = false;
		LearningController.enabled = true;
	}
	#endregion // Public Methods

	#region Overrides / Event Handlers
	private void ScenePlacement_PlacingStopped(object sender, System.EventArgs e)
	{
		// Can never place again!
		ScenePlacement.enabled = false;
		SceneCollider.enabled = false;

		// Done placing, start default mode
		StartDefault();
	}
	#endregion // Overrides / Event Handlers
}
