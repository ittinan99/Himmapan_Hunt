using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Panda;
public class EnemyPandaTest : MonoBehaviour
{
    public float speed = 10.0f;
    [Task]
    void FaceTowardPlayer()
    {
        GameObject player = null;
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            Debug.Log("No Player !!");
            Task.current.Fail();
        }
        else
        {
            Debug.Log("Have Player !!");
            this.gameObject.transform.LookAt(player.transform);
            Task.current.Succeed();
        }
    }
    [Task]
    void MoveToPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 destination = player.transform.position;
        Vector3 delta = (destination - transform.position);
        Vector3 velocity = speed * delta.normalized;

        transform.position = transform.position + velocity * Time.deltaTime;

        Vector3 newDelta = (destination - transform.position);
        float d = newDelta.magnitude;

        if (Task.isInspected)
            Task.current.debugInfo = string.Format("d={0:0.000}", d);

        if (Vector3.Dot(delta, newDelta) <= 0.0f || d < 5f)
        {
            transform.position = transform.position;
            Task.current.Succeed();
            d = 0.0f;
            Task.current.debugInfo = "d=0.000";
        }

    }
    
}
