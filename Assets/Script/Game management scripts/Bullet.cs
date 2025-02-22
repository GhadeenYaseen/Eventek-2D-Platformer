using UnityEngine;

/*
    instantiate bullets that kill enemies, and are shot using "x" key
*/

public class Bullet : MonoBehaviour
{
    public PlaterMovement player;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed=16f;

    private Rigidbody2D bulletRB;

    private void Update() 
    {
        if(Input.GetButtonDown("x"))
        {
            Debug.Log("shoot!");

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletPrefab.transform.rotation, this.gameObject.transform);
            bulletRB = bullet.GetComponent<Rigidbody2D>();
            
            if(player.isFacingRight)
            {
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, bulletRB.velocity.y);
                bullet.GetComponentInChildren<SpriteRenderer>().flipY = false;
            }
            else
            {
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * -1f, bulletRB.velocity.y);
                bullet.GetComponentInChildren<SpriteRenderer>().flipY = true;
            }
            
            Destroy(bullet,3f);
        }
    }
}
