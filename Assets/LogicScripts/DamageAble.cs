using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageAble : MonoBehaviour
{
    public int HP;
    public int MaxHP;
    public int DamageFromLastHit;
    public UnityEvent OnDamageTaken = new UnityEvent();
    public UnityEvent OnHealthRestored = new UnityEvent();

    void Start()
    {
        HP = MaxHP;
    }

    public void TakeDamage(int amount)
    {
        DamageFromLastHit = amount;

        if (HP != 0)
        {
            HP -= amount;
        }

        if (HP < 0)
        {
            HP = 0;
        }

        OnDamageTaken.Invoke();
    }

    public void HealDamage(int amount)
    {
        if (HP != MaxHP)
        {
            HP += amount;
        }

        if (HP > MaxHP)
        {
            HP = MaxHP;
        }

        OnHealthRestored.Invoke();
    }
}
