using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePoint : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Civil")) {
            Destroy(other.gameObject);
        }
    }

    public static SafePoint GetClosestSafePoint(Vector3 position) {
        SafePoint[] safePoints = FindObjectsOfType<SafePoint>();
        SafePoint closest = null;
        float closestDistance = float.MaxValue;

        foreach (SafePoint safePoint in safePoints) {
            float distance = Vector3.Distance(safePoint.transform.position, position);
            if (distance < closestDistance) {
                closest = safePoint;
                closestDistance = distance;
            }
        }

        return closest;
    }
}
