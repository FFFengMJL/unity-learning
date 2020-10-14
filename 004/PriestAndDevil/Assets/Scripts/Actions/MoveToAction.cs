using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PriestsAndDevils
{
  public class MoveToAction : Action
  {
    // 表示运动目的地。
    public Vector3 destination;
    // 表示运动速度。
    public float speed;

    // 创建 MoveToAction 。
    public static MoveToAction GetAction(GameObject gameObject, IActionCallback callback, Vector3 destination, float speed)
    {
      MoveToAction action = CreateInstance<MoveToAction>();
      // 设置需要进行直线运动的游戏对象。
      action.gameObject = gameObject;
      action.transform = gameObject.transform;
      action.callback = callback;
      // 设置直线运动的终点。
      action.destination = destination;
      // 设置直线运动的速度。
      action.speed = speed;
      return action;
    }

    public override void Start() { }

    // 在此方法中实现直线运动的逻辑。
    public override void Update()
    {
      transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
      if (transform.position == destination)
      {
        destroy = true;
        callback?.ActionDone(this);
      }
    }
  }
}