using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  public class BoatController : Moveable
  {
    public Boat model = new Boat();
    public Location location { get { return model.location; } set { model.location = value; } }

    // 船只移动
    public void Move()
    {
      if (model.location == Location.Left)
      {
        model.location = Location.Right;
        SetDestination(Boat.departure);
      }
      else
      {
        model.location = Location.Left;
        SetDestination(Boat.destination);
      }
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
      }
      model.characters = new Character[2];
    }
  }
}
