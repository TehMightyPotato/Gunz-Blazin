using UnityEngine;

namespace Avatar.Player.Movement.GroundChecking
{
    public class RaycastGroundSensor : MonoBehaviour, IGroundSensor
    {
        [SerializeField]
        private float raycastDistance;

        [SerializeField]
        private LayerMask layerMask;

        [SerializeField]
        private bool showGizmos;

        public bool GetGroundedStatus()
        {
            var result = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, layerMask);
            if (showGizmos)
            {
                if (result.collider != null)
                {
                    Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.green);
                }
                else
                {
                    Debug.DrawRay(transform.position, Vector3.down * raycastDistance, Color.red);
                }
            }
            return result.collider != null;
        }
    }
}