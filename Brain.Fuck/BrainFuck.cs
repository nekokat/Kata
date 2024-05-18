using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace BrainFuck;

public static class Kata
{  
  static StringBuilder result = new();
  static char[] code;
  static Queue<byte> input;
  static Stack<byte> data = new();
  //TODO: don't change position
  static int position = 0;
  static Stack<int> loopPosition = new();
  
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
    while(input.Count != 0 || data.Count != 0){
      Interpretate(code[position]).Invoke();
    }
  }
  
  static Action Interpretate(char value)
  {
    return value switch {
        '>' => () => position++,
        '<' => () => position--,
        '+' => DataInc,
        '-' => DataDis,
        '.' => OutputCurrentValue,
        ',' => AddValueToData,
        '[' => NextIfZero,
        ']' => PrevIfNotZero,
          _ => throw new ArgumentException()
    };
  }
  
  static void AddValueToData()
  {
    if (input.Count != 0){  
      data.Push(input.Dequeue());
      position++;
    }
  }

  static void DataInc()
  {
    byte value = data.Pop();
    byte insert = ++value < byte.MaxValue ? value : byte.MinValue;
    data.Push(insert);
    position++;
  }

  static void DataDis()
  {
    byte value = data.Pop();
    byte insert = --value < byte.MinValue ? byte.MaxValue : value;
    data.Push(insert);
    position++;
  }

  static void OutputCurrentValue()
  {
    result.Append((char)data.Pop());
    position++;
  }
  
  //TODO: посмотреть условие
  static void NextIfZero()
  {
    if(data.Count != 0 & data.Peek() != 0){
      loopPosition.Push(++position);
    }
    else
    {
      while (code[position] != ']')
      {
        position++;
        if(code[position] == ']')
        {
          loopPosition.Pop();
        }
      }
    }
  }
  
  //TODO: посмотреть условие
  static void PrevIfNotZero()
  {
    if(loopPosition.Count != 0)
    {
      position = loopPosition.Peek();
    }
    else{
      position++;
    }
  }
}