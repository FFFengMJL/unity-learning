using System.Collections.Generic;
using UnityEngine;

namespace PriestsAndDevils
{
  public class SequenceAction : Action, IActionCallback
  {
    public List<Action> sequence; // 动作序列
    public int repeat = 1; // 重复次数
    public int currentActionIndex = 0; // 正在进行的动作

    // 创建 SequenceAction 。
    public static SequenceAction GetAction(IActionCallback callback, List<Action> sequence, int repeat = 1, int currentActionIndex = 0)
    {
      SequenceAction action = CreateInstance<SequenceAction>();
      action.callback = callback;
      action.sequence = sequence;
      action.repeat = repeat;
      action.currentActionIndex = currentActionIndex;
      return action;
    }

    // 设置每个子动作的回调
    // 子动作完成时，切换至下一动作。
    public override void Start()
    {
      foreach (Action action in sequence)
      {
        action.callback = this;
        action.Start();
      }
    }

    // 执行子动作。
    public override void Update()
    {
      if (sequence.Count == 0)
      {
        return;
      }
      if (currentActionIndex < sequence.Count)
      {
        sequence[currentActionIndex].Update();
      }
    }

    // 子动作完成时的钩子函数
    // 用于切换下一子动作。
    public void ActionDone(Action action)
    {
      action.destroy = false;
      currentActionIndex++;
      if (currentActionIndex >= sequence.Count)
      {
        currentActionIndex = 0;

        if (repeat > 0) // 判断是否需要重复执行。
        {
          repeat--;
        }
        if (repeat == 0)
        {
          destroy = true;
          callback?.ActionDone(this);
        }
      }
    }

    // 响应 Object 被销毁的事件。
    void OnDestroy()
    {
      foreach (Action action in sequence)
      {
        Destroy(action);
      }
    }
  }
}