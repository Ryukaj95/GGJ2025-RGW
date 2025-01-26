using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableOnExit : MonoBehaviour
{
    public void DestroyOnExit()
    {
        //  Debug.Log(this.gameObject.name);
        Destroy(this.gameObject);
    }
}
