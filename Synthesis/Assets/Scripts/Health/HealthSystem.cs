using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    
    public float Health = 3.0f,MaxHealth=3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void TakeDamage(int damage)
    {
        Health-=damage;
    }

   public void HealthUp(int HealthUp)
    {
        Health+=HealthUp;   
    }
    public float GetHealth()
    {
        return Health;
    }
    public float GetHealthLimit()
    {
        return MaxHealth;
    }
}
