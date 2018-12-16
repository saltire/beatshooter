using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
  public Vector3 movement;

  void Update() {
    transform.position += movement * Time.deltaTime;

    if (Mathf.Max(Mathf.Abs(transform.position.x), Mathf.Abs(transform.position.y)) > 11) {
      Destroy(gameObject);
    }
  }
}
