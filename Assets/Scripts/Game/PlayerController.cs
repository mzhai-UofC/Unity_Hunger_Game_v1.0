using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : ObjectBase
{
    [SerializeField] Animator animator;
    [SerializeField] CharacterController characterController;
    [SerializeField] float moveSpeed = 1;

    Quaternion targetDirQuaternion;
    public static PlayerController Instance;
    private bool isAttacking=false;
    public float hungry;//other object cannot use, so put the variable in playercontroller

    [SerializeField] Image hpImage;
    [SerializeField] Image HungryImage;
    [SerializeField] CheckColiider checkColiider;
    private bool isHurting = false;
    public bool isDraging = false;

    private void Awake()
    {
        Instance = this;
        checkColiider.Init(this, 30);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHungry();
        if (!isAttacking)
        {
            Move();
            Dash();
            Attack();
        }
        else
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetDirQuaternion, Time.deltaTime * 10);
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0)
        {
            animator.SetBool("Walk", false);
        }

        else
        {
            animator.SetBool("Walk", true);

            //get direction and turn
            targetDirQuaternion = Quaternion.LookRotation(new Vector3(h, 0, v));
            transform.localRotation = Quaternion.Slerp(transform.localRotation,targetDirQuaternion, Time.deltaTime*10f); 
            //move
            characterController.SimpleMove(new Vector3(h, 0, v).normalized * moveSpeed); 
            
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            if (isDraging||UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 100, LayerMask.GetMask("Ground")))
            {
                //detected to be touch the ground objects
                animator.SetTrigger("Attack");
                //set to attack status
                isAttacking = true;
                //turning to the target
                targetDirQuaternion = Quaternion.LookRotation(hitInfo.point - transform.position);
            }
        }
    }
    private void Dash()
    {
        if (Input.GetMouseButton(1))
        {
            if (hungry > 0)
            {
                //detected to be touch the ground objects
                animator.SetTrigger("Dash");
                hungry -= 1;
            }
        }
        
    }
    private void UpdateHungry()
    {
        hungry -= Time.deltaTime * 3;
        if (hungry <= 0)
        {
            hungry = 0;
            Hp -= Time.deltaTime / 2;
        }
        HungryImage.fillAmount = hungry / 100;
    }
    protected override void OnHpUpdate()
    {
        //update hp value/ui
        hpImage.fillAmount = Hp / 100;
    }

    public override void Hurt(int damage)
    {
        base.Hurt(damage);
        animator.SetTrigger("Hurt");
        PlayAudio(2);
        isHurting = true;
    }

    public override bool AddItem(ItemType itemType)
    {
        return BagPanel.Instance.AddItem(itemType);
    }

    public bool UseItem(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.meat:
                Hp += 10;
                hungry += 30;
                return true;
            case ItemType.CookedMeat:
                Hp += 10;
                hungry += 40;
                return true;
            case ItemType.wood:
                Hp -= 30;
                hungry += 20;
                return true;
            case ItemType.berry:
                Hp += 30;
                hungry += 20;
                return true;
        }
        return false;
    }

    #region animation event
    private void StartHit()
    {
        //play audio
        PlayAudio(0);
        //detect attacking
        checkColiider.StartHit();
    }

    private void StopHit()
    {
        //stop detect attacking
        isAttacking = false;
        checkColiider.StopHit();
    }

    private void HurtOver()
    {
        isHurting = false;
    }
    
    #endregion
}
