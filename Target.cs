using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    private float health = 10f;

    public void TakeDamage(float damage)
    {
       health -= damage;
        if (health <= 0)
            SoundManager.PlaySound(SoundType.TargetDestroyed, 0.02f);
        Destroy(gameObject);
            
    }

}
