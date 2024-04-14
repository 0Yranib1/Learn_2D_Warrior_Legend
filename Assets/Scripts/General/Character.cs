using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本属性")] 
    public float maxHealth;
    public float currentHealth;
    [Header("无敌时间")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    private bool invulnerable;
        
    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if (invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
    }

    public void TakeDamage(Attack attacker)
    {
        if (invulnerable)
        {
            return;
        }

        if (currentHealth - attacker.damage > 0)
        {
            Debug.Log("收到伤害"+attacker.damage);
            currentHealth -= attacker.damage;
            TriggerInvulnerable();
            //受伤
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            //死亡
            OnDie?.Invoke();
        }
    }

    //无敌时间计算
    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
}
