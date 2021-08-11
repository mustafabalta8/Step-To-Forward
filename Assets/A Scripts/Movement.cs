
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float HorizontalRotation;
    [SerializeField] float ThrustForce;

    AudioSource audioSource;
    Rigidbody rg;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

       
    }
    private void ProcessInput()
    {
        // process thrust
        if (Input.GetKey(KeyCode.Space))
        {
            rg.AddRelativeForce(Vector3.up*Time.deltaTime* ThrustForce);
            if(!audioSource.isPlaying)
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
        // process rotation
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(HorizontalRotation);
        }
        else if (Input.GetKey(KeyCode.D)) // if player tabs both A and D at the same time, A will work because of else if condition
        {
            ApplyRotation(-HorizontalRotation);
        }
    }
    private void ApplyRotation(float horizontalRotation)
    {
        rg.freezeRotation = true;//freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * horizontalRotation * Time.deltaTime);
        //rg.freezeRotation = false;//unfreezing rotation so physics system can take over
        rg.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
}
