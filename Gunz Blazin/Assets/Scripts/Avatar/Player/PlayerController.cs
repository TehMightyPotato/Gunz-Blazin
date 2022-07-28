using System;
using System.Runtime.CompilerServices;
using Avatar.Player.Movement;
using Camera;
using Input;
using Mirror;
using UnityEngine;

namespace Avatar.Player
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField]
        private AvatarMover avatarMover;

        [SerializeField]
        private WeaponHandler weaponHandler;

        private InputMaster _input;


        public override void OnStartLocalPlayer()
        {
            if (isLocalPlayer)
            {
                _input = new InputMaster();
                SubscribeInputEvents();
                _input.Player.Enable();
            }
        }

        public override void OnStartClient()
        {
            if (isClient)
            {
                CameraTargetGroup.Instance.RegisterPlayer(transform);
            }
        }

        private void SubscribeInputEvents()
        {
            _input.Player.Jump.performed += avatarMover.JumpInputPerformed;
            _input.Player.Jump.canceled += avatarMover.JumpInputCanceled;
            _input.Player.Movement.performed += avatarMover.MoveInput;
            _input.Player.Aim.performed += weaponHandler.Aim;
            _input.Player.Attack.performed += weaponHandler.Attack;
        }
        
        private void UnsubscribeInputEvents()
        {
            _input.Player.Jump.performed -= avatarMover.JumpInputPerformed;
            _input.Player.Jump.canceled -= avatarMover.JumpInputCanceled;
            _input.Player.Movement.performed -= avatarMover.MoveInput;
            _input.Player.Aim.performed -= weaponHandler.Aim;
            _input.Player.Attack.performed -= weaponHandler.Attack;
        }

        public override void OnStopLocalPlayer()
        {
            if (!isLocalPlayer) return;
            _input.Player.Disable();
            UnsubscribeInputEvents();
        }

        private void OnDestroy()
        {
            if (isClient)
            {
                CameraTargetGroup.Instance.RemovePlayer(transform);
            }
        }
    }
}