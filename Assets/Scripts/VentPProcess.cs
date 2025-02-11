using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;


public class VentilationPostProcessing : MonoBehaviour
{
    private PostProcessVolume postProcessingVolume; // Reference to the Volume
    private ColorGrading colorAdjustments;
    private Bloom bloom;
    private Vignette vignette;
    private LensDistortion lensDistortion;

    void Start()
    {
        // Automatically find the Volume component in a child of the "Tunnel" object
        postProcessingVolume = GetComponentInChildren<PostProcessVolume>();

        bloom = postProcessingVolume.profile.GetSetting<Bloom>();
        vignette = postProcessingVolume.profile.GetSetting<Vignette>();
        lensDistortion = postProcessingVolume.profile.GetSetting<LensDistortion>();
        colorAdjustments = postProcessingVolume.profile.GetSetting<ColorGrading>();

        colorAdjustments.saturation.value = -50; // Desaturate colors
        colorAdjustments.colorFilter.value = Color.gray; // Apply a metallic tone


        bloom.intensity.value = 2.5f; // Subtle bloom effect
        

        vignette.intensity.value = 0.5f; // Add vignette effect
        vignette.smoothness.value = 0.8f;
       

        lensDistortion.intensity.value = -0.3f; // Apply mild lens distortion
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the ventilation area
        if (other.CompareTag("Player"))
        {
            OnEnterVentilation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exited the ventilation area
        if (other.CompareTag("Player"))
        {
            OnExitVentilation();
        }
    }

    public void OnEnterVentilation()
    {
        // Enable post-processing effects
        postProcessingVolume.enabled = true;
    }

    public void OnExitVentilation()
    {
        // Disable post-processing effects
        postProcessingVolume.enabled = false;
    }
}
