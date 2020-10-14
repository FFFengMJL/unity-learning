using UnityEngine;

namespace PriestsAndDevils
{
  public interface IActionCallback
  {
    void ActionDone(Action action); // 动作执行完毕
  }
}