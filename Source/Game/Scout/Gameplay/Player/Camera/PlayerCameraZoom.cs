using System;
using FlaxEngine;
using Game.Scout.Gameplay.Camera.Orbit;

namespace Game.Scout.Gameplay.Player.Camera
{
	/// <summary>
	/// Player camera zoom.
	/// </summary>
	public sealed class PlayerCameraZoom : Script
	{
		[Serialize] [ShowInEditor]
		[EditorDisplay(name: "Camera")] [EditorOrder(-1)]
		private OrbitCamera POrbitCamera { get; set; }

		[Serialize] [ShowInEditor]
		[EditorDisplay(name: "Alignment Time")]
		[Tooltip("Time to snap in milliseconds.")]
		private Single PTime { get; set; }

		[Serialize] [ShowInEditor]
		[EditorDisplay(name: "Step")]
		[Tooltip("Zoom step in percent.")] [Range(0.001f, 1f)]
		private Single PStep { get; set; }

		[Serialize] [ShowInEditor]
		[EditorDisplay(name: "Minimum Offset")]
		[Tooltip("Minimum offset from the player in centimeters.")]
		private Single PMinOffset { get; set; }

		/// <summary>
		///	Maximum offset from the target in player.
		/// </summary>
		[Serialize]
		private Single _maxOffset;

		/// <summary>
		/// Destination zoom level.
		/// </summary>
		[Serialize]
		private Single _zoom;

		/// <summary>
		///	Current zoom level.
		/// </summary>
		[Serialize]
		private Single _current;

		/// <summary>
		/// Zoom the camera in.
		/// </summary>
		public void ZoomIn()
		{
			this.Zoom(this._zoom + this.PStep);
		}

		/// <summary>
		/// Zoom the camera out.
		/// </summary>
		public void ZoomOut()
		{
			this.Zoom(this._zoom - this.PStep);
		}

		/// <summary>
		/// Zoom the camera to the desired level.
		/// </summary>
		/// <param name="level">Desired zoom level (0-1).</param>
		public void Zoom(Single level)
		{
			this._zoom = Mathf.Clamp(level, 0f, 1f);
		}

		/// <summary>
		/// Update maximum offset from the player.
		/// </summary>
		/// <param name="value">New offset in centimeters.</param>
		public void UpdateMaximumOffset(Single value)
		{
			if (value <= this.PMinOffset)
			{
				throw new ArgumentOutOfRangeException("Maximum offset cannot be less than the minimum.");
			}
			this._maxOffset = value;
		}

		/// <inheritdoc />
		public override void OnStart()
		{
			this.Zoom(0.5f);
		}

		/// <inheritdoc />
		public override void OnUpdate()
		{
			var time = (1 / (this.PTime / 1000f)) * Time.UnscaledDeltaTime;
			var destination = Mathf.Lerp(this._maxOffset, this.PMinOffset, this._zoom);
			this._current = Mathf.Lerp(this._current, destination, Mathf.Clamp(time, 0f, 1f));
			this.POrbitCamera.UpdateOffset(this._current);
		}
	}
}
