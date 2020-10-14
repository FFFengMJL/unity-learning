using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace PriestsAndDevils
{
  public class Director : System.Object
  {
    private static Director instance;                             // 导演必须为单例
    public ISceneController currentSceneController { get; set; }  // 当前场记
    public bool running { get; set; }                             // 游戏是否正在运行

    public int fps // 帧数
    {
      get
      {
        return Application.targetFrameRate;
      }
      set
      {
        Application.targetFrameRate = value;
      }
    }

    // 获取单例
    public static Director GetInstance()
    {
      return instance ?? (instance = new Director());
    }

    // 比较重要的函数
    public void OnSceneWake(ISceneController controller)
    {
      this.currentSceneController = controller;
      controller.LoadResources();
    }

  }
}