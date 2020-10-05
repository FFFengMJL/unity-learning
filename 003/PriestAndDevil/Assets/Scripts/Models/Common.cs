using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  public enum Location { Left, Right }

  public static class Utils
  {
    public static GameObject Instantiate(string path, Vector3 position)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, position, Quaternion.identity, null) as GameObject;
    }
  }
}
