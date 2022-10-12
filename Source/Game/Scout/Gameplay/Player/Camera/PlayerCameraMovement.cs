using System;
using FlaxEngine;
using Game.Scout.Gameplay.Camera;

namespace Game.Scout.Gameplay.Player.Camera
{
    /// <summary>
    /// Player camera movement.
    /// </summary>
    [Category("Player/Camera")]
    internal sealed class PlayerCameraMovement : Script
    {
        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Camera")]
        private OrbitCamera PCamera { get; set; }

        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Target", group: "Following")]
        private Actor PTarget { get; set; }

        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Speed", group: "Following")]
        private Single PSpeed { get; set; }

        /// <summary>
        /// Destination to move to.
        /// </summary>
        private Vector3 _destination;

        /// <inheritdoc />
        public override void OnStart()
        {
            if (this.PCamera == null)
                throw new PropertyNotSetException(nameof(this.PCamera));
            if (this.PTarget == null)
                throw new PropertyNotSetException(nameof(this.PTarget));
            if (this.PSpeed <= 0.001f)
                throw new ConfigurationException("Speed is too low.", new ArgumentOutOfRangeException());
        }

        /// <inheritdoc />
        public override void OnLateUpdate()
        {
            var target = this.PTarget.Position;
            this._destination = Vector3.Lerp(this._destination, target, this.PSpeed * Time.UnscaledDeltaTime);
            this.PCamera.UpdateOrigin(this._destination);
        }
    }
}
