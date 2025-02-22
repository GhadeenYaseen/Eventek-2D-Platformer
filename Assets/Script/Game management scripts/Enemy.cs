using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("enemy shot!");
            this.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
