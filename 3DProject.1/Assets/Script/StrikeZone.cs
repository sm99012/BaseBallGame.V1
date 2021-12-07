using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeZone : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.transform.position, this.transform.localScale);
    }
}
