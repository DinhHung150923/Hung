using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHeroStates : ChangeCharStates
{
    [SerializeField] protected float timer = 0;
    [SerializeField] protected float timeDamage = 1.5f;
    [SerializeField] protected Transform CurrentObj;
    [SerializeField] protected bool isAttacking = false;
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.CompareTag("Enemy"))
        {
            this.characterCtrl.ModelCtrl.Animator.SetBool("IsAttacking", true);
            this.characterCtrl.Movement.gameObject.SetActive(false);
            this.CurrentObj = other.transform.parent;
            this.isAttacking = true;
        }
    }
    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.transform.parent.CompareTag("Enemy"))
        {
            this.AttackCoolDown(other.transform);
        }
    }
    protected virtual void AttackCoolDown(Transform obj)
    {
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.timeDamage) return;
        this.timer = 0;

        this.characterCtrl.DamageSender.SendObj(obj);
    }
    protected virtual void FixedUpdate()
    {
        this.CheckIsAttacking();
    }
    protected virtual void CheckIsAttacking()
    {
        if (this.isAttacking == true)
        {
            if (this.CurrentObj.gameObject.activeSelf) return;
            this.ReturnToMove();
        }
    }
    protected virtual void ReturnToMove()
    {
        this.characterCtrl.ModelCtrl.Animator.SetBool("IsAttacking", false);
        this.characterCtrl.Movement.gameObject.SetActive(true);
        this.isAttacking = false;
        this.CurrentObj = null;
    }
}
