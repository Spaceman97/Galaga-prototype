using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BoundsCheck bnd;
    // Start is called before the first frame update
    void Start()
    {
        bnd = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bnd.offUp) {
            Destroy(gameObject);
        }
    }
}
