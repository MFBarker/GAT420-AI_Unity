using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAutonomousAgnet : AIAgent
{
   [SerializeField] AIPerception seekPerception = null;
   [SerializeField] AIPerception fleePerception = null;
   [SerializeField] AIPerception flockPerception = null;
   [SerializeField] AIPerception obstaclePerception = null;

    private void Update()
    {
        //seek
        if (seekPerception != null)
        {
            var gameObjects = seekPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Seek(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }
        //flee
        if (fleePerception != null)
        {
            var gameObjects = fleePerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Flee(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }

        //flock
        if (flockPerception != null) 
        {
            var gameObjects = flockPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                movement.ApplyForce(Cohesion(gameObjects));
                movement.ApplyForce(Separation(gameObjects, 3));
                movement.ApplyForce(Alignment(gameObjects));
            }
        }

        //obstacle avoudance
        if (obstaclePerception != null)
        {
            if (((AISpherecastPerception)obstaclePerception).CheckDirection(Vector3.forward))
            {
                Vector3 open = Vector3.zero;

                if (((AISpherecastPerception)obstaclePerception).GetOpenDirection(ref open))
                { 
                    movement.ApplyForce(GetSteeringForce(open) * 5);
                }
            }

        }

        //cancel y movement
        Vector3 acceleration = movement.Acceleration;
        acceleration.y = 0;
        movement.Acceleration = acceleration;

        //wrap position in world
        transform.position = Utilities.Wrap(transform.position, new Vector3(-10,-10, -10), new Vector3(10,10,10));
    }

    private Vector3 Seek(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 Flee(GameObject target)
    {
        Vector3 direction = transform.position - target.transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    /* Flocking */
    private Vector3 Cohesion(GameObject[] neighbors)
    {
        Vector3 positions = Vector3.zero;
        foreach (var neighbor in neighbors) 
        {
            positions += neighbor.transform.position;
        }

        Vector3 center = positions / neighbors.Length;
        Vector3 direction = center - transform.position;

        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 Separation(GameObject[] neighbors, float radius)
    {
        Vector3 separation = Vector3.zero;
        foreach (var neighbor in neighbors)
        {
            // get direction vector away from neighbor
            Vector3 direction = (transform.position - neighbor.transform.position);
            // check if within separation radius
            if (direction.magnitude < radius)
            {
                // scale separation vector inversely proportional to the direction distance
                // closer the distance the stronger the separation
                separation += direction / direction.sqrMagnitude;
            }
        }

        Vector3 force = GetSteeringForce(separation);

        return force;
    }

    private Vector3 Alignment(GameObject[] neighbors)
    { 
        Vector3 velocities = Vector3.zero;
        foreach (var neighbor in neighbors)
        {
            velocities += neighbor.GetComponent<AIAgent>().movement.Velocity;
        }

        Vector3 averageVelocity = velocities / neighbors.Length;

        Vector3 force = GetSteeringForce(averageVelocity);
        return force;
    }

    //
    public Vector3 GetSteeringForce(Vector3 direction)
    {
        Vector3 desired = direction.normalized * movement.maxSpeed;
        Vector3 steer = desired - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, movement.maxForce);

        return force;
    }
}
