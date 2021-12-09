using UnityEngine;


/// <summary>
/// This script gives the player a spirit and destorys the spirit object when the player collides with the spirit (picks it up). 
/// </summary>
public class DestroyParent : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            PlayerEntity.INSTANCE.spirits += 1;
            Destroy(transform.parent.gameObject);
        }
    }

}
