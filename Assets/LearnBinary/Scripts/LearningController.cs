using MixedRealityToolkit.Common;
using MixedRealityToolkit.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearningController : MonoBehaviour
{
	#region Inspector Fields
	[Tooltip("The bit manager the scene.")]
	public BitManager bitManager;

	[Tooltip("The text label that provides captions.")]
	public Text captionsText;

	[Tooltip("The cursor to be used with the Directional Indicator.")]
	public GameObject cursor;

	[Tooltip("The text label that represents total values.")]
	public Text totalText;
	#endregion // Inspector Fields

	#region Behavior Overrides
	void OnDisable()
	{
		// Unsubscribe from events
		bitManager.TotalValueChanged -= BitManager_TotalValueChanged;
	}

	void OnEnable()
	{
		// Reset all bits
		bitManager.ResetAllBits();

		// Set defaults
		captionsText.text = "Learning Mode";
		totalText.text = "0";

		// Subscribe to events
		bitManager.TotalValueChanged += BitManager_TotalValueChanged;
	}

	void Start()
	{
		// Validate components
		if (bitManager == null)
		{
			Debug.LogErrorFormat("The {0} inspector field is not set and is required. {1} did not load completely.", "bitManager", this.GetType().Name);
			return;
		}
		if (cursor == null)
		{
			Debug.LogErrorFormat("The {0} inspector field is not set and is required. {1} did not load completely.", "cursor", this.GetType().Name);
			return;
		}
		if (captionsText == null)
		{
			Debug.LogErrorFormat("The {0} inspector field is not set and is required. {1} did not load completely.", "captionsText", this.GetType().Name);
			return;
		}
		if (totalText == null)
		{
			Debug.LogErrorFormat("The {0} inspector field is not set and is required. {1} did not load completely.", "totalText", this.GetType().Name);
			return;
		}
	}
	#endregion // Behavior Overrides

	#region Overrides / Event Handlers
	private void BitManager_TotalValueChanged(object sender, System.EventArgs e)
	{
		// Update the total block
		totalText.text = bitManager.TotalValue.ToString();
	}
	#endregion // Overrides / Event Handlers
}
