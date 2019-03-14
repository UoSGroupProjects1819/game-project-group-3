using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    //camera stuff
    public Camera cam;
    public Vector3 offset;
    private Vector3 smoothSpeed;
    public float camSmoothTime;
    public float camRotSpeed;


    //active players in selection
    public List<Transform> targets;

    private void Update()
    {
        CalculateCameraOffset();
    }

    void LateUpdate()
    {
        CameraLookAtActivePlayers();
    }

    Vector3 GetCentrePoint()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    void CameraLookAtActivePlayers()
    {
        Vector3 centerPoint = GetCentrePoint();

        //camera position
        Vector3 nextPos = centerPoint + offset;
        cam.transform.position = Vector3.SmoothDamp(transform.position, nextPos, ref smoothSpeed, camSmoothTime);

        //camera look rotation
        Vector3 lookDirection = centerPoint - cam.transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, camRotSpeed * Time.deltaTime);


        //cam.transform.LookAt(centerPoint);
    }

    void CalculateCameraOffset()
    {
        switch (targets.Count)
        {
            case 2:
                offset.z = -2.5f;
                break;
            case 3:
                offset.z = -3.5f;
                break;
            case 4:
                offset.z = -4.5f;
                break;
        }
    }
}
