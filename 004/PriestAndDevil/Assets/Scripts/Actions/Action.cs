using UnityEngine;

namespace PriestsAndDevils
{
  public class Action : ScriptableObject
  {
    public bool enable = true; // 动作是否完成
    public bool destroy = false; // 是否需要进行运动
    public GameObject gameObject { get; set; }
    public Transform transform { get; set; }
    public IActionCallback callback; // 动作执行完毕后，需要通知的对象。

    // 初始化
    public virtual void Start()
    {
      // 提示用户需要实现此方法！
      throw new System.NotImplementedException();
    }

    // 动作逻辑
    public virtual void Update()
    {
      // 提示用户需要实现此方法！
      throw new System.NotImplementedException();
    }
  }
}