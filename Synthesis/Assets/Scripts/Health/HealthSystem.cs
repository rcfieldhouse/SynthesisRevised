using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public float Health = 3.0f, MaxHealth = 3.0f;
    [SerializeField] private bool SuspendMovement = false;
    [SerializeField] private float Cooldown = 1.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Cooldown > 0)
        {
            Cooldown -= Time.deltaTime;
        }
    }

   public void TakeDamage(int damage,Vector3 Knockback)
    {
        gameObject.GetComponent<Rigidbody>().velocity=Knockback*damage*2;
        Health -= damage;      
            SetSuspendMove(true);    
    }
    public void SetSuspendMove(bool var)
    {   
        if ((Cooldown <= 0.0f)&& (var != SuspendMovement))
        {
                SuspendMovement = var;
                Cooldown = 0.25f;        
        }
    
    }
    public bool GetSuspendMove()
    {
        return SuspendMovement; 
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
