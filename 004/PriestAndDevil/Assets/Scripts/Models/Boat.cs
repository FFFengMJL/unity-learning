using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  public class Boat
  {
    // 名称，但是好像没用上
    public string name { get; set; }

    // 船的位置：左岸|右岸
    public Location location { get; set; }

    // 分别储存离开时的坐标和目标坐标
    public static readonly Vector3 departure = new Vector3(3, 1, 0);
    public static readonly Vector3 destination = new Vector3(-3, 1, 0);

    // 用于角色移动
    public static readonly Vector3[] departures = { new Vector3(2.5f, 1.5f, 0), new Vector3(3.5f, 1.5f, 0) };
    public static readonly Vector3[] destinations = { new Vector3(-3.5f, 1.5f, 0), new Vector3(-2.5f, 1.5f, 0) };

    // 角色
    public Character[] characters = new Character[2];

    // 初始化船只
    public Boat()
    {
      // 起始时刻，船只需要在右岸
      location = Location.Right;
    }

    // 用于判断船上是否为空载
    public bool IsEmpty()
    {
      for (int i = 0; i < characters.Length; ++i)
      {
        if (characters[i] != null)
        {
          return false;
        }
      }
      return true;
    }

    // 获取船只空的位置
    public int GetEmptyIndex()
    {
      for (int i = 0; i < characters.Length; ++i)
      {
        if (characters[i] == null)
        {
          return i;
        }
      }
      return -1;
    }

    // 返回空的位置
    public Vector3 GetEmptyPosition()
    {
      Vector3 position;
      int index = GetEmptyIndex();
      if (location == Location.Right)
      {
        position = departures[index];
      }
      else
      {
        position = destinations[index];
      }
      return position;
    }

    // 获取牧师魔鬼的数量
    public int[] GetCharacterAmount()
    {
      int[] amount = { 0, 0 }; // 牧师 | 魔鬼
      for (int i = 0; i < characters.Length; ++i)
      {
        if (characters[i] != null)
        {
          if (characters[i].name.Contains("Priest"))
          {
            amount[0]++; // 牧师
          }
          else
          {

            amount[1]++; // 魔鬼
          }
        }
      }
      return amount;
    }
  }
}