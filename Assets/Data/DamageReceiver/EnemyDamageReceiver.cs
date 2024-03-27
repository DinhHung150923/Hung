using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class EnemyDamageReceiver : DamageReceiver
{
    protected override void Ondead()
    {
        this.characterCtrl.ModelCtrl.Animator.SetBool("IsDying", true);
        Invoke(nameof(this.DespawnEnemy), this.timeDieDelay);
    }
    protected virtual void DespawnEnemy()
    {
        EnemySpawner.Instance.DeSpawn(transform.parent);
    }
}