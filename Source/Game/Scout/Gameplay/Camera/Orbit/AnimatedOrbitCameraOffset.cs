using System;
using FlaxEngine;

namespace Game.Scout.Gameplay.Camera.Orbit
{
	/// <summary>
	/// Animated OrbitCamera Offset.
	/// </summary>
	public sealed class AnimatedOrbitCameraOffset : Script
	{
		[ShowInEditor, Serialize]
		[EditorDisplay(name: "Camera")]
		private OrbitCamera POrbitCamera { get; set; }

		[ShowInEditor, Serialize]
		[EditorDisplay(name: "Alignment Time")]
		[Tooltip("Time to snap in milliseconds.")]
		private Single PTime { get; set; }

		/// <summary>
		/// Current offset in centimeters.
		/// </summary>
		[Serialize]
		private Single _current;

		/// <summary>
		/// Desired offset in centimeters.
		/// </summary>
		[Serialize]
		private Single _destination;

		/// <summary>
		/// Move offset.
		/// </summary>
		/// <param name="destination">Desired offset in centimeters.</param>
		public void Move(Single destination)
		{
			this._destination = destination;
		}

		/// <inheritdoc />
		public override void OnUpdate()
		{
			var time = (1 / (this.PTime / 1000f)) * Time.UnscaledDeltaTime;
			this._current = Mathf.Lerp(this._current, this._destination, Mathf.Clamp(time, 0f, 1f));
			this.POrbitCamera.UpdateOffset(this._current);
		}
	}
}
