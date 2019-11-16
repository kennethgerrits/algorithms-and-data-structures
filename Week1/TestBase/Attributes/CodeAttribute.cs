using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Alg1.Practica.TestBase.Attributes
{
    public class CodeAttribute : PropertyAttribute, ITestAction
    {
        static readonly string ClassKey = "Class";
        static readonly string MethodKey = "Method";

        public String ClassName => Properties.Get(ClassKey) as string;
        public String MethodName => Properties.Get(MethodKey) as string;

        public CodeAttribute(String className, String methodName) : base()
        {
            Properties.Add(ClassKey, className);
            Properties.Add(MethodKey, methodName);
        }

        public ActionTargets Targets => ActionTargets.Test;

        public void AfterTest(ITest test)
        {
            try
            {
                var currentFilePath = Assembly.GetExecutingAssembly().Location;
                DirectoryInfo currentDirectory = new DirectoryInfo(currentFilePath);
                DirectoryInfo targetDirectory = currentDirectory.Parent;

                var classFile = targetDirectory.GetFiles(ClassName + ".cs").First();

                string line;
                bool found = false;
                var ss = new StringWriter();
                using (var reader = new StreamReader(classFile.FullName))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        var lowerLine = line.ToLowerInvariant();

                        if (found && (lowerLine.Contains("public ") || lowerLine.Contains("private ") || lowerLine.Contains("protected")))
                        {
                            break;
                        }

                        if (lowerLine.Contains("public ") &&
                            (lowerLine.Contains(MethodName.ToLowerInvariant() + "(") ||
                             lowerLine.Contains(MethodName.ToLowerInvariant() + " (")))
                        {
                            found = true;
                        }

                        if (found)
                        {
                            ss.WriteLine(line);
                        }
                    }
                }
                test.Properties.Add("Code", ss.ToString());

            }
            catch
            {
            }

        }

        public void BeforeTest(ITest test)
        {
        }
    }
}
