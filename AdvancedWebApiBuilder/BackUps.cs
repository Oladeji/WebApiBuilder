using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBAttribLib;
using System.Reflection;

namespace AdvancedWebApiBuilder
{
    public partial class MainForm
    {

         private string GenerateBasicBackUps(string classname)
         {
             String sb2 = "";

             Type tp = (Type)listBox1.SelectedItem;

             if (tp.IsClass)
             {
                 DataTableAttribute[] dataTable = (DataTableAttribute[])tp.GetCustomAttributes(typeof(DataTableAttribute), true);

                 if (dataTable.Length > 0)
                 {

  
            
                //  sb2 =  "\n public void BackUp" + classname + "(string Filename, string Query)\n {  \n   " + classname + "List A" + classname + " = " + classname + "List.Retrieve(Query);\n    " + classname + "ToSerialize objectToSerialize = new " + classname + "ToSerialize(); \n";
                //  sb2 = sb2 + classname + "Serializer serializer = new " + classname + "Serializer();\n  objectToSerialize.AList = A" + classname + ";\n serializer.SerializeObject<" + classname + "ToSerialize>(Filename+\"" + classname + ".dj\", objectToSerialize);\n}\n";

                  sb2 = sb2 + "public void BackUp" + classname + "Alist (string Filename, " + classname + "List A" + classname + ")\n {\n    " + classname + "ToSerialize objectToSerialize = new " + classname + "ToSerialize(); \n";
                  sb2 = sb2 + classname + "Serializer serializer = new " + classname + "Serializer();\n  objectToSerialize.AList = A" + classname + ";\n serializer.SerializeObject<" + classname + "ToSerialize>(Filename+\"" + classname + "Alist.dj\", objectToSerialize);\n}\n";


                 // sb2 = sb2 + "public void BackUp" + classname + "(string Filename,string AppendToName , string Query)\n {  \n   " + classname + "List A" + classname + " = " + classname + "List.Retrieve(Query);\n    " + classname + "ToSerialize objectToSerialize = new " + classname + "ToSerialize(); \n";
                 // sb2 = sb2 + classname + "Serializer serializer = new " + classname + "Serializer();\n  objectToSerialize.AList = A" + classname + ";\n serializer.SerializeObject<" + classname + "ToSerialize>(Filename+AppendToName+\"" + classname + ".dj\", objectToSerialize);\n}\n";

                  sb2 = sb2 + "public void BackUp" + classname + "Alist (string Filename,string AppendToName, " + classname + "List A" + classname + ")\n {\n    " + classname + "ToSerialize objectToSerialize = new " + classname + "ToSerialize(); \n";
                  sb2 = sb2 + classname + "Serializer serializer = new " + classname + "Serializer();\n  objectToSerialize.AList = A" + classname + ";\n serializer.SerializeObject<" + classname + "ToSerialize>(Filename+AppendToName+\"" + classname + "Alist.dj\", objectToSerialize);\n}\n";
          




                 }

              }
               return sb2;
             }

         private string GenerateRestore(string classname)
         {
             String sb2 = "";

             Type tp = (Type)listBox1.SelectedItem;

             if (tp.IsClass)
             {
                 DataTableAttribute[] dataTable = (DataTableAttribute[])tp.GetCustomAttributes(typeof(DataTableAttribute), true);

                 if (dataTable.Length > 0)
                 {


                     sb2 = "public  " + classname + "List RestoreBackUp" + classname + "List(string Filename)\n {  \n   " + classname + "Serializer Serializer  = new " + classname + "Serializer();\n   " + classname + "ToSerialize obj = new " + classname + "ToSerialize(); \n";
                     sb2 = sb2 + "   obj = Serializer.DeSerializeObject <" + classname + "ToSerialize>(Filename);\n   return obj.AList;\n \n}";
           
            
                 }

              }
               return sb2;
             }

         private string GenerateBasicAllBackUpOnce(string classname)
         {
             String sb2 = "";

             Type tp = (Type)listBox1.SelectedItem;

             if (tp.IsClass)
             {
                 DataTableAttribute[] dataTable = (DataTableAttribute[])tp.GetCustomAttributes(typeof(DataTableAttribute), true);

                 if (dataTable.Length > 0)
                 {


					//  sb2 = classname + "List A" + classname + " = " + classname + "List.Retrieve();\n    " + classname + "ToSerialize " + classname + "objectToSerialize = new " + classname + "ToSerialize(); \n    ";
					sb2 =" \n    " + classname + "ToSerialize " + classname + "objectToSerialize = new " + classname + "ToSerialize(); \n    ";

					sb2 = "    " + sb2 + classname + "Serializer " + classname + "serializer = new " + classname + "Serializer();\n    " + classname + "objectToSerialize.AList = (" + classname + "List )mylist;\n    " + classname + "serializer.SerializeObject<" + classname + "ToSerialize>(Filename+\"" + classname + ".dj\", " + classname + "objectToSerialize);\n ";


                 }

             }
             return sb2;
         }

         }

    }
