public interface IDamageable
{
    void getDamage(float dmg);
    void freeze(float num);
    void knockback(float knockbackDist, float freezeTime);
}