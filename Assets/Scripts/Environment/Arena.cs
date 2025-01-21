using UnityEngine;

public class Arena : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.GetComponent<DestroyableOnExit>()) {
            other.gameObject.GetComponent<DestroyableOnExit>().DestroyOnExit();
        }
    }
}
