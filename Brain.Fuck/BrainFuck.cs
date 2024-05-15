using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace BrainFuck;

public static class Kata
{  
  
  public static StringBuilder? Result { get; set; }
  public static char[]? Code { get; set; }
  public static Queue<byte>? Input { get; set; }
  public static byte? Data { get; set; } = 0;
  public static int Position { get; set; } = 0;
  
  public static string BrainLuck(string code, string input)
  {
    Inititalisation();
    Execute();
    return Result?.ToString() ?? string.Empty;
  }

  void Inititalisation()
  {
    Code = code.ToCharArray();
    Input = new Queue<byte>(input.ToCharArray().Select(x => (byte)x));
  }

  void Execute()
  {
    foreach (char code in Code)
    {
      Interpretate(code).Invoke();
    }
  }
  public Action Interpretate(char value)
  {
    return char switch {
        '>' => () => Position++,
        '<' => () => Position--,
        '+' => () => Data++,
        '-' => () => Data--,
        '.' => OutputCurrentValue,
        ',' => GetValue,
        '[' => NextIfZero,
        ']' => PrevIfNotZero
          _ => throw new ArgumentException();
        
    };
  }
  */
    
  public static void OutputCurrentValue()
  {
    Result?.Append(Data);
    Data = 0;
  }
  
  public static void GetValue()
  {
    Data = Input?.Dequeue();
  }
  
  public static void NextIfZero()
  {
    if(Data == 0){
      NextCell();
    }
  }
  
  public static void PrevIfNotZero()
  {
    if(Data != 0)
    {
      PrevCell();
    }
  }
}