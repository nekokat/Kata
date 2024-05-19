using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace BrainFuck;

/// <summary>
/// INTERPRETATION OF THE PROGRAMMING LANGUAGE "BRAINFU*K"
/// </summary>
public static class Kata
{  
  /// <summary>
  /// Output value
  /// </summary>
  static StringBuilder result;

  /// <summary>
  /// Interpreted program code
  /// </summary>
  static char[] code;

  /// <summary>
  /// Input data for program code
  /// </summary>
  static Queue<byte> input;

  /// <summary>
  /// Data pointer
  /// </summary>
  static byte[] data;

  /// <summary>
  /// Current carriage position
  /// </summary>
  static int position;

  /// <summary>
  /// Сell number in the date pointer
  /// </summary>
  static int pointer;

  /// <summary>
  /// Loop bracket counter
  /// </summary>
  static int bracketsCounter;
  
  /// <summary>
  /// Starts calculations
  /// </summary>
  /// <param name="brainCode">Interpreted program code</param>
  /// <param name="dataInput">Input data for program code</param>
  /// <returns>Result of program execution</returns>
  public static string BrainLuck(string brainCode, string dataInput)
  {
    Inititalisation(brainCode, dataInput);
    Execute();
    return result.ToString();
  }

  /// <summary>
  /// Initializing the program and assigning the necessary values
  /// </summary>
  /// <param name="brainCode">Interpreted program code</param>
  /// <param name="dataInput">Input data for program code</param>
 static void Inititalisation(string brainCode, string dataInput)
  {
    result = new();
    bracketsCounter = 0;
    pointer = 0;
    position = 0;
    data = new byte[30000];
    code = brainCode.ToCharArray();
    input = new Queue<byte>(Encoding.ASCII.GetBytes(dataInput));
  }

  /// <summary>
  /// Execute program
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
  /// Interpretation of the operator into action
  /// </summary>
  /// <param name="value">Symbolic representation of operator</param>
  /// <returns>Action assigned to operator</returns>
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
      '.' => () => result.Append(Convert.ToChar(data[pointer])),
      //accept one byte of input, storing its value in the byte at the data pointer.
      ',' => () => input.TryDequeue(out data[pointer]),
      '[' => Loop,
      ']' => EndLoop,
        _ => None
    };
  }

  /// <summary>
  /// Do nothing (skipping a character in the code)
  /// </summary>
  static void None(){}

  /// <summary>
  /// Increment (increase by one, truncate overflow: 255 + 1 = 0) the byte at the data pointer.
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
    if (data[pointer] == 0)
    {
      bracketsCounter++;
      while (bracketsCounter >= 1)
      {
        position++;
        if (code[position] == '[')
        {
          bracketsCounter++;
        } else if(code[position] == ']'){
          bracketsCounter--;
        }
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
    if (data[pointer] != 0)
    {
      bracketsCounter++;
      while (bracketsCounter >= 1)
      {
        position--;
        if(code[position] == ']')
        {
          bracketsCounter++;
        }
        else if(code[position] == '[')
        {
          bracketsCounter--;
        }
      }
    }
  }
}
