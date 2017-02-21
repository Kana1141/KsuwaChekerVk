using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsuwaChecker
{
    internal class PrefixedWriter : TextWriter
        {
            private readonly TextWriter _originalOut;

            public PrefixedWriter()
            {
                _originalOut = Console.Out;
            }

            public override Encoding Encoding => new ASCIIEncoding();

            public override void WriteLine(string message)
            {
            _originalOut.WriteLine($"{DateTime.Now:T} {message}");
            }
            public override void Write(string message)
            {
            _originalOut.Write($"> {message}");
            }
        }
    }

