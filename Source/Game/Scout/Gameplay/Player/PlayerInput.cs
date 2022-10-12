using FlaxEngine;
using Game.Scout.Gameplay.Player.Input;

namespace Game.Scout.Gameplay.Player
{
    /// <summary>
    /// PlayerInput.
    /// </summary>
    [Category("Player/Input")]
    internal sealed class PlayerInput : Script
    {
        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Player Camera")]
        private PlayerCamera PCamera { get; set; }

        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Zoom Input")]
        private PlayerZoomInput PZoom { get; set; }

        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Look Input")]
        private PlayerLookInput PLook { get; set; }

        /// <inheritdoc />
        public override void OnEnable()
        {
            this.PZoom.ZoomIn += this.ZoomCameraIn;
            this.PZoom.ZoomOut += this.ZoomCameraOut;
            this.PLook.Look += this.RotateCamera;
        }

        /// <inheritdoc />
        public override void OnDisable()
        {
            this.PZoom.ZoomIn -= this.ZoomCameraIn;
            this.PZoom.ZoomOut -= this.ZoomCameraOut;
            this.PLook.Look -= this.RotateCamera;
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

        /// <summary>
        /// Rotate player camera.
        /// </summary>
        /// <param name="delta">Rotation delta.</param>
        private void RotateCamera(Float2 delta)
        {
            this.PCamera.Rotate(delta.X, delta.Y);
        }
    }
}
