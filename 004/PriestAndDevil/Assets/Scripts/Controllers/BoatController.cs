using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  public class BoatController : Moveable
  {
    public Boat model = new Boat();
    public Location location { get { return model.location; } set { model.location = value; } }
    private Vector3 nowDestination;

    // 船只移动
    public void Move()
    {
      if (location == Location.Left)
      {
        this.location = Location.Right;
        this.nowDestination = Boat.departure;
      }
      else
      {
        this.location = Location.Left;
        this.nowDestination = Boat.destination;
      }
    }

    // 获取目的地
    public Vector3 GetDestination()
    {
      return this.nowDestination;
    }



    // 上船时调用
    public void GoAboard(CharacterController character)
    {
      int index = model.GetEmptyIndex();
      model.characters[index] = character.model;
    }

    // 上岸时调用
    public void GoAshore(CharacterController character)
    {
      var characters = model.characters;
      for (int i = 0; i < characters.Length; ++i)
      {
        if (characters[i] != null && characters[i].name == character.name)
        {
          characters[i] = null;
        }
      }
    }

    // 重置游戏时调用
    public new void Reset()
    {
      base.Reset();
      if (location == Location.Left)
      {
        Move();
        transform.position = nowDestination;
      }
      model.characters = new Character[2];
    }
  }
}
