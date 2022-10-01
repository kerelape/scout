using System;
using FlaxEngine;

namespace Game.Scout.Gameplay 
{
	/// <summary>
	/// Rotation component.
	/// </summary>
	public abstract class Rotation : Script 
	{
		/// <summary>
		/// Add vertical rotation.
		/// </summary>
		/// <param name="delta">Rotation delta in degrees.</param>
		public abstract void RotateVertically(Single delta);

		/// <summary>
		/// Add horizontal rotation.
		/// </summary>
		/// <param name="delta">Rotation delta in degrees.</param>
		public abstract void RotateHorizontally(Single delta);

		/// <summary>
		/// Add rotation.
		/// </summary>
		/// <param name="phi">Horizontal rotation delta in degrees.</param>
		/// <param name="theta">Vertical rotation delta in degrees.</param>
		public void Rotate(Single phi, Single theta) 
		{
			this.RotateHorizontally(phi);
			this.RotateVertically(theta);
		}
	}
}
