using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace BrainFuck;

public class Kata
{  
  
  public StringBuilder result = new();
  public char[] code;
  public Queue<byte> input;
  public Stack<byte> data = new();
  //TODO: don't change position
  public int position = 0;
  public bool loop = false;
  public Stack<int> loopPosition = new();
  
  public string BrainLuck(string brainCode, string dataInput)
  {
    Inititalisation(brainCode, dataInput);
    Execute();
    return result.ToString();
  }

  void Inititalisation(string brainCode, string dataInput)
  {
    code = brainCode.ToCharArray();
    input = new Queue<byte>(Encoding.ASCII.GetBytes(dataInput));
  }

  void Execute()
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
  
  public Action Interpretate(char value)
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
  
  public void AddInputToData() {
    if (input.Count != 0){
      data.Push(input.Dequeue());
    }
    position++;
  }

  public void DataInc()
  {
    byte value = data.Pop();
    byte insert = ++value < byte.MaxValue ? value : byte.MinValue;
    data.Push(insert);
    position++;
  }

  public void DataDis()
  {
    byte value = data.Pop();
    byte insert = --value < byte.MinValue ? byte.MaxValue : value;
    data.Push(insert);
    position++;
  }

  public void OutputCurrentValue()
  {
    result.Append(data.Pop());
    position++;
    loopPosition.Append(position);
  }
    
  public void NextIfZero()
  {
    if(data.Count == 0){
      position++;
      loop = true;
    }
  }
  
  public static void PrevIfNotZero()
  {
    if(data.Count != 0)
    {
      position = loopPosition.Peek();
    }
    else{ position ++; }
  }
}