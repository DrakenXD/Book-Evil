using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;


    public void Shake(float force)
    {
        FindObjectOfType<CinemachineImpulseSource>().GenerateImpulse(force);
        
     


    }

    private void Update()
    {
        mainCamera.rotation = Quaternion.Euler(0, 0, 0);
    }

   
 

}
