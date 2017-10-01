// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.3.6
// Machine:  HUB
// DateTime: 21.09.2017 20:15:14
// UserName: someone
// Input file <SimpleYacc.y>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace SimpleParser
{
public enum Tokens {
    error=1,EOF=2,BEGIN=3,END=4,CYCLE=5,INUM=6,
    RNUM=7,ID=8,ASSIGN=9,SEMICOLON=10};

// Abstract base class for GPLEX scanners
public abstract class ScanBase : AbstractScanner<int,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

public class Parser: ShiftReduceParser<int, LexLocation>
{
  // Verbatim content from SimpleYacc.y
// ��� ���������� ����������� � ����� GPPGParser, �������������� ����� ������, ������������ �������� gppg
    public Parser(AbstractScanner<int, LexLocation> scanner) : base(scanner) { }
  // End verbatim content from SimpleYacc.y

#pragma warning disable 649
  private static Dictionary<int, string> aliasses;
#pragma warning restore 649
  private static Rule[] rules = new Rule[14];
  private static State[] states = new State[22];
  private static string[] nonTerms = new string[] {
      "progr", "$accept", "block", "stlist", "statement", "assign", "cycle", 
      "ident", "expr", };

  static Parser() {
    states[0] = new State(new int[]{3,4},new int[]{-1,1,-3,3});
    states[1] = new State(new int[]{2,2});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{8,14,3,4,5,18},new int[]{-4,5,-5,21,-6,9,-8,10,-3,16,-7,17});
    states[5] = new State(new int[]{4,6,10,7});
    states[6] = new State(-12);
    states[7] = new State(new int[]{8,14,3,4,5,18},new int[]{-5,8,-6,9,-8,10,-3,16,-7,17});
    states[8] = new State(-4);
    states[9] = new State(-5);
    states[10] = new State(new int[]{9,11});
    states[11] = new State(new int[]{8,14,6,15},new int[]{-9,12,-8,13});
    states[12] = new State(-9);
    states[13] = new State(-10);
    states[14] = new State(-8);
    states[15] = new State(-11);
    states[16] = new State(-6);
    states[17] = new State(-7);
    states[18] = new State(new int[]{8,14,6,15},new int[]{-9,19,-8,13});
    states[19] = new State(new int[]{8,14,3,4,5,18},new int[]{-5,20,-6,9,-8,10,-3,16,-7,17});
    states[20] = new State(-13);
    states[21] = new State(-3);

    rules[1] = new Rule(-2, new int[]{-1,2});
    rules[2] = new Rule(-1, new int[]{-3});
    rules[3] = new Rule(-4, new int[]{-5});
    rules[4] = new Rule(-4, new int[]{-4,10,-5});
    rules[5] = new Rule(-5, new int[]{-6});
    rules[6] = new Rule(-5, new int[]{-3});
    rules[7] = new Rule(-5, new int[]{-7});
    rules[8] = new Rule(-8, new int[]{8});
    rules[9] = new Rule(-6, new int[]{-8,9,-9});
    rules[10] = new Rule(-9, new int[]{-8});
    rules[11] = new Rule(-9, new int[]{6});
    rules[12] = new Rule(-3, new int[]{3,-4,4});
    rules[13] = new Rule(-7, new int[]{5,-9,-5});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
    switch (action)
    {
    }
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliasses != null && aliasses.ContainsKey(terminal))
        return aliasses[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

}
}