using System.Collections.Generic;
using UnityEngine;

namespace PriestsAndDevils
{
  public class GameActionManager : ActionManager
  {
    // 移动船只。
    public void MoveBoat(BoatController boat)
    {
      // 创建船的直线运动。
      MoveToAction action = MoveToAction.GetAction(boat.gameObject, this, boat.GetDestination(), 20);
      AddAction(action);
    }

    // 移动人物。
    public void MoveCharacter(CharacterController character)
    {
      Vector3 destination = character.GetDestination();
      GameObject gameObject = character.gameObject;
      Vector3 currentPosition = character.transform.position;
      Vector3 pivotPosition = currentPosition; // 横向直线运动和纵向直线运动的转折点。

      if (destination.y > currentPosition.y)
      {
        pivotPosition.y = destination.y;
      }
      else
      {
        pivotPosition.x = destination.x;
      }
      // 创建序列动作来表示人物的折线运动：横向的直线运动、纵向的直线运动。
      Action action1 = MoveToAction.GetAction(gameObject, null, pivotPosition, 20);
      Action action2 = MoveToAction.GetAction(gameObject, null, destination, 20);
      SequenceAction action = SequenceAction.GetAction(this, new List<Action> { action1, action2 });
      AddAction(action);
    }
  }
}