using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefab;
    [SerializeField] private float PositionIndex=1; 
    // Start is called before the first frame update
    void Start()
    {
        PositionIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(prefab[0], new Vector3(0.0f, PositionIndex * 20, 0.0f), Quaternion.identity);
            PositionIndex += 1;
        }
    }
}
