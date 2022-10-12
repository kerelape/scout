using System;
using FlaxEngine;

namespace Game.Scout.Gameplay.Player.Input
{
    /// <summary>
    /// Player look input.
    /// </summary>
    [Category("Player/Input")]
    internal sealed class PlayerLookInput : Script
    {
        /// <summary>
        /// Invoked when player looks around.
        /// </summary>
        public event Action<Float2> Look;

        [Serialize]
        [ShowInEditor]
        [EditorDisplay(name: "Sensitivity")]
        private Single PSensitivity { get; set; } = 1f;

        /// <inheritdoc />
        public override void OnUpdate()
        {
            var delta = FlaxEngine.Input.MousePositionDelta;
            if (delta.Length > 0f)
            {
                this.Look?.Invoke(delta * this.PSensitivity);
            }
        }
    }
}
