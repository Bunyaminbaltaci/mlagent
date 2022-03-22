using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PushAgent : Agent
{
    private Rigidbody agentrigidbody;


    private void Awake()
    {
        agentrigidbody=GetComponent<Rigidbody>();
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        int moveX = actions.DiscreteActions[0];// 0= durur 1= saða 2=sola

        int moveZ = actions.DiscreteActions[1];//0=durur 1=ileri 2=geri
        Vector3 addforceVec = Vector3.zero;

        switch (moveX)
        {
            case 0:addforceVec.x = 0f;break;  
            case 1:addforceVec.x =-1f;break;  
            case 2:addforceVec.x = 1f;break;  
        }
        switch (moveZ)
        {
            case 0: addforceVec.z = 0f; break;
            case 1: addforceVec.z = -1f; break;
            case 2: addforceVec.z = 1f; break;
        }
        float movespeed = 20f;
        agentrigidbody.velocity = addforceVec * movespeed + new Vector3(0,agentrigidbody.velocity.y,0);
        bool isUseButtonDown = actions.DiscreteActions[2]==1;
        if (isUseButtonDown)
        {
            Collider[] colliderarray = Physics.OverlapBox(transform.position,Vector3.one*2.5f);
            foreach (Collider col in colliderarray)
            {
                if (col.TryGetComponent<FoodButton>(out FoodButton foodButton))
                {
                    if (foodButton.CanBeUse())
                    {
                        foodButton.UseButton();
                        AddReward(1f);
                    }
                }
               

            }
        }
        
        
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {   
        ActionSegment<int> discreteAction = actionsOut.DiscreteActions;
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1: discreteAction[0] = 1;break;
            case 0: discreteAction[0] = 0;break;
            case 1: discreteAction[0] = 2;break;
        }
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case -1: discreteAction[1] = 1; break;
            case 0: discreteAction[1] = 0; break;
            case 1: discreteAction[1] = 2; break;
        }
        discreteAction[2] = Input.GetKey(KeyCode.E) ? 1 : 0;
    }
}
