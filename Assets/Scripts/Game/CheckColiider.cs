using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColiider : MonoBehaviour
{
    private ObjectBase owner;
    public int damage;
    private bool canHit = false;

    [SerializeField] List<string> enermyTags = new List<string>();
    [SerializeField] List<string> itemTags = new List<string>();

    public void Init(ObjectBase owner, int damage)
    {
        this.damage = damage;
        this.owner = owner;
    }  
    /// <summary>
    /// Open damage detection
    /// </summary>
    public void StartHit()
    {
        canHit = true;
    }
    /// <summary>
    /// close damage detection
    /// </summary>
    public void StopHit()
    {
        canHit = false;
        lastAttackObjectList.Clear();
    }

    private List<GameObject> lastAttackObjectList = new List<GameObject>();

    private void OnTriggerStay(Collider other)
    {
        if (canHit)
        {
            //Debug.LogError("11111112222222");
            //current atk has not detected this game object before && the tag of this game object is in the enermy list
            if (!lastAttackObjectList.Contains(other.gameObject)&&enermyTags.Contains(other.tag))
            { 
               // Debug.LogError("1111111");
                lastAttackObjectList.Add(other.gameObject);
                //damage logic
                other.GetComponent<ObjectBase>().Hurt(damage);
            }
            return;
        }
        //detecting reterive
        if (itemTags.Contains(other.tag))
        {
            ItemType itemType = System.Enum.Parse<ItemType>(other.tag);
            if(owner.AddItem(itemType))
            {
            owner.PlayAudio(1);
            Destroy(other.gameObject);
            }
            
        }
    }
}
