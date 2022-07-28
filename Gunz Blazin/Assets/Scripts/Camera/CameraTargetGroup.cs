using Cinemachine;
using Mirror;
using UnityEngine;

namespace Camera
{
    public class CameraTargetGroup : NetworkBehaviour
    {
        public static CameraTargetGroup Instance { get; private set; }
        public int localPlayerWeight;
        public int remotePlayerWeight;
        public int radius;

        [SerializeField]
        private CinemachineTargetGroup _targetGroup;
        private void Awake()
        {
            Instance = this;
        }

        public void RegisterPlayer(Transform player)
        {
            if (isLocalPlayer)
            {
                _targetGroup.AddMember(player, localPlayerWeight,radius);
            }
            else
            {
                _targetGroup.AddMember(player, remotePlayerWeight,radius);
            }
        }

        public void RemovePlayer(Transform player)
        {
            _targetGroup.RemoveMember(player);
        }

    }
}
