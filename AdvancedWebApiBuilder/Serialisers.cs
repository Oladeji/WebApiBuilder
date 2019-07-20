using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AdvancedWebApiBuilder
{
    public partial class MainForm
    {
        private string GenerateDeSerialiserCodes(Type type)
        {
            String str = "\n[Serializable()] \n public partial  class " + type.Name + " : ISerializable \n {  \n public " + type.Name + "(  SerializationInfo info, StreamingContext ctxt)  \n {";
            String str2 = "";
            string str3 = "";// str3 will write the GetObjectData method

            FieldInfo[] fieldproperties = type.GetFields();
            foreach (FieldInfo field in fieldproperties)
            {
                // str = str + field.FieldType.Name + " " + field.Name.Remove(0, 2) + ", ";
                str3 = str3 + "   info.AddValue(\"" + field.Name.ToUpper() + "\", this." + field.Name + "); \n";
                str2 = str2 + "   this." + field.Name + " = (" + field.FieldType.Name + ")info.GetValue(\"" + field.Name.ToUpper() + "\" , typeof(" + field.FieldType.Name + "));\n";

            }
            str3 = "\n public void GetObjectData(SerializationInfo info, StreamingContext ctxt) \n { " + str3 + "\n }";
            return str + "\n " + str2 + "\n}\n" + str3 + " \n ";
        }

        private string GenerateSerialiserCodes(Type tp)
        {
            string str = "";
            str = " [Serializable()] \npublic class " + tp.Name + "Serializer  :NewBigGDll.SerializeDeserialise\n {\n  public " + tp.Name + "Serializer() \n {\n  }\n   }//end of serialiser class \n";
            //1 public void SerializeObject(string filename," + tp.Name + "ToSerialize objectToSerialize)\n  {";
            //2 str = str + "\n  Stream stream = File.Open(filename, FileMode.Create);\n  BinaryFormatter bFormatter = new BinaryFormatter(); \n  bFormatter.Serialize(stream, objectToSerialize);  \n  stream.Close(); \n  }\n ";
            //3str = str +" public "+tp.Name+"ToSerialize DeSerializeObject(string filename) \n   {\n    "+tp.Name+"ToSerialize objectToSerialize; \n    Stream stream = File.Open(filename, FileMode.Open);";

            //4 str = str +"BinaryFormatter bFormatter = new BinaryFormatter(); \n  objectToSerialize = ("+tp.Name +"ToSerialize)bFormatter.Deserialize(stream); \n  stream.Close(); \n  return objectToSerialize;\n  }\n  

            return str;

        }

        private string GenerateSerialisationCodes(Type type)
        {
            string str = "";



            str = " [Serializable()]  \n public class " + type.Name + "ToSerialize : ISerializable  \n  { \npublic " + type.Name + "ToSerialize() \n{\n}\n  public  " + type.Name + "List" + " AList ; \n  public " + type.Name + "ToSerialize (SerializationInfo info, StreamingContext ctxt) \n { \n   this.AList =  (" + type.Name + "List)info.GetValue(\"" + type.Name.ToUpper() + "LIST\",typeof(" + type.Name + "List));\n} ";
            str = str + "\n  public void GetObjectData(SerializationInfo info, StreamingContext ctxt) \n    {        info.AddValue(\"" + type.Name.ToUpper() + "LIST\", this.AList);\n    }\n }\n";



            return str;
        }

    }
}
