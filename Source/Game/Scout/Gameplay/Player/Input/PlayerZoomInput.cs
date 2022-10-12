using System;
using FlaxEngine;

namespace Game.Scout.Gameplay.Player.Input
{
    /// <summary>
    /// Player zoom input.
    /// </summary>
    [Category("Player/Input")]
    internal sealed class PlayerZoomInput : Script
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
