using System;
using FlaxEngine;

namespace Game.Scout.Gameplay.Camera 
{
	/// <summary>
	/// Camera that can be controlled.
	/// </summary>
	public abstract class ControlledCamera : Script
	{
		/// <summary>
		/// Update horizontal rotation (yaw) of the camera.
		/// </summary>
		/// <param name="value">New horizontal rotaton in degrees.</param>
		public abstract void UpdatePhi(Single value);
		
		/// <summary>
		/// Update vertical rotation (pitch) of the camera.
		/// </summary>
		/// <param name="value">New vertical rotation in degrees.</param>
		public abstract void UpdateTheta(Single value);
	}
}
