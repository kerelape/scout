using System;
using FlaxEngine;

namespace Game.Scout.Gameplay.Camera
{
	/// <summary>
	/// Camera rotation component.
	/// </summary>
	public sealed class CameraRotation : Rotation
	{
		[ShowInEditor, Serialize]
		[EditorDisplay(name: "Camera")]
		[Tooltip("The camera to rotate.")]
		private ControlledCamera PCamera { get; set; }

		[ShowInEditor, Serialize]
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
		public override void RotateHorizontally(Single delta)
		{
			this._phi += delta;
			this.PCamera.UpdatePhi(this._phi);
		}

		/// <inheritdoc />
		public override void RotateVertically(Single delta)
		{
			this._theta = Mathf.Clamp(this._theta + delta, -this.PLimit, this.PLimit);
			this.PCamera.UpdateTheta(this._theta);
		}

		/// <inheritdoc />
		public override void OnStart()
		{
			this.Rotate(this._phi, this._theta);
		}
	}
}

