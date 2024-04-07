using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data",menuName = "Character Stats/Data")]
public class CharacterData_SO : ScriptableObject
{
    // Start is called before the first frame update
    public float maxHealth;
    public float currentHealth;
    public float invulnerableDuration;
}
