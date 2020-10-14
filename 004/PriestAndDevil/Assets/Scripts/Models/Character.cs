using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  public class Character
  {
    // It 名称
    public string name { get; set; }

    // 用于储存在哪个岸边
    public Location location { get; set; }

    // 判断是否在船上
    public bool isOnboard { get; set; }
  }
}