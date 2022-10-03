using System;
using FlaxEngine;
using Game.Scout.Gameplay.Camera;

namespace Game.Scout.Gameplay.Player.Camera
{
	/// <summary>
	/// Player camera slide.
	/// </summary>
	public sealed class PlayerCameraSlide : Script
	{
		[Serialize] [ShowInEditor]
		[EditorDisplay(name: "Camera")]
		[EditorOrder(-1)]
		private OrbitCamera POrbitCamera { get; set; }

		[Serialize] [ShowInEditor]
		[EditorDisplay(name: "Alignment Time")]
		[Tooltip("Time to snap in milliseconds.")]
		private Single PTime { get; set; }

		/// <summary>
		/// Desired slide in centimeters.
		/// </summary>
		[Serialize]
		private Single _destination;

		/// <summary>
		/// Current slide in centimeters.
		/// </summary>
		[Serialize]
		private Single _current;

		/// <summary>
		/// Update slide.
		/// </summary>
		/// <param name="value">New slide in centimeters.</param>
		public void Update(Single value)
		{
			this._destination = value;
		}

		/// <inheritdoc />
		public override void OnUpdate()
		{
			var time = (1 / (this.PTime / 1000f)) * Time.UnscaledDeltaTime;
			this._current = Mathf.Lerp(this._current, this._destination, Mathf.Clamp(time, 0f, 1f));
			this.POrbitCamera.UpdateSlide(this._current);
		}
	}
}
