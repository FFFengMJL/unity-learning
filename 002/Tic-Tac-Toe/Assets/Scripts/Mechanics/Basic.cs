using System;
using UnityEngine;

namespace TicTacToe.Mechanics
{
  public enum Player { Unfinished, Tie, First, Second };

  public class Basic
  {
    // 游戏是否正在进行
    protected bool playing = true;

    // 是否是P1行动
    protected bool turn = true;

    // 储存的历史记录
    protected readonly Player[,] history = new Player[3, 3];

    // 获取游戏状态
    public bool GetPlaying()
    {
      return playing;
    }

    public bool GetTurn()
    {
      return turn;
    }

    public Player GetHistory(int i, int j)
    {
      return history[i, j];
    }

    public void SetHistory(int i, int j)
    {
      history[i, j] = turn ? Player.First : Player.Second;
      turn = !turn;
    }

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
      // Cross-Line Check
      if (history[1, 1] != Player.Unfinished)
      {
        if (history[1, 1] == history[0, 0] && history[1, 1] == history[2, 2] ||
            history[1, 1] == history[0, 2] && history[1, 1] == history[2, 0])
        {
          playing = false;
          return history[1, 1];
        }
      }
      // Check if it is a tie.
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
