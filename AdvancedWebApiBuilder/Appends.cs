using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AdvancedWebApiBuilder
{
    public partial class MainForm
    {
        //public string GetInfo(string path, string Value)
        //{
        //    XmlReader rdr = XmlReader.Create(path);
        //    string lib = "";
        //    while (rdr.Read())
        //    {
        //        //  if (rdr.MoveToContent() == XmlNodeType.Element && rdr.Name == "Campus")
        //        if (rdr.NodeType == XmlNodeType.Element)
        //        {
        //            if (rdr.Name ==Value )
        //            {
        //                lib = rdr.ReadElementString();
        //            }
        //        }//else rdr.Read();  
        //    }
        //    return lib;
        //}
        private string AppendClassStater(string classname, string thenamespace)
        {
            return "using System; \nusing System.Collections.Generic; \nusing System.Linq;\nusing System.Text; \nusing System.Data.Common;\n  \n\nnamespace  " + thenamespace + " \n { \n   [Serializable()] \n public  partial class  " + classname + " : IEntity  \n {";
        }
        private string AppendClassStaterWithoutSeriAttr(string classname, string thenamespace)
        {
			//     return "using System; \nusing System.Collections.Generic; \nusing System.Linq;\nusing System.Text; \nusing System.Data.Common;\n  \nusing BigGDLLib;\nnamespace  " + thenamespace + " \n { \n    public  partial class  " + classname + " : IEntity  \n {";
			return "using System; \nusing System.Collections.ObjectModel; \nusing System.ComponentModel.DataAnnotations;\n\nnamespace  " + thenamespace + " \n { \n    public  partial class  " + classname + " : IEntity  \n {";

		}
		private string AppendClassStaterWithoutSeriAttrless(string classname, string thenamespace)
		{
			//		return "using System; \nusing System.Collections.ObjectModel;\nusing System.Collections.Generic; \nusing System.ComponentModel.DataAnnotations;\nusing " + Library_Name_Space.Text + ".Model.Models;\n" + "\nnamespace  " + thenamespace + " \n { \n    public   class  xxx_" + classname + "  \n {";
			return "using System; \nusing System.Collections.ObjectModel;\nusing System.Collections.Generic; \nusing " + Library_Name_Space.Text + ".Model.Models;\n" + "\nnamespace  " + thenamespace + " \n { \n    public   class  xxx_" + classname + "  \n {";

		}
		private string AppendClassStaterWebApi(string classname, string thenamespace)
        {
            return "using System; " +
				 // "\nusing System.Collections.Generic;" +
				 "\nusing System.Collections.ObjectModel;" +
				 "\nusing System.ComponentModel.DataAnnotations; " +
                // "\nusing System.Data.Common;\n" +
             //   "\nnamespace  " + thenamespace + " \n { \n public  class  " + classname + "  \n{" + " \n    public    int  " + classname+ "Id   { get; set; } " + " \n ";
			"\nnamespace  " + thenamespace + " \n { \n public  class  " + classname + ": IEntity  \n{" + " \n ";

		}
		private string AppendClassListStater(string classname, string Classlistname, string thenamespace)
        {
            return " using System; \n using System.Collections.Generic; \n using System.Linq;\n  using System.Text; \n using System.Data.Common;\n    \n namespace  " + thenamespace + ".Model.Models \n {  \n[Serializable()] \n   public partial class  " + Classlistname + " :List<" + classname + ">  \n { \n ";
        }
        private string AppendClassListStaterWithoutSeriAttr(string classname, string Classlistname, string thenamespace)
        {
            return " using System; \n using System.Collections.Generic; \n using System.Linq;\n  using System.Text; \n using System.Data.Common;\n    \nnamespace  " + thenamespace + ".Model.Models \n {  \npublic partial class  " + Classlistname + " : List<" + classname + ">  \n { \n ";
        }
        private string AppendSerialClassStater(string classname, string thenamespace)
        {
            return "using System; \nusing System.Collections.Generic;\nusing System.IO;\nusing System.Runtime.Serialization.Formatters.Binary;    \nusing System.Linq;\nusing System.Text; \nusing System.Data.Common;\nusing System.Runtime.Serialization;  \nnamespace  " + thenamespace + ".Model.Models \n {";
        }

        private string AppendWebApiFinishing()
        {
            return " }  \n} ";
        }
        private string AppendFinishing()
        {
            return " }  \n    } ";
        }
    }
}
