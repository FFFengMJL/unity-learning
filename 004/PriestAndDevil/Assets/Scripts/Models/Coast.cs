using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  public class Coast
  {
    // 名字，用于区分左右
    public string name { get; set; }

    // 位置，表示左右
    public Location location { get; set; }

    // 代表起始点和终止点
    public static readonly Vector3 departure = new Vector3(7, 1, 0);
    public static readonly Vector3 destination = new Vector3(-7, 1, 0);

    // 角色用的位置
    public static readonly Vector3[] positions = {
                new Vector3(4.5f, 2.25f, 0),
                new Vector3(5.5f, 2.25f, 0),
                new Vector3(6.5f, 2.25f, 0),
                new Vector3(7.5f, 2.25f, 0),
                new Vector3(8.5f, 2.25f, 0),
                new Vector3(9.5f, 2.25f, 0),};

    // 角色
    public Character[] characters = new Character[6];

    // 获取岸边空的位置的指针
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
      Vector3 position = positions[GetEmptyIndex()];
      position.x *= (location == Location.Right ? 1 : -1); // 坐标计算
      return position;
    }

    // 返回该岸边角色组成
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