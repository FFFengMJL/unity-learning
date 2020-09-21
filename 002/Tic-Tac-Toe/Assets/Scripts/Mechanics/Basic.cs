using System;
using UnityEngine;

namespace TicTacToe.Mechanics
{
  // 四种状态，分别代表：未着棋 | 平局 | 玩家1着棋 | 玩家2着棋
  public enum Player { Unfinished, Tie, First, Second };

  public class Basic
  {
    // 用于判断游戏是否正在进行
    protected bool playing = true;

    // 用于判断是否是P1行动
    protected bool turn = true;

    // 储存的历史记录
    protected readonly Player[,] history = new Player[3, 3];

    // 获取游戏状态
    public bool GetPlaying()
    {
      return playing;
    }

    // 获取当前回合所属
    public bool GetTurn()
    {
      return turn;
    }

    // 获取棋盘的位置
    public Player GetHistory(int i, int j)
    {
      return history[i, j];
    }

    // 着棋
    public void SetHistory(int i, int j)
    {
      history[i, j] = turn ? Player.First : Player.Second;
      turn = !turn;
    }

    // 重置
    public void Reset()
    {
      playing = true;
      turn = true;
      // 重置棋盘
      Array.Clear(history, 0, 3 * 3);
    }

    // 检查游戏是否结束
    public Player CheckWin()
    {
      // 行是否已经达成条件
      for (int i = 0; i < 3; ++i)
      {
        if (history[i, 0] != Player.Unfinished &&
            history[i, 0] == history[i, 1] &&
            history[i, 1] == history[i, 2])
        {
          playing = false;
          return history[i, 0];
        }
      }

      // 列是否已经达成条件
      for (int j = 0; j < 3; ++j)
      {
        if (history[0, j] != Player.Unfinished &&
            history[0, j] == history[1, j] &&
            history[1, j] == history[2, j])
        {
          playing = false;
          return history[0, j];
        }
      }

      // 斜线是否达成条件
      if (history[1, 1] != Player.Unfinished)
      {
        if (history[1, 1] == history[0, 0] && history[1, 1] == history[2, 2] ||
            history[1, 1] == history[0, 2] && history[1, 1] == history[2, 0])
        {
          playing = false;
          return history[1, 1];
        }
      }

      // 判断是否平局
      var numOfUnfinished = 0;
      for (int i = 0; i < 3; ++i)
      {
        for (int j = 0; j < 3; ++j)
        {
          if (history[i, j] == Player.Unfinished)
          {
            numOfUnfinished++;
          }
        }
      }
      if (numOfUnfinished == 0)
      {
        playing = false;
        return Player.Tie;
      }
      return Player.Unfinished;
    }

    public virtual void AIMove() { }
  }
}
