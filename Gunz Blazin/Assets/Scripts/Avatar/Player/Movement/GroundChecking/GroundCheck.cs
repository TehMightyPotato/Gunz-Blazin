using System;
using System.Collections.Generic;
using UnityEngine;

namespace Avatar.Player.Movement.GroundChecking
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> sensorObjects = new List<GameObject>();
        
        private readonly List<IGroundSensor> _sensors = new List<IGroundSensor>();
        
        public bool IsGrounded { get; private set; }

        private void Awake()
        {
            foreach (var sensorObject in sensorObjects)
            {
                if (sensorObject.TryGetComponent<IGroundSensor>(out var comp))
                {
                    _sensors.Add(comp);
                }
            }
        }

        private void Update()
        {
            IsGrounded = CheckAllSensors();
        }

        public bool CheckAllSensors()
        {
            foreach (var sensor in _sensors)
            {
                if (sensor.GetGroundedStatus())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
