using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){

        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerState>().DoHarm(collision.gameObject.GetComponent<PlayerState>().healthPoints);
            return;
        }
        Destroy(collision.gameObject.transform.parent.gameObject);
}
}
