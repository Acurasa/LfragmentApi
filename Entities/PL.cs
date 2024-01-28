using Microsoft.JSInterop.Infrastructure;
using System;
using System.Collections;

namespace LfragmentApi.Entities
{
    public static class PL 
    {
        public const string csharp = "C#";
        public const string java = "Java";
        public const string cpp = "C++";
        public const string js = "JavaScript";
        public const string rubyonrails = "RubyOnRails";
        public const string haskell = "Haskell";
        public const string asm = "ASM";
        public const string html = "HTML";
        public const string css = "CSS";
        public const string yaml = "YAML";

        //public static List<string> ToList()                       //reflection is too heavy 
        //{
        //    return typeof(PL)
        //        .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
        //        .Where(f => f.FieldType == typeof(string))
        //        .Select(f => (string)f.GetValue(null))
        //        .ToList();
        //}
        public static List<string> ToList()
        {
            return new List<string>
            {
                CSharp, Java, Cpp, JavaScript, RubyOnRails, Haskell, ASM, HTML, CSS, YAML
            };
        }
    }


}
