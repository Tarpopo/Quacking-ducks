using Interfaces.SoundsTypes;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IDamagable
    {
        void ApplyDamage(int damage, Vector2 pos, float force);
        void ApplyExplosionDamage(int damage, Vector2 pos, float force, float damageRadius);
        void PlayDamageSound(ISoundVisitor visitor);
    }
}