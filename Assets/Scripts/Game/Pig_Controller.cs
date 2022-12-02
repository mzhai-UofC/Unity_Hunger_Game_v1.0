using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    idle,
    move,
    purse,
    attack,
    hurt,
    die
}
public class Pig_Controller :   ObjectBase
{
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] CheckColiider checkCollider;

    public float maxX = 4.74f;
    public float minX = -5.62f;
    public float maxZ = 5.92f;
    public float minZ = -6.33f;

    private EnemyState enemyState;
    private Vector3 targetPos;

    public EnemyState EnemyState
    {
        get => enemyState;
        set
        {
            enemyState = value;

            switch(enemyState)
            {
                case EnemyState.idle:
                    //display animation
                    //close nav
                    //rest
                    animator.CrossFadeInFixedTime("idle", 0.25f);
                    navMeshAgent.enabled = false;
                    Invoke(nameof(GoMove), Random.Range(3f, 10f));
                    break;

                case EnemyState.move:
                    //display anim
                    //open nav
                    //get haunt point
                    //move to certain position
                    animator.CrossFadeInFixedTime("move", 0.25f);
                    navMeshAgent.enabled = true;
                    targetPos = GetTargetPos();
                    navMeshAgent.SetDestination(targetPos);
                    break;

                case EnemyState.purse:
                    animator.CrossFadeInFixedTime("move", 0.25f);
                    navMeshAgent.enabled = true;
                    break;

                case EnemyState.attack:
                    animator.CrossFadeInFixedTime("attack", 0.25f);
                    navMeshAgent.enabled = false;
                    transform.LookAt(PlayerController.Instance.transform.position);
                    break;

                case EnemyState.hurt:
                    PlayAudio(0);
                    navMeshAgent.enabled = false;
                    animator.CrossFadeInFixedTime("hurt", 0.25f);
                    break;

                case EnemyState.die:
                    PlayAudio(0);
                    navMeshAgent.enabled = false;
                    animator.CrossFadeInFixedTime("die", 0.25f);
                    Destroy(gameObject);
                    break;
            }
        }
    }

    private void Start()
    {
        Hp = 100;
        checkCollider.Init(this,10);
        EnemyState = EnemyState.idle;
    }

    private void Update()
    {
        StateOnUpdate();
    }

    private void StateOnUpdate()
    {
        switch (enemyState)
        {
            case EnemyState.idle:
                break;
            case EnemyState.move:
                if (Vector3.Distance(transform.position, targetPos) < 1.5f)
                {
                    EnemyState = EnemyState.idle;
                }
                break;
            case EnemyState.purse:
                //if distance<1 between pig and player,attack
                if(Vector3.Distance (transform.position, PlayerController.Instance.transform.position) < 1)
                {
                    EnemyState = EnemyState.attack;
                }
                else
                {
                    //keep persuing
                    navMeshAgent.SetDestination(PlayerController.Instance.transform.position);
                }
                break;
            case EnemyState.hurt:
                break;
            case EnemyState.attack:
                break;
            case EnemyState.die:
                break;
        }
    }

    private void GoMove()
    {
        if (EnemyState != EnemyState.die)
        {
            EnemyState = EnemyState.move;
        }
    }
    //get a random point in a area
    private Vector3 GetTargetPos()
    {
        return new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
    }
    public override void Hurt(int damge)
    {
        if (EnemyState == EnemyState.die) return;
        base.Hurt(damge);
        CancelInvoke(nameof(GoMove));
        if (Hp > 0) EnemyState = EnemyState.hurt;
    }

    protected override void Dead()
    {
        base.Dead();
        EnemyState = EnemyState.die;
    }
    #region
    private void StartHit()
    {
        checkCollider.StartHit();
    }

    private  void StopHit()
    {
        checkCollider.StopHit();
    }

    private void StopAttack()
    {
        if (EnemyState != EnemyState.die)
            { EnemyState = EnemyState.purse; }
    }

    private void HurtOver()
    {
        if (EnemyState != EnemyState.die)
        { EnemyState = EnemyState.purse; }
    }
    /*private void Die()
    {
        Destroy(gameObject);
    }*/
    #endregion
}
