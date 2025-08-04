using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Vector2 currentPosition;
    Vector2 goalPosition;
    [SerializeField] GameObject currentGoal;
    [SerializeField] float speedModifier;
    float moveAmount;
    float distanceToGoal;

    [SerializeField] GameObject startGoal;
    [SerializeField] GameObject endGoal;
    bool moveToGoal = true;
    [SerializeField] float waitTime;

    void Update()
    {
        currentPosition = transform.position;
        goalPosition = currentGoal.transform.position;
        moveAmount = speedModifier * Time.deltaTime;

        distanceToGoal = Vector2.Distance(goalPosition, currentPosition);

        if(distanceToGoal > moveAmount && moveToGoal == true) 
        {
            transform.position = Vector2.MoveTowards(currentPosition, goalPosition, moveAmount);
        }

        else if (distanceToGoal < moveAmount && moveToGoal == true)
        {
            if (currentGoal == startGoal)
            {
                currentGoal = endGoal;
            }
            else
                    {
                currentGoal = startGoal;
                
                     }
            StartCoroutine(TimeDelay());
        }
        
    }

    private IEnumerator TimeDelay()
    {
        moveToGoal = false;
        yield return new WaitForSeconds(waitTime);
        moveToGoal = true;
    }



}
