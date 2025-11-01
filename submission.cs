using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;

// new import 
using System.Text;
using System.Collections.Generic;

// CSE 445 onguyen6 FALL 2025

/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "https://raw.githubusercontent.com/onguyen6/Assignment-4-XML-CSE445/refs/heads/main/Hotels.xml";
        public static string xmlErrorURL = "https://raw.githubusercontent.com/onguyen6/Assignment-4-XML-CSE445/refs/heads/main/HotelsErrors.xml";
        public static string xsdURL = "https://raw.githubusercontent.com/onguyen6/Assignment-4-XML-CSE445/refs/heads/main/Hotels.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;

                // Load schema properly using XmlSchema.Read
                using (XmlReader schemaReader = XmlReader.Create(xsdUrl))
                {
                    XmlSchema schema = XmlSchema.Read(schemaReader, null);
                    settings.Schemas.Add(schema);
                }

                // Collect errors 
                StringBuilder errors = new StringBuilder();
                bool hasErrors = false;

                settings.ValidationEventHandler += (sender, e) =>
                {
                    hasErrors = true;
                    errors.AppendLine($"{e.Message} at line {e.Exception.LineNumber}, position {e.Exception.LinePosition}");
                };

                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                while (reader.Read()) 
                { 
                    // Read through document
                }
                }

                return hasErrors ? errors.ToString().Trim() : "No errors are found";
            }
            catch (Exception ex)
            {
                return $"Validation Error: {ex.Message}";
            }
        }
            //return "No Error" if XML is valid. Otherwise, return the desired exception message.


        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlUrl);

                //formatting.None for compact JSON
                string jsonText = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.None);
        
                // verify it can be deserialized back (as required by assignment)
                JsonConvert.DeserializeXmlNode(jsonText);
        
                return jsonText;
            }
            catch (Exception ex)
            {
                return $"Error converting XML to JSON: {ex.Message}";
            }
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            //return jsonText;
        }
    }
}

