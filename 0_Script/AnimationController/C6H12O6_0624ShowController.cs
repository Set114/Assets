using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C6H12O6_0624ShowController : MonoBehaviour
{
    public GameObject C6H12O6;
    public GameObject h2oNo;
    // Start is called before the first frame update
    void Start()
    {
        C6H12O6.SetActive(true);
        h2oNo.SetActive(false);
    }
}
