using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;     // Priority 10
    public CinemachineVirtualCamera secondaryCamera; // Priority 8
    public CinemachineVirtualCamera gateCamera;      // Priority 6

    public float gateCameraDuration = 2f;  // Duration to keep the Gate camera active
    private bool isGateCameraActive = false;
    private bool isSecondCameraActive = false;
    void Update()
    {
        // Check if "K" is pressed to switch to the secondary camera
        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchToSecondaryCamera();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGateCameraActive)
        {
            StartCoroutine(SwitchToGateCamera());
        }
    }

    private void SwitchToSecondaryCamera()
    {
        if (!isSecondCameraActive) {
            mainCamera.Priority = 8;
            secondaryCamera.Priority = 10;
            isSecondCameraActive=true;
        }
        else
        {
            mainCamera.Priority = 10;
            secondaryCamera.Priority = 8;
        }
    }

    private IEnumerator SwitchToGateCamera()
    {

        isGateCameraActive = true;
        gateCamera.Priority = 11;
        mainCamera.Priority = 8;   
        secondaryCamera.Priority = 6;  

        // Wait for the duration
        yield return new WaitForSeconds(gateCameraDuration);

        // Return to the main camera
        gateCamera.Priority = 6;
        secondaryCamera.Priority = 8;
        mainCamera.Priority = 10;    

        isGateCameraActive = false;
    }
}
