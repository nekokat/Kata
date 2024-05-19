using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace BrainFuck;

/// <summary>
/// 
/// </summary>
public static class Kata
{  
  /// <summary>
  /// 
  /// </summary>
  static StringBuilder result = new();

  /// <summary>
  /// 
  /// </summary>
  static char[] code;

  /// <summary>
  /// 
  /// </summary>
  static Queue<byte> input;

  /// <summary>
  /// 
  /// </summary>
  static byte[] data = new byte[1000];

  /// <summary>
  /// 
  /// </summary>
  static int position = 0;

  /// <summary>
  /// 
  /// </summary>
  static int pointer = 0;

  /// <summary>
  /// 
  /// </summary>
  static Stack<int> loopPosition = new();
  
  /// <summary>
  /// 
  /// </summary>
  /// <param name="brainCode"></param>
  /// <param name="dataInput"></param>
  /// <returns></returns>
  public static string BrainLuck(string brainCode, string dataInput)
  {
    Inititalisation(brainCode, dataInput);
    Execute();
    return result.ToString();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="brainCode"></param>
  /// <param name="dataInput"></param>
 static void Inititalisation(string brainCode, string dataInput)
  {
    code = brainCode.ToCharArray();
    input = new Queue<byte>(Encoding.ASCII.GetBytes(dataInput));
  }

  /// <summary>
  /// 
  /// </summary>
  static void Execute()
  {
    while(position < code.Count())
    {
      Interpretate(code[position]).Invoke();  
      position++;
    } 
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  static Action Interpretate(char value)
  {
    return value switch {
        //increment the data pointer (to point to the next cell to the right).
        '>' => () => ++pointer,
        //decrement the data pointer (to point to the next cell to the left).
        '<' => () => ++pointer,
        '+' => DataInc,
        '-' => DataDis,
        '.' => OutputCurrentValue,
        ',' => AddValueToData,
        '[' => NextIfZero,
        ']' => PrevIfNotZero,
          _ => throw new ArgumentException()
    };
  }
  
  /// <summary>
  /// accept one byte of input, storing its value in the byte at the data pointer.
  /// </summary>
  static void AddValueToData()
  {
    if (input.Count != 0){  
      data[pointer] = input.Dequeue();
    }
  }

  /// <summary>
  /// increment (increase by one, truncate overflow: 255 + 1 = 0) the byte at the data pointer.
  /// </summary>
  static void DataInc()
  {
    byte value = data[pointer];
    data[pointer] = ++value < byte.MaxValue ? value : byte.MinValue;
  }

  /// <summary>
  /// decrement (decrease by one, treat as unsigned byte: 0 - 1 = 255 ) the byte at the data pointer.
  /// </summary>
  static void DataDis()
  {
    byte value = data[pointer];
    data[pointer] = --value < byte.MinValue ? byte.MaxValue : value;
  }

  /// <summary>
  /// output the byte at the data pointer.
  /// </summary>
  static void OutputCurrentValue()
  {
    result.Append((char)data[pointer]);
  }
  
  /// <summary>
  /// Loop. If the byte at the data pointer is zero, 
  /// then instead of moving the instruction pointer forward to the next command,
  /// jump it forward to the command after the matching ] command.
  /// </summary>
  static void NextIfZero()
  {
    Action act = data[pointer] != 0 ? StartLoop : JumpEndLoop;
    act.Invoke();
  }

  /// <summary>
  /// Loop support command
  /// </summary>
  static void StartLoop()
  {
    loopPosition.Push(position);
  }
  
  /// <summary>
  /// Move to position in end of current loop.
  /// </summary>
  static void JumpEndLoop()
  {
    while (loopPosition.Count != 0)
    {
      if(code[++position] == ']')
      {
        loopPosition.Pop();
      }
    }
  }

  /// <summary>
  /// End of current loop. If the byte at the data pointer is nonzero,
  /// then instead of moving the instruction pointer forward to the next command,
  /// jump it back to the command after the matching [ command.
  /// </summary>
  static void PrevIfNotZero()
  {
    if (data[pointer] != 0 & loopPosition.Count != 0)
    {
      position = loopPosition.Peek();
    }
  }
}
