using System;
using FlaxEngine;
using static FlaxEngine.Mathf;

#if USE_LARGE_WORLDS
using Real = System.Double;
#else
using Real = System.Single;
#endif

namespace Game.Scout.Gameplay.Camera
{
    /// <summary>
    /// Base component to implement OrbitCamera. It doesn't do anything by itself,
    /// but can be controlled by other components.
    ///
    /// Only works in world space.
    /// </summary>
    internal sealed class OrbitCamera : Script
    {
        [Serialize] [ShowInEditor]
        [Tooltip("Physical size of the camera.")]
        private Single Radius { get; set; } = 32;

        /// <summary>
        /// Rotation phi in degrees.
        /// </summary>
        private Single _phi;

        /// <summary>
        /// Rotation theta in degrees.
        /// </summary>
        private Single _theta;

        /// <summary>
        /// Offset from rig in centimeters.
        /// </summary>
        private Real _offset = 200;

        /// <summary>
        /// Rig horizontal offset from origin.
        /// </summary>
        private Real _slide;

        /// <summary>
        /// Rig vertical offset from origin.
        /// </summary>
        private Real _elevation;

        /// <summary>
        /// Rig depth (forwards/backwards) offset from origin.
        /// </summary>
        private Real _shift;

        /// <summary>
        /// Point around which to rotate.
        /// </summary>
        private Vector3 _origin;

        /// <summary>
        /// Update horizontal rotation (yaw) of the camera.
        /// </summary>
        /// <param name="value">New rotation value in degrees.</param>
        public void UpdatePhi(Single value)
        {
            this._phi = value;
        }

        /// <summary>
        /// Update vertical rotation (pitch) of the camera.
        /// </summary>
        /// <param name="value">New rotation value in degrees</param>
        public void UpdateTheta(Single value)
        {
            this._theta = value;
        }

        /// <summary>
        /// Update distance between the camera and its rig.
        /// </summary>
        /// <param name="value">New offset in cantimeters.</param>
        public void UpdateOffset(Real value)
        {
            this._offset = value;
        }

        /// <summary>
        /// Update rig horizontal offset from origin.
        /// </summary>
        /// <param name="value">New horizontal offset in centimeters.</param>
        public void UpdateSlide(Real value)
        {
            this._slide = value;
        }

        /// <summary>
        /// Update rig vertical offset from origin.
        /// </summary>
        /// <param name="value">New vertical offset in centimeters.</param>
        public void UpdateElevation(Real value)
        {
            this._elevation = value;
        }

        /// <summary>
        /// Update rig depth offset from origin.
        /// </summary>
        /// <param name="value">New depth offset in centimeters.</param>
        public void UpdateShift(Real value)
        {
            this._shift = value;
        }

        /// <summary>
        /// Update the origin of the camera.
        /// </summary>
        /// <param name="value">New origin.</param>
        public void UpdateOrigin(Vector3 value)
        {
            this._origin = value;
        }

        /// <inheritdoc />
        public override void OnLateUpdate()
        {
            var plane = Plane.Calculate(this._phi * DegreesToRadians, this._theta * DegreesToRadians);
            var rig = GetRig(this._origin, plane, this._slide, this._elevation, this._shift);
            var position = rig - plane.Tangent * this._offset;
            var clipped = Physics.RayCast(
                origin: rig,
                direction: (position - rig).Normalized,
                maxDistance: (Single) this._offset,
                hitTriggers: false,
                hitInfo: out var hit
            );
            if (clipped)
            {
                position = hit.Point + (rig - hit.Point).Normalized * this.Radius;
            }
            this.Actor.Position = position;
            this.Actor.Orientation = Quaternion.LookRotation((rig - position).Normalized);
        }

        private static Vector3 GetRig(Vector3 origin, Plane plane, Real slide, Real elevation, Real shift)
        {
            var rig = origin;
            rig += plane.Tangent * shift;
            rig += plane.Normal * elevation;
            rig += plane.Bitangent * slide;
            return rig;
        }

        private struct Plane
        {
            public readonly Vector3 Normal;
            public readonly Vector3 Tangent;
            public readonly Vector3 Bitangent;

            public Plane(Vector3 normal, Vector3 tangent, Vector3 bitangent)
            {
                this.Normal = normal;
                this.Tangent = tangent;
                this.Bitangent = bitangent;
            }

            /// <summary>
            /// Calculate camera plane with given rotation.
            /// </summary>
            /// <param name="phi">Horizontal rotation of the plane in radians.</param>
            /// <param name="theta">Vertical rotation of the plane in radians.</param>
            /// <returns>Rotated plane.</returns>
            public static Plane Calculate(Single phi, Single theta)
            {
                var normal = Vector3.Up;
                var tangent = Vector3.Forward * Quaternion.RotationAxis(normal, phi);
                var bitangent = Vector3.Cross(normal, tangent);
                normal *= Quaternion.RotationAxis(bitangent, theta);
                tangent = Vector3.Cross(bitangent, normal);
                return new Plane(normal, tangent, bitangent);
            }
        }
    }
}
