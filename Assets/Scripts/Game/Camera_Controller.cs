using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float transitionSpeed = 2;
   
    // player move first, then camera follows
    public void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPos = target.position + offset;
            //continous convert from current position to target position
            transform.position = Vector3.Lerp(transform.position, targetPos, transitionSpeed * Time.deltaTime);
        }
    }
}
