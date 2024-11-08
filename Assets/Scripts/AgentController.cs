using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentController : Agent
{
    [SerializeField] float move_speed = 20f;
    [SerializeField] float rotate_speed = 8f;
    Rigidbody rb;

    [SerializeField] GunController gunController;
    bool shootReady, targetHit, shotFired = false;
    int time_until_next_shot = 0;
    int min_time_until_next_shot = 30;

    [SerializeField] EnvBehavior env_behavior;

    private bool lastEpisodeWallHit = false;

    public float totalReward = 0f;

    private int episodeCounter = 0;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        episodeCounter++;
        Debug.Log("Total Reward: " + totalReward + " Episode: " + episodeCounter);

        // Spawn the agent
        if (lastEpisodeWallHit)
            env_behavior.spawnAgent();

        // Spawn the target
        env_behavior.spawnTarget();

        lastEpisodeWallHit = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);

        Vector3 targetPosition = env_behavior.target.transform.localPosition;
        sensor.AddObservation(targetPosition);

        float distanceToTarget = Vector3.Distance(targetPosition, transform.localPosition);
        sensor.AddObservation(distanceToTarget);

        sensor.AddObservation(this.transform.forward);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        shootReady = false;
        float move_rotate = actions.ContinuousActions[0];
        float move_forward = actions.ContinuousActions[1];

        bool shoot = actions.DiscreteActions[0] > 0;

        rb.MovePosition(transform.position + transform.forward * move_forward * move_speed * Time.deltaTime);
        transform.Rotate(0f, move_rotate * rotate_speed, 0f, Space.Self);

        // Vector3 targetPosition = env_behavior.target.transform.localPosition;
        // Vector3 directionToTarget = (targetPosition - transform.localPosition).normalized;
        // float alignment = Vector3.Dot(transform.forward, directionToTarget);
        // AddReward(alignment * 0.1f);

        // AddReward(-0.01f);

        // float distanceToTarget = Vector3.Distance(transform.localPosition, targetPosition);
        // float distanceReward = -distanceToTarget * 0.01f;
        // AddReward(distanceReward);

        if (shoot && !shotFired)
            shootReady = true;

        if (shootReady)
        {
            targetHit = gunController.shootGun();
            time_until_next_shot = min_time_until_next_shot;
            shotFired = true;
            if (targetHit)
            {
                float reward = 1f;
                AddReward(reward);
                totalReward += reward;

                EndEpisode();
            }
            else
            {
                float penalty = -0.2f;
                Vector3 targetPosition = env_behavior.target.transform.localPosition;
                Vector3 directionToTarget = (targetPosition - transform.localPosition).normalized;
                float alignment = Vector3.Dot(transform.forward, directionToTarget);
                AddReward((1 - alignment) * penalty);
                totalReward += penalty;

                // EndEpisode();
                Debug.Log("Total Reward: " + totalReward + " Episode: " + episodeCounter);
            }
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        continuousActions[0] = Input.GetAxis("Horizontal");
        continuousActions[1] = Input.GetAxis("Vertical");

        discreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
        // discreteActions[0] = Input.GetMouseButtonDown(0) ? 1 : 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            float penalty_wall = -0.1f;
            AddReward(penalty_wall);
            totalReward += penalty_wall;

            lastEpisodeWallHit = true;

            EndEpisode();
        }
        else if (collision.gameObject.tag == "Target")
        {
            float penalty_target = -0.05f;
            AddReward(penalty_target);
            totalReward += penalty_target;

            EndEpisode();
        }
    }

    private void FixedUpdate()
    {
        if (shotFired)
        {
            time_until_next_shot--;
            if (time_until_next_shot <= 0)
            {
                shotFired = false;
            }
        }
    }
}
