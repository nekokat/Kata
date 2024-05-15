using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace BrainFuck;

public static class Kata
{  
  
  public static StringBuilder Result { get; set; } = new();
  public static char[] Code { get; set; }
  public static Queue<byte> Input { get; set; }
  public static Stack<byte> Data { get; set; } = new();
  public static int Position { get; set; } = 0;
  
  public static string BrainLuck(string code, string input)
  {
    Inititalisation(code, input);
    Execute();
    return Result.ToString();
  }

  static void Inititalisation(string code, string input)
  {
    Code = code.ToCharArray();
    Input = new Queue<byte>(input.ToCharArray().Select(x => (byte)x));
  }

  static void Execute()
  {
    while(Position != Code.Count()){
      if (Input.Count == 0 || Data.Count == 0)
      {
        break;
      }
      Interpretate(Code[Position]).Invoke();
    }
  }
  
  public static Action Interpretate(char value)
  {
    return value switch {
        '>' => () => Position++,
        '<' => () => Position--,
        '+' => DataInc,
        '-' => DataDis,
        '.' => OutputCurrentValue,
        ',' => () => Data.Push(Input.Dequeue()),
        '[' => NextIfZero,
        ']' => PrevIfNotZero,
          _ => throw new ArgumentException()
        
    };
  }
  
  public static void DataInc()
  {
    byte value = Data.Pop();
    Data.Push(value++);
  }

  public static void DataDis()
  {
    byte value = Data.Pop();
    Data.Push(value--);
  }

  public static void OutputCurrentValue()
  {
    Result.Append(Data.Pop());
  }
    
  public static void NextIfZero()
  {
    if(Data.Count == 0){
      Position++;
    }
  }
  
  public static void PrevIfNotZero()
  {
    if(Data.Count != 0)
    {
      Position--;
    }
  }
}