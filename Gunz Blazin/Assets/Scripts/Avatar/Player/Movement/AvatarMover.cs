using System;
using System.Collections;
using System.Collections.Generic;
using Avatar.Player.Movement.GroundChecking;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Avatar.Player.Movement
{
    public class AvatarMover : MonoBehaviour
    {
        [Header("Object References")]
        public Rigidbody2D rigidbodyToMove;

        public GroundCheck groundCheck;

        [Header("Movement")]
        public float groundedMovementModifier;

        public float aerialMovementModifier;

        public ForceMode2D movementForceMode;

        [Header("Jumping")]
        public float initialJumpForceModifier;

        public ForceMode2D initialJumpForceMode;

        [Header("Drag")]
        [Range(0, 10)]
        public float aerialDragModifier;

        [Range(0, 10)]
        public float groundedDragModifier;

        [Header("Falling Modifications")]
        public float fallModifier;

        public float maxFallingVelocityForceApplicationThreshold;

        [Header("Restrictions")]
        public float maxVerticalVelocity;

        private bool _grounded;
        private float _movementDirection;
        private bool _jumpInput;
        private Coroutine _jumpCoroutine;
        private readonly WaitForFixedUpdate _waitForFixedUpdate = new();

        private void FixedUpdate()
        {
            HandleMovement();
            HandleDrag();
            HandleFallModification();
        }

        private void HandleMovement()
        {
            if (_movementDirection is < 0.1f and > -0.1f) return;
            if (Math.Abs(rigidbodyToMove.velocity.x) > maxVerticalVelocity &&
                Math.Sign(_movementDirection) == Math.Sign(rigidbodyToMove.velocity.x)) return;
            if (groundCheck.IsGrounded)
            {
                rigidbodyToMove.AddForce(new Vector2(_movementDirection * groundedMovementModifier, 0),
                    movementForceMode);
            }
            else
            {
                rigidbodyToMove.AddForce(new Vector2(_movementDirection * aerialMovementModifier, 0),
                    movementForceMode);
            }
        }

        private void HandleDrag()
        {
            if (_movementDirection is >= 0.1f or <= -0.1f) return;
            rigidbodyToMove.AddForce(groundCheck.IsGrounded
                ? new Vector2(-rigidbodyToMove.velocity.x * groundedDragModifier, 0)
                : new Vector2(-rigidbodyToMove.velocity.x * aerialDragModifier, 0));
        }

        private void HandleFallModification()
        {
            if (!groundCheck.IsGrounded && (!_jumpInput || rigidbodyToMove.velocity.y < 0) &&
                rigidbodyToMove.velocity.y < maxFallingVelocityForceApplicationThreshold)
            {
                rigidbodyToMove.AddForce(Vector2.down * fallModifier);
            }
        }

        #region InputHandling

        public void MoveInput(InputAction.CallbackContext context)
        {
            _movementDirection = context.ReadValue<float>();
        }

        public void JumpInputPerformed(InputAction.CallbackContext context)
        {
            if (_jumpCoroutine != null || !groundCheck.IsGrounded) return;
            _jumpCoroutine = StartCoroutine(JumpCoroutine());
            _jumpInput = true;
        }

        public void JumpInputCanceled(InputAction.CallbackContext context)
        {
            _jumpInput = false;
        }

        #endregion

        private IEnumerator JumpCoroutine()
        {
            yield return _waitForFixedUpdate;
            rigidbodyToMove.AddForce(Vector2.up * initialJumpForceModifier, initialJumpForceMode);
            _jumpCoroutine = null;
        }
    }
}