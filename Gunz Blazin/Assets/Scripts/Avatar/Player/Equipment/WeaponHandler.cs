using System;
using System.Collections;
using System.Collections.Generic;
using Equipment.Interfaces;
using Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponObject;

    private IWeapon _weapon;

    private UnityEngine.Camera _mainCam;

    private void Start()
    {
        _mainCam = UnityEngine.Camera.main;
    }

    public void Aim(InputAction.CallbackContext context)
    {
        var mousePosition = context.action.ReadValue<Vector2>();
        var viewPortPosition = _mainCam.ScreenToWorldPoint(mousePosition);
        viewPortPosition.z = 0;
        Debug.DrawRay(transform.position, transform.position.DirectionTowardsNormalized(viewPortPosition)* 4);
    }
    
    public void Attack(InputAction.CallbackContext context)
    {
        // _weapon.Attack();
    }



    public void SwitchWeapon(GameObject weapon)
    {
        weaponObject = weapon;
        weaponObject.TryGetComponent<IWeapon>(out _weapon);
    }
    
}
