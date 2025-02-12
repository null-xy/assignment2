using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 50f;
    public Transform planet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(planet.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
