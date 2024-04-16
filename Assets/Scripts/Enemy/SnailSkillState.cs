using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailSkillState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("Walk",false);
        currentEnemy.anim.SetBool("Hide",true);
        currentEnemy.anim.SetTrigger("Skill");
        currentEnemy.GetComponent<Character>().invulnerableCounter = currentEnemy.lostTime;
        currentEnemy.GetComponent<Character>().invulnerable = true;
    }

    public override void LogicUpdate()
    {
        if (currentEnemy.lostTimeCounter <= 0)
        {
            currentEnemy.SwitchState(Enums.NPCState.Patrol);
        }
        currentEnemy.GetComponent<Character>().invulnerableCounter = currentEnemy.lostTimeCounter;
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("Hide",false);
        currentEnemy.GetComponent<Character>().invulnerable = false;
    }
}
