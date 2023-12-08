using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CamAdjust : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;

    public float zoomSpd = 5;
    public float startFOV;
    public float zoomedOutFOV;

    public float offSetStart;
    public float offSetEnd;

    private float targetFOV;
    private float targetOffSet;
    private bool isZoomOut;

    private void Start()
    {
        targetFOV = startFOV;
        targetOffSet = offSetStart;
    }

    private void Update()
    {
        isZoomOut = Input.GetKey(KeyCode.W);

        targetFOV = isZoomOut ? zoomedOutFOV : startFOV;
        targetOffSet = isZoomOut ? offSetEnd : offSetStart;


        float newFOV = Mathf.Lerp(freeLookCam.m_Orbits[1].m_Radius, targetFOV, Time.deltaTime * zoomSpd);
        float newOffset = Mathf.Lerp(freeLookCam.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset.x,
            targetOffSet, Time.deltaTime * zoomSpd);

        CinemachineComposer composer = freeLookCam.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
        composer.m_TrackedObjectOffset.x = newOffset;
        freeLookCam.m_Orbits[1].m_Radius = newFOV;
    }
}
