using System.Collections;
using UnityEngine;

public class ComboAnimationTriggerManager : MonoBehaviour
{
    public Transform playerTransform;
    public float chargeDistance = 2.0f; // Distance to charge forward during an attack
    public float chargeSpeed = 5.0f; // Speed of the charge
    public float stoppingDistance = 2.0f; // Distance to stop from the target

    public Transform target;
    public float range = 5.0f;

    public IEnumerator Charge()
    {
        float chargeTime = chargeDistance / chargeSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < chargeTime)
        {
            LookAtTargetXZ(target);

            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= stoppingDistance)
            {
                break; // Stop charging if within stopping distance
            }

            float step = chargeSpeed * Time.deltaTime;

            if (target == null)
            {
                playerTransform.Translate(Vector3.forward * chargeSpeed * Time.deltaTime);
            }
            else
            {
                playerTransform.position = Vector3.MoveTowards(playerTransform.position, target.position, step);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public void LookAtTargetXZ(Transform target)
    {
        if (target == null) return;

        // Create a new vector with the target's X and Z coordinates but the GameObject's Y coordinate
        Vector3 targetPositionXZ = new Vector3(target.position.x, playerTransform.position.y, target.position.z);

        // Rotate the GameObject to look at the new target position
        playerTransform.LookAt(targetPositionXZ);
    }

    public void DisableMovement()
    {
        InputManager.Instance.DisableMovement();
    }
    public void EnableMovement()
    {
        InputManager.Instance.EnableMovement();
    }

    public bool IsTargetWithinRange(Transform target, float range)
    {
        // Calculate the distance between this object and the target
        float distance = Vector3.Distance(playerTransform.position, target.position);

        // Return true if the target is within range, false otherwise
        return distance <= range;
    }
}
