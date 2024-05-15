using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace BrainFuck;

public static class Kata
{  
  
  public static StringBuilder result = new();
  public static char[] code;
  public static Queue<byte> input;
  public static Stack<byte> data = new();
  //TDOD: don't change position
  public static int position = 0;
  
  public static string BrainLuck(string brainCode, string dataInput)
  {
    Inititalisation(brainCode, dataInput);
    Execute();
    return result.ToString();
  }

  static void Inititalisation(string brainCode, string dataInput)
  {
    code = brainCode.ToCharArray();
    input = new Queue<byte>(Encoding.ASCII.GetBytes(dataInput));
  }

  static void Execute()
  {
    while(true){
      if (position == code.Count() || input.Count == 0)
      {
        break;
      }
      int p = position;
      Interpretate(code[position]).Invoke();
    }
  }
  
  public static Action Interpretate(char value)
  {
    return value switch {
        '>' => () => position++,
        '<' => () => position--,
        '+' => DataInc,
        '-' => DataDis,
        '.' => OutputCurrentValue,
        ',' => AddInputToData,
        //TODO: create jmp
        '[' => NextIfZero,
        ']' => PrevIfNotZero,
          _ => throw new ArgumentException()
    };
  }
  
  public static void AddInputToData() {
    if (input.Count != 0){
      data.Push(input.Dequeue());
    }
    position++;
  }

  public static void DataInc()
  {
    byte value = data.Pop();
    byte insert = ++value < byte.MaxValue ? value : byte.MinValue;
    data.Push(insert);
    position++;
  }

  public static void DataDis()
  {
    byte value = data.Pop();
    byte insert = --value < byte.MinValue ? byte.MaxValue : value;
    data.Push(insert);
    position++;
  }

  public static void OutputCurrentValue()
  {
    result.Append(data.Pop());
    position++;
  }
    
  public static void NextIfZero()
  {
    if(data.Count == 0){
      position++;
    }
  }
  
  public static void PrevIfNotZero()
  {
    if(data.Count != 0)
    {
      position--;
    }
  }
}