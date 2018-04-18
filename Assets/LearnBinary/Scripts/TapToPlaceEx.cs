using MixedRealityToolkit.InputModule.EventData;
using MixedRealityToolkit.InputModule.InputHandlers;
using MixedRealityToolkit.SpatialMapping;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlaceEx : TapToPlace, IInputClickHandler
{
	private void NotifyPlace(bool wasBeingPlaced)
	{
		if (wasBeingPlaced != IsBeingPlaced)
		{
			if (IsBeingPlaced)
			{
				if (PlacingStarted != null) { PlacingStarted(this, EventArgs.Empty); }
			}
			else
			{
				if (PlacingStopped != null) { PlacingStopped(this, EventArgs.Empty); }
			}
		}
	}

	protected override void Start()
	{
		bool wasBeinglacing = IsBeingPlaced;
		base.Start();
		NotifyPlace(wasBeinglacing);
	}

	public override void OnInputClicked(InputClickedEventData eventData)
	{
		bool wasBeinglacing = IsBeingPlaced;
		base.OnInputClicked(eventData);
		NotifyPlace(wasBeinglacing);
	}

	public event EventHandler PlacingStarted;
	public event EventHandler PlacingStopped;
}
