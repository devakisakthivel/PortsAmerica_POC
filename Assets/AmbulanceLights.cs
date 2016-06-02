using UnityEngine;
using System.Collections;

public class AmbulanceLights : MonoBehaviour {

    
    public Color firstColor, secondColor;
    public bool onFirst;
    Component[] lensFlares;
    Component[] spotLight;
    public float flashDelay;
    
	

    void OnEnable()
    {
            spotLight = GetComponentsInChildren<Light>();
            lensFlares = GetComponentsInChildren<LensFlare>();
            StartCoroutine(EmergencyLights());
    }

    void Update()
    {

    }



    IEnumerator EmergencyLights()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(flashDelay);
            foreach (LensFlare flare in lensFlares)
            {
                flare.enabled = onFirst;
            }

            foreach (Light lights in spotLight)
            {
                lights.enabled = onFirst;
            }
            GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", firstColor);
            yield return new WaitForSeconds(flashDelay);
            foreach (LensFlare flare in lensFlares)
            {
                flare.enabled = !onFirst;
            }

            foreach (Light lights in spotLight)
            {
                lights.enabled = !onFirst;
            }
            GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", secondColor);
        }
    }
}
