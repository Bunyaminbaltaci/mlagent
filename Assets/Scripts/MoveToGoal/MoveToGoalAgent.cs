using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform target;
    [SerializeField] private Material winmaterial;
    [SerializeField] private Material losematerial;
    [SerializeField] private MeshRenderer floormeshRenderer;
    private float targetx, targetz, xdeger, zdeger;

    public void randomdeger()
    {
        targetx = Random.Range(-14f, 14f);
        targetz = Random.Range(-14f, 14f);
        xdeger = Random.Range(-14f, 14f);
        zdeger = Random.Range(-14f, 14f);
        if (Mathf.Abs(targetx-xdeger)<1f|| Mathf.Abs(targetz-zdeger)<1f)
        {
            randomdeger();
        }


    }


    public override void OnEpisodeBegin()
    {
        randomdeger();
        transform.localPosition = new Vector3(xdeger,0,zdeger);
        target.localPosition = new Vector3(targetx, 0, targetz);
        


    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);


    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        
      float moveX=actions.ContinuousActions[0];
      float movez=actions.ContinuousActions[1];
      float movespeed=20f;
      transform.localPosition+=
      new Vector3(moveX,0,movez)*
      Time.deltaTime*
      movespeed;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Gol>(out Gol gol))
        {
            SetReward(1f);
            floormeshRenderer.material = winmaterial;
            EndEpisode();

        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
           
           SetReward(-1f);
            floormeshRenderer.material = losematerial;

            EndEpisode();

        }
        

    }
}
