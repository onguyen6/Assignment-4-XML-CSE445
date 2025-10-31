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
                // A new XML reader settings with validation
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                
                // Add the XSD schema to the settings
                settings.Schemas.Add(null, xsdUrl);
                
                // making a validation event handler to throw exceptions on errors
                settings.ValidationEventHandler += (sender, e) =>
                {
                    throw new XmlSchemaValidationException($"Validation error: {e.Message} at line {e.Exception.LineNumber}, position {e.Exception.LinePosition}");
                };

                // Read and validate the entire XML doc
                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                    while (reader.Read()) 
                    { 
                        // Read through entire XML doc
                    }
                }

                return "No errors are found";
            }
            catch (XmlSchemaValidationException ex)
            {
                return ex.Message;
            }
            catch (XmlException ex)
            {
                return $"XML Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }

            //return "No Error" if XML is valid. Otherwise, return the desired exception message.
        }

        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                // starting the load of the XML document from the URL 
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlUrl);

                // converting XML to JSON by Newtonsoft.Json
                string jsonText = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.Indented);

                // The returned jsonText needs to be de-serializable by Newtonsoft.Json package
                return jsonText;
            }
            // otherise exception
            catch (Exception ex)
            {
                return $"Error converting XML to JSON: {ex.Message}";
            }

            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            return jsonText;

        }
    }

}
