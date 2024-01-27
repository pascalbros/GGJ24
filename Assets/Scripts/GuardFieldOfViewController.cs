using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardFieldOfViewController: MonoBehaviour {

    public float viewRadius = 10f;
    [Range(0, 360)]
    public float viewAngle = 90f;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public Vector3 offset;

    void Update() {
        DrawFieldOfView();
    }

    void DrawFieldOfView() {
        int rayCount = 100;
        float angleIncrement = viewAngle / rayCount;
        var position = transform.position + offset;

        for (int i = 0; i <= rayCount; i++) {
            float angle = transform.eulerAngles.y - viewAngle / 2 + angleIncrement * i;
            Vector3 dir = Quaternion.Euler(0f, angle, 0f) * transform.forward;

            Ray ray = new(position, dir);

            if (Physics.Raycast(ray, out RaycastHit hit, viewRadius, obstacleMask)) {
                Debug.DrawLine(position, hit.point, Color.green);
            } else if (Physics.Raycast(ray, out hit, viewRadius, targetMask)) {
                Debug.DrawLine(position, hit.point, Color.red);
            } else {
                Debug.DrawLine(position, position + dir * viewRadius, Color.green);
            }
        }
    }


}
