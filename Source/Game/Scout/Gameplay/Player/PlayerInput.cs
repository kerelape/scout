using FlaxEngine;
using Game.Scout.Gameplay.Player.Input;

namespace Game.Scout.Gameplay.Player
{
	/// <summary>
	/// PlayerInput.
	/// </summary>
	public sealed class PlayerInput : Script
	{
		[Serialize] [ShowInEditor]
		[EditorDisplay(name: "Player Camera")]
		private PlayerCamera PCamera { get; set; }

		[Serialize] [ShowInEditor]
		[EditorDisplay(name: "Zoom Input")]
		private PlayerInputZoom PZoom { get; set; }

		/// <inheritdoc />
		public override void OnEnable()
		{
			this.PZoom.ZoomIn += this.ZoomCameraIn;
			this.PZoom.ZoomOut += this.ZoomCameraOut;
		}

		/// <inheritdoc />
		public override void OnDisable()
		{
			this.PZoom.ZoomIn -= this.ZoomCameraIn;
			this.PZoom.ZoomOut -= this.ZoomCameraOut;
		}

		/// <summary>
		/// Zoom player camera in.
		/// </summary>
		private void ZoomCameraIn()
		{
			this.PCamera.ZoomIn();
		}

		/// <summary>
		/// Zoom player camera out.
		/// </summary>
		private void ZoomCameraOut()
		{
			this.PCamera.ZoomOut();
		}
	}
}
