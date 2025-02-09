using UnityEngine;

public class desroyble : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody2D != null)
            {
                playerRigidbody2D.AddForce(transform.up * 8f, ForceMode2D.Impulse);
            }

            enemy enemyScript = gameObject.GetComponentInParent<enemy>();
            if (enemyScript != null)
            {
                enemyScript.startDeart();
            }
        }
    }
}
