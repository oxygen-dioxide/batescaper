using System.Collections.Generic;

namespace BatEscaper.Test;

public class Tests
{
    List<ICmdTest> testTypes = new List<ICmdTest>();

    public Tests(){
        testTypes.Add(new CmdScriptTest());
    }

    public void TestStr(string text){
        foreach(var testType in testTypes){
            testType.RunTest(text);
        }
    }

    [Fact]
    public void TestBasicCalls(){
        TestStr("hello world");
    }

    [Fact]
    public void TestPrintableAsciiOnlyChar(){
        foreach(var i in Enumerable.Range(32, 127-32)){
            TestStr($"{(char)i}");
        }
    }

    [Fact]
    public void TestPrintableAsciiStartsText(){
        foreach(var i in Enumerable.Range(32, 127-32)){
            TestStr($"{(char)i}a");
        }
    }

    [Fact]
    public void TestPrintableAsciiEndsText(){
        foreach(var i in Enumerable.Range(32, 127-32)){
            TestStr($"a{(char)i}");
        }
    }

    [Fact]
    public void TestPrintableAsciiInText(){
        foreach(var i in Enumerable.Range(32, 127-32)){
            TestStr($"a{(char)i}b");
        }
    }

    [Fact]
    public void TestPrintableAsciiDoubledChar(){
        foreach(var i in Enumerable.Range(32, 127-32)){
            TestStr($"{(char)i}{(char)i}");
        }
    }

    [Fact]
    public void TestPrintableAsciiDoubledStartsText(){
        foreach(var i in Enumerable.Range(32, 127-32)){
            TestStr($"{(char)i}{(char)i}a");
        }
    }

    [Fact]
    public void TestPrintableAsciiDoubledEndsText(){
        foreach(var i in Enumerable.Range(32, 127-32)){
            TestStr($"a{(char)i}{(char)i}");
        }
    }

    [Fact]
    public void TestPrintableAsciiDoubledInText(){
        foreach(var i in Enumerable.Range(32, 127-32)){
            TestStr($"a{(char)i}{(char)i}b");
        }
    }

    [Fact]
    public void TestCommandInjection(){
        TestStr("\"&calc.exe&\"");
    }
}
