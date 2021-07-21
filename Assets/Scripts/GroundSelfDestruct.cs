using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSelfDestruct : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        if(transform.parent.gameObject.tag != "Land")
            Destroy(transform.parent.gameObject);
    }
}
