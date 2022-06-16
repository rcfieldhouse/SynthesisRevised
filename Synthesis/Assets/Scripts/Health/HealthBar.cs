using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public HealthSystem Health; 
    public Transform Transform;
    Vector3 ScaleHealthBar = new Vector3(0.9f, 0.7f, 1.0f);
    Vector3 ScaleHealthBarPos = new Vector3(0.0f, 0.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     ScaleHealthBar.x = Health.GetHealth()/Health.GetHealthLimit();
     ScaleHealthBarPos.x= ((Health.GetHealth() / Health.GetHealthLimit())/2-0.5f)*0.9f;
     gameObject.transform.localScale= ScaleHealthBar;
     gameObject.transform.position = Transform.position+ ScaleHealthBarPos;
    }
}
