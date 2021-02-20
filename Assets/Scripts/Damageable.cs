using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    public int initialLife;
    protected int currentLife;
    public E_TargetType type;


    public E_TargetType GetTType() { return type; }    
    public abstract void TakeDamage();
}
