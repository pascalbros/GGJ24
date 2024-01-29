using UnityEngine;

public class Collectible: MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        // Check if the colliding object is on the player layer
        if (other.CompareTag("Player")) {
            AudioManager.Instance.PlaySfx("glug");
            Destroy(gameObject);
            PlayerController.Instance.OnCollectible();
        }
    }
}