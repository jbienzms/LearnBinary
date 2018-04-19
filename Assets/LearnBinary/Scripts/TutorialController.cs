using MixedRealityToolkit.Common;
using MixedRealityToolkit.SpatialMapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
	#region Inspector Fields
	[Tooltip("The bit manager the scene.")]
	public BitManager BitManager;

	[Tooltip("The PlayableDirector that animates the tutorial.")]
	public PlayableDirector Director;
	#endregion // Inspector Fields

	#region Behavior Overrides
	void OnEnable()
	{
		// Reset all bits
		BitManager.ResetAllBits();

		// Start playback
		Director.Stop();
		Director.Play();
	}

	void Start()
	{
		// Validate components
		if (BitManager == null)
		{
			Debug.LogErrorFormat("The {0} inspector field is not set and is required. {1} did not load completely.", "bitManager", this.GetType().Name);
			return;
		}
		if (Director == null)
		{
			Debug.LogErrorFormat("The {0} inspector field is not set and is required. {1} did not load completely.", "Director", this.GetType().Name);
			return;
		}
	}
	#endregion // Behavior Overrides

	public void Pause()
	{
		if (Director.state != PlayState.Paused)
		{
			Director.Pause();
		}
	}
}
