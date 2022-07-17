using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBall : MonoBehaviour
{
    private float currentRotation = 0.0f;
    [SerializeField] private float speed = 20.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //  transform.rotation.Set(transform.rotation.x + Time.deltaTime, 0.0f, 0.0f,0.0f);
    // Update is called once per frame
    void Update()
    {
        currentRotation+=Time.deltaTime * speed;
        transform.rotation=(Quaternion.Euler(0.0f, currentRotation, 0.0f)); 
    }
}
