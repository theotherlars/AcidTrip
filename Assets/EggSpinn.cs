using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpinn : MonoBehaviour
{
    void Update()
    {
        transform.eulerAngles = new Vector3 (transform.eulerAngles.x , transform.rotation.y + Time.time * 150f, transform.eulerAngles.z);
    }
}
