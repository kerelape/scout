using System;
using FlaxEngine;
using Game.Scout.Gameplay.Player.Camera;

namespace Game.Scout.Gameplay.Player
{
    /// <summary>
    /// Player camera.
    /// </summary>
    [Category("Player/Camera")]
    internal sealed class PlayerCamera : Script
    {
        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Slide")]
        private PlayerCameraSlide PSlide { get; set; }

        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Rotation")]
        private PlayerCameraRotation PRotation { get; set; }

        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Zoom")]
        private PlayerCameraZoom PZoom { get; set; }

        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Combat Mode Slide")]
        private Single PCombatSlide { get; set; }

        /// <summary>
        /// Zoom the camera in.
        /// </summary>
        public void ZoomIn()
        {
            this.PZoom.ZoomIn();
        }

        /// <summary>
        /// Zoom the camera out.
        /// </summary>
        public void ZoomOut()
        {
            this.PZoom.ZoomOut();
        }

        /// <summary>
        /// Rotate the camera.
        /// </summary>
        /// <param name="phiDelta">Horizontal rotation delta in degrees.</param>
        /// <param name="thetaDelta">Vertical rotation delta in degrees.</param>
        public void Rotate(Single phiDelta, Single thetaDelta)
        {
            this.PRotation.RotateHorizontally(phiDelta);
            this.PRotation.RotateVertically(thetaDelta);
        }

        /// <summary>
        /// Enter combat mode.
        /// </summary>
        public void EnterCombatMode()
        {
            this.PSlide.Update(this.PCombatSlide);
        }

        /// <summary>
        /// Exit combat mode.
        /// </summary>
        public void ExitCombatMode()
        {
            this.PSlide.Update(0);
        }
    }
}
