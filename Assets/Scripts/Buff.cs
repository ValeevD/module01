using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffApplier))]
public class Buff : MonoBehaviour
{
    public BuffType buffType;
    public float chance;

    private BuffApplier buffApplier;

    private void Awake()
    {
        buffApplier = GetComponent<BuffApplier>();    
    }

    private void Start()
    {
        buffApplier.AddBuff(this);
    }

    public bool ChanceSuccess()
    {
        return Random.Range(0, 100) <= chance;
    }
}

public enum BuffType{
    DoubleDamage,
    Block,
}