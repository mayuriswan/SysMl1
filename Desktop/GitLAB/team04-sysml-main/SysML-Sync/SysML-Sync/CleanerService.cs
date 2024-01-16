using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SysML_Sync
{
    public class CleanerService
    {
        /// <summary>
        /// This Method returns the name of the whole Project.
        /// </summary>
        /// <param name="umlFile"></param>
        /// <returns>The name of the Project.</returns>
        public static string GetFilename(XDocument umlFile)
        {
            XElement modelElement = umlFile.Descendants().FirstOrDefault(e => e.Name.LocalName == "Model");

            return modelElement.Attribute("name").Value;
        }

        /// <summary>
        /// This Method searches all Descendants in the xmlFile, 
        /// which are from the type "uml:Package" and saves them in a List.
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="xmi"></param>
        /// <returns>A List of all XElements which are from the type uml:Package</returns>
        public static IEnumerable<XElement> FindPackagesUmlFile(XDocument umlFile, XNamespace xmi)
        {
            var packages = umlFile.Descendants().Where(x => (string)x.Attribute(xmi + "type") == "uml:Package");
            return packages;
        }

        /// <summary>
        /// This Method creates a Cleaner and adds all packages which were found
        /// in the "FindPackagesUmlFile" Method.
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="xmi"></param>
        /// <returns>A Cleaner, that has all packages from the UML-File</returns>
        public static Cleaner CreateCleanerOptionsForCheckbox(XDocument umlFile, XNamespace xmi)
        {
            var packages = FindPackagesUmlFile(umlFile, xmi);

            Dictionary<string, string> content = new Dictionary<string, string>();
            Cleaner cleaner = new Cleaner(content);

            foreach (var package in packages)
            {
                string id = package.Attribute(xmi + "id").Value;
                string name = package.Attribute("name").Value;
                cleaner.Content.Add(id, name);
            }
            return cleaner;
        }

        /// <summary>
        /// This Method creates the Cleaner, which you selected via the Checkbox.
        /// </summary>
        /// <param name="cleanerOptions"></param>
        /// <returns>A Cleaner</returns>
        public static Cleaner CreateUmlCleaner(Cleaner cleanerOptions)
        {
            return null;
        }

        /// <summary>
        /// This Method searches all the relevant cleaners for the Notation-File,
        /// on the basis of the umlCleaner.
        /// </summary>
        /// <param name="umlFile"></param>
        /// <param name="xmi"></param>
        /// <param name="umlCleaner"></param>
        /// <returns>A Cleaner</returns>
        public static Cleaner CreateNotationCleaner(XDocument umlFile, XNamespace xmi, Cleaner umlCleaner)
        {
            IEnumerable<XElement> packagesSupplier = null;
            IEnumerable<XElement> packagesDependency = null;

            Dictionary<string, string> content = new Dictionary<string, string>();
            Cleaner notationCleaner = new Cleaner(content);

            for(int i = 0; i < umlCleaner.Content.Count; i++)
            {
                packagesSupplier = umlFile.Descendants("packagedElement").Where(element => (string)element.Attribute("supplier") == umlCleaner.Content.ElementAt(i).Key);
                
                foreach (var package in packagesSupplier)
                {
                    string id = package.Attribute(xmi + "id").Value;
                    string name = "No Name";
                    
                    bool CleanerHasId = notationCleaner.Content.ContainsKey(id);
                    
                    if(!CleanerHasId)
                    {
                        notationCleaner.Content.Add(id, name);
                    }
                }

                packagesDependency = umlFile.Descendants("packagedElement").Where(element => (string)element.Attribute(xmi + "id") == umlCleaner.Content.ElementAt(i).Key);
                packagesDependency = packagesDependency.Descendants("packagedElement").Where(element => (string)element.Attribute(xmi + "type") == "uml:Dependency");

                foreach (var package in packagesDependency)
                {
                    string id = package.Attribute(xmi + "id").Value;
                    string name = "No Name";
                    
                    bool CleanerHasId = notationCleaner.Content.ContainsKey(id);
                    
                    if(!CleanerHasId)
                    {
                        notationCleaner.Content.Add(id, name);
                    }
                }
            }
            return notationCleaner;
        }

        /// <summary>
        /// This Method deletes all packagedElements in the umlFile,
        /// which were selected to be cleaned through the cleaner.
        /// </summary>
        /// <param name="umlFile"></param>
        /// <param name="cleaner"></param>
        /// <param name="xmi"></param>
        /// <returns>A cleaned uml File</returns>
        public static XDocument CleanUmlFile(XDocument umlFile, Cleaner cleaner, XNamespace xmi)
        {
            for (int i = 0; i < cleaner.Content.Count; i++)
            {
                var queryPackage = umlFile.Descendants("packagedElement").Where(element => (string)element.Attribute(xmi + "id") == cleaner.Content.ElementAt(i).Key);
                queryPackage.Remove();

                var queryDependency = umlFile.Descendants("packagedElement").Where(element => (string)element.Attribute("supplier") == cleaner.Content.ElementAt(i).Key);
                queryDependency.Remove();
            }
            return umlFile;
        }

        /// <summary>
        /// This Method deletes all packagedElements in the notationFile,
        /// which were selected to be cleaned through the cleaner.
        /// </summary>
        /// <param name="notationFile"></param>
        /// <param name="cleaner"></param>
        /// <returns>A cleaned notation File</returns>
        public static XDocument CleanNotationFile(XDocument notationFile, Cleaner umlCleaner, Cleaner notationCleaner, string filename)
        {
            for (int i = 0; i < umlCleaner.Content.Count; i++)
            {
                var queryUmlCleaner = notationFile.Descendants("children").Where(child => child.Element("element")?.Attribute("href")?.Value == $"{filename}.uml#{umlCleaner.Content.ElementAt(i).Key}").FirstOrDefault();
                queryUmlCleaner.Remove();
            }

            for (int i = 0; i < notationCleaner.Content.Count; i++)
            {
            var queryNotationCleaner = notationFile.Descendants("edges").Where(element => element.Element("element")?.Attribute("href")?.Value == $"{filename}.uml#{notationCleaner.Content.ElementAt(i).Key}").FirstOrDefault();
            queryNotationCleaner.Remove();
            }
            return notationFile;
        }
    }
}