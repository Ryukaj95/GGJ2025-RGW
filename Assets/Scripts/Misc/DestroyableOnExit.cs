using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableOnExit : MonoBehaviour
{
    public void DestroyOnExit() {
        Destroy(this.gameObject);
    }
}
