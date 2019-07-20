using DBAttribLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWebApiBuilder
{
	public partial class MainForm 

	{
			/// <summary>
		///  example for factbl will shall return string camp, string prog,string fac
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>

		private string ParseGenerateUniqueSearchInListMethod(Type type)
		{

			String str = GenerateClasstblKeyandtype(type);
			if (str.Trim() != "")
			{


				str = "public  " + type.Name + " GetItemInList(" + str + ")  \n { \n  ";
				str = str + " " + type.Name + "  New" + type.Name + " = new " + type.Name + "(); \n";

				//      str = str + " for (int i =0 ; i < this.Count;++i)  \n  { if ( "+CreateConditionForItemInList(type)+")  \n { \n  New"+type.Name+" = this[i]; break ; \n }  \n }  return  New"+type.Name+";  \n } \n ";

				str = str + " for (int i =0 ; i < this.Count;++i)  \n  { if ( " + CreateConditionForItemInList(type) + ")  \n { \n  New" + type.Name + " = this[i]; break ; \n }  \n }  return  New" + type.Name + ";  \n } \n ";

			}
			return str;
		}

		private string GenerateChangeListStatez1(Type type)
		{
			string line = "/// <summary>  \n/// byte value for e which ranges from 0 to 3\n/// 0 for No change\n/// 1 for insertion\n/// 2 for update\n/// 3 for delete\n/// anything else the state of the entity is left\n/// </summary>\n/// <param name=\"i\"></param> \n/// <returns></returns>";

			return line + "\n public void ChangeListState(byte e) \n  {\n     foreach (" + type.Name + " item in this)\n item.ChangeEntitystate(e); \n } ";

		}
		private string ParseGenerateUniqueFoundInListMethod(Type type)
		{

			String str = GenerateClasstblKeyandtype(type);
			if (str.Trim() != "")
			{

				str = "public  bool  IsItemInList(" + str + ")  \n { \n  ";
				str = str + " " + " bool  found= false;  \n";


				str = str + " for (int i =0 ; i < this.Count;++i)  \n  { if ( " + CreateConditionForItemInList(type) + ")  \n { \n  found= true;  break ; \n }  \n }  return  found;  \n } \n ";

			}
			return str;
		}

		private string ParseGenerateUniquePositionInListMethod(Type type)
		{

			String str = GenerateClasstblKeyandtype(type);
			if (str.Trim() != "")
			{

				str = "public  int ItemPositionInList(" + str + ")  \n { \n  ";
				str = str + " " + "  int Pos = -1;  \n";


				str = str + " for (int i =0 ; i < this.Count;++i)  \n  { if ( " + CreateConditionForItemInList(type) + ")  \n { \n    Pos = i;  break ; \n }  \n }  return  Pos;  \n } \n ";

			}
			return str;
		}
	
		/// <summary>
		/// this will generate the fields as private and the properties as public 
		/// it  will also make sure if the field is a list then  create an instance
		/// it will enforce lenght constraint on string properties ,
		/// finally writes the call the makecopy functins
		/// </summary>
		/// <param name="field"></param>
		/// <returns></returns>
		/// <summary>
		/// Dataowner means using the basic attribute that can identify an entity e.g matricno for a student
		/// </summary>
		/// <param name="classlistname"></param>
		/// <returns></returns>
		private string ParseClasstblForOwnerAttrb(Type type)
		{
			string s2 = " ";
			FieldInfo[] fieldproperties = type.GetFields();
			// gets the other fields
			if (fieldproperties.Length > 0)
			{
				string s;

				foreach (FieldInfo field in fieldproperties)
				{

					s = "";


					DataFieldAttribute[] Newfield = (DataFieldAttribute[])field.GetCustomAttributes(typeof(DataFieldAttribute), true);
					if (Newfield.Length > 0)
					{
						if ((Newfield[0] is DataFieldAttribute))
						{


							if (Newfield[0].IsDataOwner)
							{

								s = field.Name.Remove(0, 2) + ",";
							}
						}

					}

					if (s != "")
					{
						// s2 = s2 + field.FieldType.Name.ToLower() + " " + s;
						s2 = s2 + field.FieldType.Name + " " + s;

					}


				}
				if (s2.TrimEnd() != "") { s2 = " " + s2.Remove(s2.Length - 1, 1) + " "; }

			}
			return s2;
		}

	}
}
