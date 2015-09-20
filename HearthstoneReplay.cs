using System;
using System.Xml;
using System.Xml.Schema;

namespace HSCSReader {
	class HearthstoneReplay {
		private String FilePath = String.Empty;

		public HearthstoneReplay(String filePath) {
			FilePath = filePath;
			if (Validate()) {
				Parse();
			}
		}

		private void Parse() {
			
		}

		private Boolean Validate() {
			// TODO: Fix this so that validation actually does something. Maybe after jleclanche finishes the XML spec and DTD.
			//// Set the validation settings.
			//XmlReaderSettings validationSettings = new XmlReaderSettings();
			//validationSettings.DtdProcessing = DtdProcessing.Parse;
			//validationSettings.ValidationType = ValidationType.DTD;
			//validationSettings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
			//
			//// Create the XmlReader object.
			//XmlReader reader = XmlReader.Create(FilePath, validationSettings);
			//
			//// Parse the file. 
			//while (reader.Read());
			//Console.WriteLine("Validation finished");
			return true;
		}

		//private static void ValidationCallBack(object sender, ValidationEventArgs e) {
		//	Console.WriteLine("Validation Error: {0}", e.Message);
		//}
	}
}
