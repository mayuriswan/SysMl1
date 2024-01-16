using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SysML_Sync
{
    public class ExtenderService
    {
        /// <summary>
        /// This Method searches all Elements, that the umlMainFile does not contain,
        /// but the umlSubFile contains.
        /// </summary>
        /// <param name="umlMainFile"></param>
        /// <param name="umlSubFile"></param>
        /// <returns>A List with all the unique Elements from the umlSubFile</returns>
        public static IEnumerable<XElement> GetElementsToAdd(XDocument MainFile, XDocument SubFile, XNamespace xmi)
        {
            var MainIds = MainFile.Descendants().Attributes(xmi + "id").Select(attr => attr.Value);
            var SubIds = SubFile.Descendants().Attributes(xmi + "id").Select(attr => attr.Value);

            var uniqueIds = SubIds.Except(MainIds);

            var elementsToAdd = SubFile.Descendants().Where(element => uniqueIds.Contains(element.Attribute(xmi + "id")?.Value ?? ""));

            return elementsToAdd;
        }

        /// <summary>
        /// This Method finds the proper Location wehere the Element should be put
        /// and adds them to this location. (For the Uml-File)
        /// </summary>
        /// <param name="umlMainFile"></param>
        /// <param name="umlSubFile"></param>
        /// <param name="elementToAdd"></param>
        /// <param name="xmi"></param>
        static void AddElementToProperLocationUml(XDocument umlMainFile, XDocument umlSubFile, XElement elementToAdd, XNamespace xmi)
        {
            var elementId = elementToAdd.Attribute(xmi + "id").Value;
            var elementInSubFile = umlSubFile.Descendants().FirstOrDefault(element => (string)element.Attribute(xmi + "id") == elementId);
            var parent = elementInSubFile.Parent;

            var parentId = parent.Attribute(xmi + "id").Value;
            var properLocation = umlMainFile.Descendants().FirstOrDefault(element => (string)element.Attribute(xmi + "id") == parentId);

            properLocation.Add(elementToAdd);
        }

        /// <summary>
        /// This Method adds all the unique Elements from the umlSubFile
        /// to the umlMainFile.
        /// </summary>
        /// <param name="umlMainFile"></param>
        /// <param name="umlSubFile"></param>
        /// <param name="xmi"></param>
        /// <returns>The updated Uml-File</returns>
        public static XDocument MergeUmlFile(XDocument umlMainFile, XDocument umlSubFile, XNamespace xmi)
        {
            var elementsToAdd = GetElementsToAdd(umlMainFile, umlSubFile, xmi);

            foreach (var element in elementsToAdd)
            {
                AddElementToProperLocationUml(umlMainFile, umlSubFile, element, xmi);
            }
            return umlMainFile;
        }

        /// <summary>
        /// This Method finds the proper Location wehere the Element should be put
        /// and adds them to this location. (For the Notation-File)
        /// </summary>
        /// <param name="notationMainFile"></param>
        /// <param name="notationSubFile"></param>
        /// <param name="elementToAdd"></param>
        /// <param name="xmi"></param>
        static void AddElementToProperLocationNotation(XDocument notationMainFile, XDocument notationSubFile, XElement elementToAdd, XNamespace xmi)
        {
            var elementId = elementToAdd.Attribute(xmi + "id").Value;
            var elementInSubFile = notationSubFile.Descendants().FirstOrDefault(element => (string)element.Attribute(xmi + "id") == elementId);
            var parent = elementInSubFile.Parent;

            if(parent == notationSubFile.Root)
            {
                // Do nothing
            }
            else if(parent == notationSubFile.Descendants().FirstOrDefault(e => e.Name.LocalName == "Diagram" && e.Attribute(xmi + "id")?.Value == parent.Attribute(xmi + "id").Value))
            {
                notationMainFile.Root.Add(parent);
            }
            else
            {
                var parentId = parent.Attribute(xmi + "id").Value;
                var properLocation = notationMainFile.Descendants().FirstOrDefault(element => (string)element.Attribute(xmi + "id") == parentId);
                properLocation.Add(elementToAdd);
            }
        }

        /// <summary>
        /// This Method adds all the unique Elements from the notationSubFile
        /// to the notationMainFile.
        /// </summary>
        /// <param name="notationMainFile"></param>
        /// <param name="notationSubFile"></param>
        /// <param name="xmi"></param>
        /// <returns></returns>
        public static XDocument MergeNotationFile(XDocument notationMainFile, XDocument notationSubFile, XNamespace xmi)
        {
            var elementsToAdd = GetElementsToAdd(notationMainFile, notationSubFile, xmi);

            foreach (var element in elementsToAdd)
            {
                AddElementToProperLocationNotation(notationMainFile, notationSubFile, element, xmi);
            }
            return notationMainFile;
        }
    }
}