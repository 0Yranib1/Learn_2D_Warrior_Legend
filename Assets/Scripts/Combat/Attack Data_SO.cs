using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack",menuName = "Attack/Attack Data")]
public class AttackData_SO : ScriptableObject
{
    public int damage;
    public float attackRange;
    public float attackRate;
    
    

}
