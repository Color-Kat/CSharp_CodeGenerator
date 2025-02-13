using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeGenerator
{
    class Metadata
    {
        public string StructName { get; set; }
        public string[] StructData { get; set; }
    }

    internal class CodeGenerator
    {

        private string sampleLine, generatedCode;
        private string[] metadataLines;
        private StreamReader sampleReader;
        private StreamWriter codeWriter;

        private readonly string[] outputTemplates =
        {
            "byte[] outputBytes = new byte[#length];",
            "for (int i = 0; i < #length; i++)",
            "outputBytes[i] = inputBuffer->#field[i];",
            "dataGridView.Rows[rowIndex].Cells[#columnIndex].Value = encoding.GetString(outputBytes, 0, outputBytes.Length);",
            "dataGridView.Rows[rowIndex].Cells[#columnIndex].Value = inputBuffer->#field.ToString();"
        };

        public void generateCode2(string templateFilePath, string metadataFilePath, string outputFilePath)
        {
            string jsonContent = File.ReadAllText(metadataFilePath);
            var metadata = JsonSerializer.Deserialize<Metadata>(jsonContent);
            string template = File.ReadAllText(templateFilePath);

            if (metadata != null)
            {
                string structData = string.Join("\n    ", metadata.StructData);
                string generatedCode = template.Replace("#StructName", metadata.StructName)
                                               .Replace("#StructData", structData);

                File.WriteAllText(outputFilePath, generatedCode);
            }
        }

        public void generateCode(string templateFilePath, string metadataFilePath)
        {
            // Read metadata file
            metadataLines = File.ReadAllLines(metadataFilePath);

            using (sampleReader = new StreamReader(templateFilePath))
            using (codeWriter = new StreamWriter("GeneratedCode.cs"))
            {
                while ((sampleLine = sampleReader.ReadLine()) != null)
                {
                    generatedCode = sampleLine;

                    // Process structure - empty line
                    if (sampleLine.Contains("struct"))
                    {
                        codeWriter.WriteLine(sampleLine);
                        if ((sampleLine = sampleReader.ReadLine()) != null)
                        {
                            codeWriter.WriteLine(sampleLine);
                            for (int i = 1; i < metadataLines.Length; i++)
                            {
                                codeWriter.WriteLine(metadataLines[i]);
                            }
                        }
                    }
                    // Replace #fn with filename
                    else if (sampleLine.Contains("#structName"))
                    {
                        generatedCode = sampleLine.Replace("#structName", "\"" + metadataLines[0] + "\"");
                        codeWriter.WriteLine(generatedCode);
                    }
                    else if (sampleLine.Contains("#structData"))
                    {
                        for (int i = 1; i < metadataLines.Length; i++)
                        {
                            if (metadataLines[i].Contains("["))
                            {
                                int bracketStart = metadataLines[i].IndexOf("[");
                                int bracketEnd = metadataLines[i].IndexOf("]");
                                string length = metadataLines[i].Substring(bracketStart + 1, bracketEnd - bracketStart - 1);

                                codeWriter.WriteLine(outputTemplates[0].Replace("#length", length));
                                codeWriter.WriteLine(outputTemplates[1].Replace("#length", length));

                                int commentStart = metadataLines[i].IndexOf("//");
                                string fieldName = metadataLines[i].Substring(commentStart + 2);

                                codeWriter.WriteLine(outputTemplates[2].Replace("#field", fieldName));
                                codeWriter.WriteLine(outputTemplates[3].Replace("#columnIndex", (i - 1).ToString()));
                            }
                            else if (metadataLines[i].Contains("double") || metadataLines[i].Contains("int"))
                            {
                                int commentStart = metadataLines[i].IndexOf("//");
                                string fieldName = metadataLines[i].Substring(commentStart + 2);

                                codeWriter.WriteLine(outputTemplates[4]
                                    .Replace("#field", fieldName)
                                    .Replace("#columnIndex", (i - 1).ToString()));
                            }
                        }
                    }
                    // Just write the line
                    else
                    {
                        codeWriter.WriteLine(sampleLine);
                    }
                }
            }
        }

    }
}
