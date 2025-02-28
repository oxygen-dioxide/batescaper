using System.Diagnostics;
using System.IO;
using System.Text;

using BatEscaper;

namespace BatEscaper.Test;
public class CmdScriptTest: ICmdTest{
    UTF8Encoding noBomEncoding = new UTF8Encoding(false);
    string helperPath = Path.Join(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "print_to_file.py");
    public string Escape(string text){
        return BatEscaper.EscapeCmdArgumentScript(text);
    }

    public void RunTest(string text){
        //Generate script
        var escapedText = BatEscaper.EscapeCmdArgumentScript(text);
        var tmpFolder = Path.GetTempPath();
        var outputFilePath = Path.Join(tmpFolder, "output.txt");
        var cmdFilePath = Path.Join(tmpFolder, "echofile.bat");
        var cmdFileContent = $"python \"{helperPath}\" --file \"{outputFilePath}\" -- {escapedText}";
        File.WriteAllText(cmdFilePath, cmdFileContent, encoding:noBomEncoding);
        
        //Run script
        var startInfo = new ProcessStartInfo() {
            FileName = cmdFilePath,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
        using (var process = Process.Start(startInfo)) {
            process.WaitForExit();
        }

        //Get result
        var result = File.ReadAllText(outputFilePath);
        Assert.Equal(text, result);
    }
}