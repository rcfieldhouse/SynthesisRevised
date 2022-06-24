using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public HealthSystem Health; 
    public Transform Transform;
   private Vector3 ScaleHealthBar = new Vector3(0.9f, 0.7f, 1.0f);
  private  Vector3 ScaleHealthBarPos = new Vector3(0.0f, 0.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     ScaleHealthBar.x = Health.GetHealth()/Health.GetHealthLimit();
     ScaleHealthBarPos.x= ((Health.GetHealth() / Health.GetHealthLimit())/2-0.5f)*0.9f;
        if (Health.GetHealth() >= 0)
        {
            gameObject.transform.localScale = ScaleHealthBar;
            gameObject.transform.position = Transform.position + ScaleHealthBarPos;
        } 
    }
}
