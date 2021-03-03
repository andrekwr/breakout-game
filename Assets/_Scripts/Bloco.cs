using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{
    public GameObject DeathParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
            Instantiate(DeathParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
