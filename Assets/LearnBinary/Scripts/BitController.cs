using MixedRealityToolkit.InputModule.EventData;
using MixedRealityToolkit.InputModule.InputHandlers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BitController : MonoBehaviour, IInputClickHandler
{
    private bool isOn;
    public int power;
    private int value;
    private bool valueOverride;


    private Light bitlight;
    public Animator bitAnimator;
    public Text valueText;
    public Text valueSign;

    public Material on;
    public Material off;
    public GameObject filament;


	#region Behavior Overrides
	// Use this for initialization
	void Start()
	{
		CalculateValue();
	}
	#endregion // Behavior Overrides

	#region Input Handlers
	void IInputClickHandler.OnInputClicked(InputClickedEventData eventData)
	{
		AnimateToggle();
	}
	#endregion // Input Handlers

	#region Public Methods
	/// <summary>
	/// Animates a switch between the on / off state
	/// </summary>
	public void AnimateToggle()
	{
		AnimateSwitch(!isOn);
	}

	/// <summary>
	/// Animates the switch to the specified state.
	/// </summary>
	/// <param name="on">
	/// Whether the switch should be on or off.
	/// </param>
	public void AnimateSwitch(bool on)
	{
		if (bitAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
		{
			bitAnimator.SetBool("toggleSwitch", on);
		}
	}

	/// <summary>
	/// Calculates the current value and updates materials.
	/// </summary>
	public void CalculateValue()
	{
		int realValue = (int)Mathf.Pow(2f, power);
		valueSign.text = realValue.ToString();
		Value = (isOn ? realValue : 0);
		if (valueText != null)
		{
			valueText.text = (isOn | valueOverride ? realValue.ToString() : "0");
		}

		if (valueSign != null)
		{
			valueSign.text = realValue.ToString();
		}

		if (isOn)
		{
			filament.GetComponent<Renderer>().material = on;
		}
		else
		{
			Value = 0;
			filament.GetComponent<Renderer>().material = off;
		}
	}

	/// <summary>
	/// Jumps directly to the specified state without any animate.
	/// </summary>
	/// <param name="on">
	/// Whether the switch should be on or off.
	/// </param>
	public void SetSwitch(int on)
	{
		isOn = (on != 0);
		CalculateValue();
	}

	public void OnInputClicked(InputClickedEventData eventData)
	{
		throw new NotImplementedException();
	}
	#endregion // Public Methods

	#region Public Properties
	/// <summary>
	/// Gets a value that indicates if the bit is on.
	/// </summary>
	public bool IsOn
	{
		get { return isOn; }
	}

	/// <summary>
	/// Gets the current value of the bit based on its state and power.
	/// </summary>
	public int Value
	{
		get
		{
			return value;
		}
		private set
		{
			this.value = value;
			if (ValueChanged != null)
			{
				ValueChanged(this, EventArgs.Empty);
			}
		}
	}

	public bool ValueOverride
	{
		get
		{
			return valueOverride;
		}

		set
		{
			valueOverride = value;
			CalculateValue();
		}
	}

	public Light Bitlight
	{
		get
		{
			return bitlight;
		}

		set
		{
			bitlight = value;
		}
	}
	#endregion // Public Properties

	#region Public Events
	public event EventHandler ValueChanged;
	#endregion // Public Events
}
