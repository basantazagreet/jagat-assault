using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    [SerializeField] float controlSpeed = 20f;
    [SerializeField] float xRange = 3f;
    [SerializeField] float yRange = 3f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;

    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = 5f;
    float xThrow;
    float yThrow;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();

    }

    private void ProcessRotation()
    {
        //Pitch aauni position in screen ra control throw both le ho
        //position on screen le lyaune pitch
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor ;
        //Pushing the controls bata originated pitch
        float pitchDueToControlThrow = yThrow * controlPitchFactor;


        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        //yaw aaune position on screen le matra ho
        float yaw = transform.localPosition.x * positionYawFactor;
        //roll aaune just controller le ho
        float roll = xThrow*controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);


        float yOffset = Time.deltaTime * yThrow * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
