using UnityEngine;

public class Arena : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other) {
        if (other.GetComponent<DestroyableOnExit>()) {
            other.GetComponent<DestroyableOnExit>().DestroyOnExit();
        }
    }
}
