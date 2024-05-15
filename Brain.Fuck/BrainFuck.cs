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
    Inititalisation(code, input);
    Execute();
    return Result?.ToString() ?? string.Empty;
  }

  static void Inititalisation(string code, string input)
  {
    Code = code.ToCharArray();
    Input = new Queue<byte>(input.ToCharArray().Select(x => (byte)x));
  }

  static void Execute()
  {
    while(Position != Code.Length || Input.Count != 0){
      Interpretate(Code[Position]).Invoke();
    }
  }
  
  public static Action Interpretate(char value)
  {
    return value switch {
        '>' => () => Position++,
        '<' => () => Position--,
        '+' => () => Data++,
        '-' => () => Data--,
        '.' => OutputCurrentValue,
        ',' => () => Data = Input?.Dequeue(),
        '[' => NextIfZero,
        ']' => PrevIfNotZero,
          _ => throw new ArgumentException()
        
    };
  }
    
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
      Position++;
    }
  }
  
  public static void PrevIfNotZero()
  {
    if(Data != 0)
    {
      Position--;
    }
  }
}