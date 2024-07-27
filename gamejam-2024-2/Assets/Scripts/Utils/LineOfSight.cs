using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight {
    public static Transform[] Calculate(Transform origin, float angle, float maxDistance, LayerMask mask) {
        List<Transform> visibleTargets = new List<Transform>();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(origin.position, maxDistance, mask);

        Debug.DrawLine(origin.position, origin.position + origin.forward * maxDistance, Color.red);
        Debug.DrawLine(origin.position, origin.position + Quaternion.Euler(0, angle / 2, 0) * origin.forward * maxDistance, Color.red);
        Debug.DrawLine(origin.position, origin.position + Quaternion.Euler(0, -angle / 2, 0) * origin.forward * maxDistance, Color.red);

        for (int i = 0; i < targetsInViewRadius.Length; i++) {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - origin.position).normalized;
            Vector3 dirToTargetNoY = new Vector3(dirToTarget.x, 0, dirToTarget.z);
            Vector3 originForwardNoY = new Vector3(origin.forward.x, 0, origin.forward.z);



            if (Vector3.Angle(originForwardNoY, dirToTargetNoY) < angle / 2) {
                float dstToTarget = Vector3.Distance(origin.position, target.position);
                if (Physics.Raycast(origin.position, dirToTarget, dstToTarget)) {
                    visibleTargets.Add(target);
                }
            }
        }

        return visibleTargets.ToArray();
    }
}
