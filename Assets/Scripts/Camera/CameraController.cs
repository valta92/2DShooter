using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;
    Vector3 offset;

    public void Initialize ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        offset = transform.position - target.position;
    }

    void FixedUpdate ()
    {
        if (!target)
            return;

        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);

    }
}
