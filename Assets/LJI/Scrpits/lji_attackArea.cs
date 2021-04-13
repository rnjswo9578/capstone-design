using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lji_attackArea : MonoBehaviour
{
    public List<Collider> colliders
    {
        get
        {
            if (0 < colliderList.Count)
            {
                colliderList.RemoveAll(c => c == null);
            }
            return colliderList;
        }
    }

    private List<Collider> colliderList = new List<Collider>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("monster"))
        {
            colliders.Add(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("monster"))
        {
            colliders.Remove(other);
        }
    }
}
