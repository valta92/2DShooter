using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;
    private bool isInitialized = false;
    Vector3 offset;

    public void Initialize ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
        isInitialized = true;
    }

    void FixedUpdate ()
    {
        if (!target)
            return;
        
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);

    }
}
