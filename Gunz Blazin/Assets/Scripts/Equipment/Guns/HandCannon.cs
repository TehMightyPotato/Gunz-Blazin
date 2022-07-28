namespace Equipment.Guns
{
    public class HandCanon : AbstractGun
    {
        public override void Attack()
        {
            currentAmmo -= 1;
            var proj = projectilePool.Get();
        }
    }
}
