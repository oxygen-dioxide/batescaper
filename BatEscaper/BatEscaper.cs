using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace BatEscaper
{
    public static class BatEscaper
    {
        /// <summary>
        /// Escapes an argument for the CMD command in Windows.
        ///
        /// This variant is the one to use when using direct system call invoking CMD on Windows. If
        /// you are intending to integrate the result of this function in a .bat script you should use
        /// `escape_cmd_argument_script()` instead.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EscapeCmdArgumentDirect(string text){
            return EscapeCmd(text, false);
        }

        /// <summary>
        /// Escapes an argument for the CMD command in Windows.
        ///
        /// This variant is the one to use when you want to create a .bat script. If you are intending to
        /// integrate the result of this function in direct system call to CMD you should use
        /// `escape_cmd_argument_direct()` instead.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EscapeCmdArgumentScript(string text){
            return EscapeCmd(text, true);
        }

        static string[] BackSlashEscapes = { "\\", "\"",};

        static string EscapeCmd(string text, bool isScript){
            var result = new StringBuilder();
            var etor = StringInfo.GetTextElementEnumerator(text);
            while(etor.MoveNext()){
                string c = etor.GetTextElement();
                if(isScript && c=="%"){
                    result.Append("%%");
                } else if(BackSlashEscapes.Contains(c)){
                    result.Append("\\"+c);
                } else {
                    result.Append(c);
                }
            }
            return $"\"{result}\"";
        }
    }
}
