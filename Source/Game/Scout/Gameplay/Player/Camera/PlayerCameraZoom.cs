using System;
using FlaxEngine;
using Game.Scout.Gameplay.Camera;

namespace Game.Scout.Gameplay.Player.Camera
{
    /// <summary>
    /// Player camera zoom.
    /// </summary>
    [Category("Player/Camera")]
    internal sealed class PlayerCameraZoom : Script
    {
        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Camera")]
        [EditorOrder(-1)]
        private OrbitCamera POrbitCamera { get; set; }

        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Alignment Time")]
        [Tooltip("Time to snap in milliseconds.")]
        private Single PTime { get; set; }

        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Step")]
        [Tooltip("Zoom step in percent.")]
        [Range(0.001f, 1f)]
        private Single PStep { get; set; }

        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Minimum Offset")]
        [Tooltip("Minimum offset from the player in centimeters.")]
        private Single PMinOffset { get; set; }

        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Maximum Offset")]
        [Tooltip("Maximum offset from the player in centimeters.")]
        private Single PMaxOffset { get; set; }

        /// <summary>
        /// Destination zoom level.
        /// </summary>
        [Serialize] private Single _zoom;

        /// <summary>
        ///	Current zoom level.
        /// </summary>
        [Serialize] private Single _current;

        /// <summary>
        /// Zoom the camera in.
        /// </summary>
        public void ZoomIn()
        {
            this.Update(this._zoom + this.PStep);
        }

        /// <summary>
        /// Zoom the camera out.
        /// </summary>
        public void ZoomOut()
        {
            this.Update(this._zoom - this.PStep);
        }

        /// <summary>
        /// Zoom the camera to the desired level.
        /// </summary>
        /// <param name="value">Desired zoom level (0-1).</param>
        public void Update(Single value)
        {
            this._zoom = Mathf.Clamp(value, 0f, 1f);
        }

        /// <inheritdoc />
        public override void OnStart()
        {
            if (this.PMinOffset >= this.PMaxOffset)
            {
                throw new ArgumentException("Maximum offset must be more than the minimum.");
            }

            this.Update(0.5f);
        }

        /// <inheritdoc />
        public override void OnUpdate()
        {
            var time = (1 / (this.PTime / 1000f)) * Time.UnscaledDeltaTime;
            var destination = Mathf.Lerp(this.PMaxOffset, this.PMinOffset, this._zoom);
            this._current = Mathf.Lerp(this._current, destination, Mathf.Clamp(time, 0f, 1f));
            this.POrbitCamera.UpdateOffset(this._current);
        }
    }
}
