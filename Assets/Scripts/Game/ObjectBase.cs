using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// All objects basic class
/// </summary>
public class ObjectBase : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClips;
    public GameObject lootObject; //dropped items

    //detect if dead if the value of hp modified, and update the value
    public float Hp { get => hp; set {
            hp = value;
            if (hp <= 0)
            {
                hp = 0;
               // Destroy(gameObject);
                Dead();
            }
            OnHpUpdate();//auto call hp update
        } 
    }

    //play audio
    public void PlayAudio(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }

    protected virtual void OnHpUpdate()
    {

    }
    protected virtual void Dead()
    {
        //Debug.LogError("111111111111111111");
        if (lootObject != null)
        {
            //Debug.LogError("22222222222222222");
            Instantiate(lootObject,
                transform.position+new Vector3(Random.Range(-0.5f,0.5f), Random.Range(1f,1.2f), Random.Range(-0.5f, 0.5f)),
                Quaternion.identity,
                null);

        }
    }

    public virtual void Hurt(int damage)
    {
        Hp -= damage;
    }
    public virtual bool AddItem(ItemType itemType)
    {
        return false;
    }

   


   
}
