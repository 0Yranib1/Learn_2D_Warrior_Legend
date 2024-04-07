using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO templateData;
    
    public CharacterData_SO characterData;
    public AttackData_SO attackData;
    private float invulnerableCounter;
    private bool invulnerable;
    
    public UnityEvent<Transform> OnTakeDamage;
    private void Awake()
    {
        if (templateData != null)
            characterData = Instantiate(templateData);
    }
    
    private void Start()
    {
        characterData.currentHealth = characterData.maxHealth;
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

    public void TakeDamage(AttackData_SO attacker)
    {
        if (invulnerable)
        {
            return;
        }

        if (characterData.currentHealth - attacker.damage > 0)
        {
            Debug.Log(this.name+"收到伤害"+attacker.damage);
            characterData.currentHealth -= attacker.damage;
            TriggerInvulnerable();
            //受伤
        }
        else
        {
            characterData.currentHealth = 0;
            //死亡
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GameObject().CompareTag("Player"))
        {
            other.GetComponent<CharacterStats>()?.TakeDamage(this.attackData);
        }
    }
    
    //无敌时间计算
    private void TriggerInvulnerable()
    {
        if (!invulnerable)
        {
            invulnerable = true;
            invulnerableCounter = characterData.invulnerableDuration;
        }
    }

}
