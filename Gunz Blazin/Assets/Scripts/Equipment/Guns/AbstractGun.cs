using System;
using Equipment.Guns.Projectiles.Interfaces;
using Equipment.Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace Equipment.Guns
{
    public abstract class AbstractGun : ScriptableObject, IWeapon
    {
        [SerializeField]
        protected Sprite sprite;

        [SerializeField]
        protected GameObject projectilePrefab;

        protected ObjectPool<GameObject> projectilePool;

        [SerializeField]
        protected int maxAmmo;

        protected int currentAmmo;

        private void Awake()
        {
            currentAmmo = maxAmmo;
            projectilePool = new ObjectPool<GameObject>(CreatePooledProjectile, OnProjectileTakenFromPool,
                OnProjectileReturnedToPool, OnDestroyPooledProjectile);
        }

        public abstract void Attack();

        protected virtual GameObject CreatePooledProjectile()
        {
            return Instantiate(projectilePrefab);
        }

        protected virtual void OnProjectileTakenFromPool(GameObject obj)
        {
            
        }

        protected virtual void OnProjectileReturnedToPool(GameObject obj)
        {
            if (obj.TryGetComponent<IProjectile>(out var proj))
            {
                proj.ResetState();
            }
            obj.SetActive(false);
        }

        protected virtual void OnDestroyPooledProjectile(GameObject obj)
        {
            Destroy(obj);
        }
    }
}