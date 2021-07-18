using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

using Panda;
public class EnemyPandaTest : MonoBehaviour
{
    public float speed = 10.0f;
    public float D = 0f;
    public Animator anim;
    public GameObject HeadMark;
    public GameObject LeftBodyMark;
    public GameObject RightBodyMark;
    public GameObject TailMark;
 
    
    void FaceTowardPlayer()
    {
        GameObject player = null;
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            Debug.Log("No Player !!");
            
        }
        else
        {
            Debug.Log("Have Player !!");
            this.gameObject.transform.LookAt(player.transform);
            
        }
    }
    [Task]
    void Roar()
    {
        Debug.Log("Fooouu  Fouuu ffoofofo fappp !!");
        Task.current.Succeed();

    }
    [Task]
    void PlayerInRanged()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(Vector3.Distance(player.transform.position,this.gameObject.transform.position) <= D)
        {
            Debug.Log("Player In Ranged");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    [Task]
    void PlayerNotInRanged()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) > D)
        {
            Debug.Log("Player Not In Ranged");
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    [Task]
    void MoveToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //FaceTowardPlayer();
        Vector3 destination = player.transform.position;
        Vector3 delta = (destination - transform.position);
        Vector3 velocity = speed * delta.normalized;

        

        Vector3 newDelta = (destination - transform.position);
        float d = newDelta.magnitude;

        if (Task.isInspected)
            Task.current.debugInfo = string.Format("d={0:0.000}", d);

        if (d<= D)
        {
            Task.current.Succeed();
            //d = 0.0f;
            //Task.current.debugInfo = "d=0.000";
        }
        else
        {
            //transform.position = transform.position + velocity * Time.deltaTime;
        }

    }
    GameObject NearestPoint()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float HeadDis = Vector3.Distance(player.transform.position, HeadMark.transform.position);
        float LeftDis = Vector3.Distance(player.transform.position, LeftBodyMark.transform.position);
        float RightDis = Vector3.Distance(player.transform.position, RightBodyMark.transform.position);
        float TailDis = Vector3.Distance(player.transform.position, TailMark.transform.position);
        float MinDis = Mathf.Min(Mathf.Min(HeadDis,RightDis), Mathf.Min(LeftDis,TailDis));
        if(MinDis == HeadDis)
        {
            return HeadMark;
        }
        else if(MinDis == LeftDis)
        {
            return LeftBodyMark;
        }
        else if (MinDis == RightDis)
        {
            return RightBodyMark;
        }
        else
        {
            return TailMark;
        }
    }
    [Task]
    void ActionNearestPoint()
    {
        GameObject NPoint = NearestPoint();
        if (NPoint.Equals(HeadMark))
        {
            anim.SetTrigger("Front");

            
        }
        else if (NPoint.Equals(LeftBodyMark))
        {
            anim.SetTrigger("Left");
        
        }
        else if (NPoint.Equals(RightBodyMark))
        {
            anim.SetTrigger("Right");
         
        }
        else
        {
            anim.SetTrigger("Back");

        }
        Task.current.Succeed();
    }
    
    
}
