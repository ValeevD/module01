using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State
    {
        Idle,
        RunningToEnemy,
        RunningFromEnemy,
        BeginAttack,
        Attack,
        BeginShoot,
        Shoot,
        BeginPunch,
        Punch,
        BeginDying,
        Dead,
    }

    public enum Weapon
    {
        Pistol,
        Bat,
        Fist,
    }

    public Weapon weapon;
    public float runSpeed;
    public float distanceFromEnemy;
    public Transform target;
    public TargetIndicator targetIndicator;
    public float damage;
    public SoundPlay attackSoundPlay;
    public SoundPlay gotDamageSoundPlay;
    public SoundPlay deathSoundPlay;
    
    State state;
    Animator animator;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Health health;

    HealthIndicator healthIndicator;

    BuffApplier buffApplier;

    BloodEffectBehaviour bloodEffect;
    
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        state = State.Idle;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        health = GetComponent<Health>();
        targetIndicator = GetComponentInChildren<TargetIndicator>();
        healthIndicator = GetComponentInChildren<HealthIndicator>();
        buffApplier = GetComponent<BuffApplier>();
        bloodEffect = GetComponent<BloodEffectBehaviour>();
    }


    public bool IsIdle()
    {
        return state == State.Idle;
    }

    public bool IsDead()
    {
        return state == State.BeginDying || state == State.Dead;
    }

    public void SetState(State newState)
    {
        if (IsDead())
            return;

        state = newState;
    }

    public void DoDamage(Character instance)
    {
        if (IsDead())
            return;

        float damageToApply = instance.damage;

        if(buffApplier != null && buffApplier.IsBlockSuccess()){
            damageToApply = 0.0f;
            Debug.Log("" + name + " blocked damage!");
        }

        if(instance.buffApplier != null && instance.buffApplier.IsDoubleDamageSuccess()){
            damageToApply *= 2.0f;
            Debug.Log("" + instance.name + " deals 2xDamage!");
        }
        

        //FIX
        //Вроде, нужно сделать как-то универсальнее. Может через switch..
        //Сделать какое нибудь сообщение о срабатывании бафов

        health.ApplyDamage(damageToApply);
        if (health.current <= 0.0f)
        {
            state = State.BeginDying;
            StartCoroutine(OnShootEnd(instance, deathSoundPlay));
            //deathSoundPlay.Play();
        }
        else if(damageToApply > 0.0f)
        {
            StartCoroutine(OnShootEnd(instance, gotDamageSoundPlay));
        }
        

    }

    [ContextMenu("Attack")]
    public void AttackEnemy()
    {
        if (IsDead())
            return;

        Character targetCharacter = target.GetComponent<Character>();
        if (targetCharacter.IsDead())
            return;

        switch (weapon) {
            case Weapon.Bat:
                state = State.RunningToEnemy;
                break;

            case Weapon.Fist:
                state = State.RunningToEnemy;
                break;

            case Weapon.Pistol:
                state = State.BeginShoot;
                
                break;
        }
    }

    bool RunTowards(Vector3 targetPosition, float distanceFromTarget)
    {
        Vector3 distance = targetPosition - transform.position;
        if (distance.magnitude < 0.00001f) {
            transform.position = targetPosition;
            return true;
        }

        Vector3 direction = distance.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        targetPosition -= direction * distanceFromTarget;
        distance = (targetPosition - transform.position);

        Vector3 step = direction * runSpeed;
        step.y = 0;
        if (step.magnitude < distance.magnitude) {
            transform.position += step;
            return false;
        }

        transform.position = targetPosition;
        return true;
    }

    void FixedUpdate()
    {
        switch (state) {
            case State.Idle:
                animator.SetFloat("Speed", 0.0f);
                transform.rotation = originalRotation;
                break;

            case State.RunningToEnemy:
                animator.SetFloat("Speed", runSpeed);
                if (RunTowards(target.position, distanceFromEnemy)) {
                    switch (weapon) {
                        case Weapon.Bat:
                            state = State.BeginAttack;
                            break;

                        case Weapon.Fist:
                            state = State.BeginPunch;
                            break;
                    }
                }
                break;

            case State.BeginAttack:
                animator.SetTrigger("MeleeAttack");
                StartCoroutine(nameof(OnCompleteAttackAnimation));
                state = State.Attack;
                break;

            case State.Attack:
                break;

            case State.BeginShoot:
                animator.SetTrigger("Shoot");
                // var shootEffect = target.GetComponent<BloodEffectBehaviour>();
                // shootEffect.PLayEffect();
                StartCoroutine(nameof(OnCompleteAttackAnimation));
                state = State.Shoot;
                break;

            case State.Shoot:
                //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                break;

            case State.BeginPunch:
                animator.SetTrigger("Punch");
                StartCoroutine(nameof(OnCompleteAttackAnimation));
                state = State.Punch;
                break;

            case State.Punch:
                break;

            case State.RunningFromEnemy:
                animator.SetFloat("Speed", runSpeed);
                if (RunTowards(originalPosition, 0.0f))
                    state = State.Idle;
                break;

            case State.BeginDying:
                animator.SetTrigger("Death");
                
                state = State.Dead;
                break;

            case State.Dead:
                healthIndicator.gameObject.SetActive(false);
                break;
        }

        
    }

    IEnumerator OnCompleteAttackAnimation()
    {
        var isPistol = weapon == Weapon.Pistol;

        if(isPistol)
            yield return new WaitForSeconds(0.2f);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);        
        
        if(isPistol)
        {
            var fireEffect = GetComponent<FireEffectBehaviour>();
            fireEffect.PlayFireEffect();
        }

        attackSoundPlay.Play();
    }

    IEnumerator OnShootEnd(Character instance, SoundPlay soundEffect)
    {
        yield return new WaitUntil(() => instance.state != State.Shoot);
        soundEffect.Play();
        bloodEffect.PLayEffect();
    }

}
