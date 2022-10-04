using System;
using FlaxEngine;

namespace Game.Scout.Gameplay.Player.Input
{
	/// <summary>
	/// PlayerInput Zoom.
	/// </summary>
	public sealed class PlayerInputZoom : Script
	{
		/// <summary>
		/// Invoked when the player zooms in.
		/// </summary>
		public event Action ZoomIn;

		/// <summary>
		/// Invoked when the player zooms out.
		/// </summary>
		public event Action ZoomOut;

		/// <inheritdoc />
		public override void OnUpdate()
		{
			var delta = FlaxEngine.Input.MouseScrollDelta;
			if (delta > 0)
			{
				this.ZoomIn?.Invoke();
			}
			if (delta < 0)
			{
				this.ZoomOut?.Invoke();
			}
		}
	}
}
