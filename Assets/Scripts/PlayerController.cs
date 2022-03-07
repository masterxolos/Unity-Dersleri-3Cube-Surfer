using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int count = 1;

    [SerializeField] private float movementSpeed;

    [SerializeField] private float controlSpeed;

    [SerializeField] private bool isTouching;
    private float touchPosX;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        if (isTouching)
        {
            touchPosX += Input.GetAxis("Mouse X") * controlSpeed * Time.fixedDeltaTime;
        }

        transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z);
        transform.position += Vector3.forward * movementSpeed * Time.fixedDeltaTime;
    }

    private void GetInput()
    {
        if (Input.GetMouseButton(0))
        {
            isTouching = true;
        }
        else
        {
            isTouching = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("block"))
        {
            Debug.Log("triggered");
            other.gameObject.transform.parent = gameObject.transform;
            var currentPos = gameObject.transform.position;
            other.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y + count * 1f, currentPos.z);
            count++;
            other.gameObject.AddComponent<Destroyer>();
            var rb = other.gameObject.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.constraints = ~RigidbodyConstraints.FreezePositionY;
            
            other.gameObject.GetComponent<Collider>().isTrigger = false;
        }
    }
}
