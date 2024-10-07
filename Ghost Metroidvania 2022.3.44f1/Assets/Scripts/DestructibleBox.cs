using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBox : MonoBehaviour
{
   public void DestroyBox()
    {
        Destroy(gameObject);
    }
}
