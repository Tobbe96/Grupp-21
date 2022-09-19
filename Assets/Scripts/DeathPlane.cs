using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("WingMan") == true)
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
        if (collision.CompareTag("Player") == true)
        {
            collision.gameObject.GetComponent<PlayerState>().Respawn();
        }
}
}
