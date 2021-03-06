﻿using System;
using System.Collections.Generic;
using System.IO;
using WixXmlGenerator.Services;
using WixXmlGenerator.Statics;

namespace WixXmlGenerator.Commands
{
    public class GenerateCommand : Command, ICommand
    {
        public GenerateCommand() : base(8)
        {

        }

        public string Execute(List<string> args)
        {
            try
            {
                var sourceDirArg = args[1];
                var sourceDir = args[2];
                var wxsPathArg = args[3];
                var wxsPath = args[4];
                var outputFileArg = args[5];
                var outputFile = args[6];
                var projectNameArg = args[7];
                var projectName = args[8];

                var resultString = "";

                if (sourceDirArg == Arguments.SourceDir && outputFileArg == Arguments.OutputFile && wxsPathArg == Arguments.WxsDir && projectNameArg == Arguments.ProjectName)
                {
                    if (Directory.Exists(sourceDir))
                    {
                        if (File.Exists(outputFile))
                        {
                            Console.WriteLine();
                            Console.Write("File path '" + outputFile + "' already exists. Do you wish to overwrite this file? [Y/N]: ");
                            var response = (char) Console.Read();
                            if (response == 'y' || response == 'Y')
                            {
                                resultString = Services.WixXmlGenerator.Generate(sourceDir, outputFile, wxsPath, projectName);
                            }
                            else if (response == 'n' || response == 'N')
                            {
                                return "Please re-run the tool with a different output file path.";
                            }
                            else
                            {
                                throw new Exception("Response '" + response + "' is not a valid response.");
                            }
                        }
                        else
                        {
                            resultString = Services.WixXmlGenerator.Generate(sourceDir, outputFile, wxsPath, projectName);
                        }
                    }
                    else
                    {
                        throw new Exception("Directory path " + sourceDir + " is not valid.");
                    }
                }
                else
                {
                    throw new Exception("One or more of the provided arguments not recognized.");
                }

                OutputFileGenerator.Generate(resultString, outputFile);

                return "Output file successfully created at '" + outputFile + "'.";
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
