using System;
using FlaxEngine;
using Game.Scout.Gameplay.Camera;

namespace Game.Scout.Gameplay.Player.Camera
{
	/// <summary>
	/// Camera rotation component.
	/// </summary>
	internal sealed class PlayerCameraRotation : Script
	{
		[Serialize] [ShowInEditor]
		[EditorDisplay(name: "Camera")]
		[Tooltip("The camera to rotate.")]
		private OrbitCamera PCamera { get; set; }

		[Serialize] [ShowInEditor]
		[EditorDisplay(name: "Limit")]
		[Tooltip("Vertical rotation limit in degrees.")]
		private Single PLimit { get; set; }

		/// <summary>
		/// Current horizontal rotation in degrees.
		/// </summary>
		[Serialize]
		private Single _phi;

		/// <summary>
		/// Current vertical rotation in degrees.
		/// </summary>
		[Serialize]
		private Single _theta;

		/// <inheritdoc />
		public void RotateHorizontally(Single delta)
		{
			this._phi += delta;
			this.PCamera.UpdatePhi(this._phi);
		}

		/// <inheritdoc />
		public void RotateVertically(Single delta)
		{
			delta *= (this.PLimit * 2f) / 360f;
			this._theta = Mathf.Clamp(this._theta + delta, -this.PLimit, this.PLimit);
			this.PCamera.UpdateTheta(this._theta);
		}

		/// <inheritdoc />
		public override void OnStart()
		{
			// Restore rotation on load
			this.PCamera.UpdatePhi(this._phi);
			this.PCamera.UpdateTheta(this._theta);
		}
	}
}

