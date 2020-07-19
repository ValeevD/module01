using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class BuffApplier : MonoBehaviour
{
    private List<Buff> buffList;
    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
        buffList = new List<Buff>();
    }

    public void AddBuff(Buff buff)
    {
        buffList.Add(buff);
    }

    public bool IsDoubleDamageSuccess()
    {
        return IsBuffSuccess(BuffType.DoubleDamage);
    }

    public bool IsBlockSuccess()
    {
        return IsBuffSuccess(BuffType.Block);
    }

    private bool IsBuffSuccess(BuffType buffType)
    {
        Buff doubleDamageBuff = buffList.Find(x => x.buffType == buffType);
        return doubleDamageBuff != null && doubleDamageBuff.ChanceSuccess();
    }
}
