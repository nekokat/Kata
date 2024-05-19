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
  static StringBuilder result;

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
  static byte[] data;

  /// <summary>
  /// 
  /// </summary>
  static int position;

  /// <summary>
  /// 
  /// </summary>
  static int pointer;

  /// <summary>
  /// 
  /// </summary>
  static Stack<int> loopPosition;
  
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
    result = new();
    loopPosition = new();
    pointer = 0;
    position = 0;
    data = new byte[1000];
    code = brainCode.ToCharArray();
    input = new Queue<byte>(Encoding.ASCII.GetBytes(dataInput));
  }

  /// <summary>
  /// 
  /// </summary>
  static void Execute()
  {
    while(position < code.Length)
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
      '<' => () => --pointer,
      '+' => IncreaseData,
      '-' => DecreaseData,
      //output the byte at the data pointer.
      '.' => () => result.Append((char)data[pointer]),
      //accept one byte of input, storing its value in the byte at the data pointer.
      ',' => () => input.TryDequeue(out data[pointer]),
      '[' => Loop,
      ']' => EndLoop,
        _ => throw new ArgumentException()
    };
  }

  /// <summary>
  /// increment (increase by one, truncate overflow: 255 + 1 = 0) the byte at the data pointer.
  /// </summary>
  static void IncreaseData()
  {
    byte value = ++data[pointer];
    data[pointer] = value < byte.MaxValue ? value : byte.MinValue;
  }

  /// <summary>
  /// decrement (decrease by one, treat as unsigned byte: 0 - 1 = 255 ) the byte at the data pointer.
  /// </summary>
  static void DecreaseData()
  {
    byte value = --data[pointer];
    data[pointer] = value < byte.MinValue ? byte.MaxValue : value;
  }
  
  /// <summary>
  /// Loop. If the byte at the data pointer is zero, 
  /// then instead of moving the instruction pointer forward to the next command,
  /// jump it forward to the command after the matching ] command.
  /// </summary>
  static void Loop()
  {
    if (data[pointer] != 0)
    {
      loopPosition.Push(position);
    }
    else{
      while (loopPosition.Count != 0)
      {
        if (code[position] == '[')
        {
          loopPosition.Push(position);
        }

        if(code[position] == ']')
        {
          loopPosition.Pop();
        }
        position++;
      }
    }
  }

  /// <summary>
  /// End of current loop. If the byte at the data pointer is nonzero,
  /// then instead of moving the instruction pointer forward to the next command,
  /// jump it back to the command after the matching [ command.
  /// </summary>
  static void EndLoop()
  {
    if(loopPosition.Count != 0 & data[pointer] != 0){
      position = loopPosition.Peek();
    }
    else
    {
      loopPosition.Pop();
    }
  }
}
