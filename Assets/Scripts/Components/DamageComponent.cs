using System;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Collider2D))]
    public class DamageComponent : MonoBehaviour, IDamageable
    {
        public IntegerVariable HP;
        public IntegerVariable startingHP;

        public void ResetHP()
        {
            HP.Value = startingHP.Value;
        }

        public void TakeDamage(int damageAmount)
        {
            HP.Value -= damageAmount;
            if (HP.Value <= 0)
                SendMessage("OnHPBelowZero");
        }
    }
}

