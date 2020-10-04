using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
  // Update is called once per frame
  void Update()
  {
    transform.RotateAround(transform.position, Vector3.up, Random.Range(1, 3));
  }
}
