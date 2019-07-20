using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DBAttribLib;

namespace AdvancedWebApiBuilder
{
	public partial class MainForm
	{
		private string GenerateFullConstructor(Type type)
		{
			String str = "public " + type.Name + "(";
			String str2 = "";


			FieldInfo[] fieldproperties = type.GetFields();
			foreach (FieldInfo field in fieldproperties)
			{
				str = str + field.FieldType.Name + " " + field.Name.Remove(0, 2) + ", ";
				str2 = str2 + field.Name + " = " + field.Name.Remove(0, 2) + " ;\n";

			}
			str = str.Remove(str.Length - 2, 1) + " ) ";
			return str + "\n { \n " + str2 + "\n}";
		}
		private string GenerateFullConstructorListRemove(Type type)
		{
			bool hasalistatttribute = false;
			String str = "public " + type.Name + "(";
			String str2 = "";


			FieldInfo[] fieldproperties = type.GetFields();
			foreach (FieldInfo field in fieldproperties)
			{
				IsAListAttribute[] fields = (IsAListAttribute[])field.GetCustomAttributes(typeof(IsAListAttribute), true);
				if ((fields.Length > 0) && (fields[0] is IsAListAttribute))
				{ hasalistatttribute = true; }
				else
				{
					str = str + field.FieldType.Name + " " + field.Name.Remove(0, 2) + ", ";
					str2 = str2 + field.Name + " = " + field.Name.Remove(0, 2) + " ;\n";
				}
			}
			str = str.Remove(str.Length - 2, 1) + " ) \n { \n " + str2 + "\n}";

			if (!hasalistatttribute)
			{// the idea here is that since this particular class has no list attribute then the method GenerateFullConstructorList would have generated the same thing , so to avoid error will return notin
				str = "";
			}
			return str;
		}
		private string GenerateFullConstructorListRemoveless(Type type)
		{
			bool hasalistatttribute = false;
			String str = "public xxx_" + type.Name + "() {}\n"+ "public xxx_" + type.Name + "(";
			String str2 = "";


			FieldInfo[] fieldproperties = type.GetFields();
			foreach (FieldInfo field in fieldproperties)
			{
				IsAListAttribute[] fields = (IsAListAttribute[])field.GetCustomAttributes(typeof(IsAListAttribute), true);
				if ((fields.Length > 0) && (fields[0] is IsAListAttribute))
				{ hasalistatttribute = true; }
				else
				{
					str = str + field.FieldType.Name + " " + field.Name.Remove(0, 2) + ", ";
					str2 = str2 + field.Name + " = " + field.Name.Remove(0, 2) + " ;\n";
				}
			}
			str = str.Remove(str.Length - 2, 1) + " ) \n { \n " + str2 + "\n}";
			//for less , i am not considering list in the first place
			//if (!hasalistatttribute)
			//{// the idea here is that since this particular class has no list attribute then the method GenerateFullConstructorList would have generated the same thing , so to avoid error will return notin
			//	str = "";
			//}
			return str;
		}
		private string GenerateConvertToOriginalless(Type type)
		{
			String str =" public "+ type.Name+ " ConvertToOriginal()"   +"\n{ \n  " + type.Name +"  new" +type.Name+"= new "+ type.Name+"();\n";
		
			FieldInfo[] fieldproperties = type.GetFields();
			foreach (FieldInfo field in fieldproperties)
			{
				IsAListAttribute[] fields = (IsAListAttribute[])field.GetCustomAttributes(typeof(IsAListAttribute), true);
				if ((fields.Length > 0) && (fields[0] is IsAListAttribute))
				{
				
				}
				else
				{
					str = str + "  new" + type.Name+"."+field.Name.Remove(0, 2) + " = " + field.Name + " ;\n";
				}
			}
			str =  str + "  return new" + type.Name + " ;\n}";
			return str;
		}

		private string GenerateRevertToXXX(Type type)
		{
			String str = " public xxx_" + type.Name + " RevertToXXX(" + type.Name + "  new" + type.Name + ")" + "\n{ \n  xxx_" + type.Name + "  p" + type.Name + "= new xxx_" + type.Name + "(";

			FieldInfo[] fieldproperties = type.GetFields();
			foreach (FieldInfo field in fieldproperties)
			{
				IsAListAttribute[] fields = (IsAListAttribute[])field.GetCustomAttributes(typeof(IsAListAttribute), true);
				if ((fields.Length > 0) && (fields[0] is IsAListAttribute))
				{

				}
				else
				{
					str = str + "  new" + type.Name + "." + field.Name.Remove(0, 2) + " , ";
				}
			}
			str = str.Remove(str.Length-2) + ");\n  return p" + type.Name + " ;\n}";
			return str;
		}
		private string GenerateRevertToXXXList(Type type)
		{
			String str = " public List<xxx_" + type.Name + "> RevertToXXXList(List<" + type.Name + ">  temp)" + "\n{ \n  List<xxx_" + type.Name + ">  p" + type.Name + "= new List<xxx_" + type.Name + ">();";

			str = str + "foreach (" + type.Name + " item in temp)\n {\n		p" + type.Name + ".Add(RevertToXXX(item));	\n}";


			str = str + "\n  return p" + type.Name + " ;\n}";
			return str;
		}


		//private string GenerateFullConstructorNoConnection(Type type)
		//{
		//    String str = "public " + type.Name + "(";
		//    String str2 = "";


		//    FieldInfo[] fieldproperties = type.GetFields();
		//    foreach (FieldInfo field in fieldproperties)
		//    {
		//        str = str + field.FieldType.Name + " " + field.Name.Remove(0, 2) + ", ";
		//        str2 = str2 + field.Name + " = " + field.Name.Remove(0, 2) + " ;\n";

		//    }
		//    str = str.Remove(str.Length - 2, 1) + " , Object T) :base(\"\") ";
		//    return str + "\n { \n " + str2 + "\n}";
		//}

		/// <summary>
		/// this will generate the Empty constructor, Full Constructor , MakeCopy,and the other Copy paramiters
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private string GenerateMakeCopyandFullConstructorMethods(Type type, string Classname)
		{///
			//defaultconstructorgeneratedfromlist
			// this is used to return true from GenerateFieldandProperty if a default constructor 
			// has been generated from a list member to that we dont need to generated default construcotr again
			///
			bool defaultconstructorgeneratedfromlist , keepdefaultconstructorgeneratedfromlist = false;
			string str = "";
			FieldInfo[] fieldproperties = type.GetFields();

			// create the constructor
			str = "\n";

			//  str = str +"public " + type.Name + "(Object T):base(\"\") \n { } \n \n ";
			// create other constructor
			str = str + GenerateFullConstructor(type) + "\n \n " + GenerateFullConstructorListRemove(type) + "\n \n  ";
			//str = str + GenerateMakeCopyMethod(type) + "\n \n ";
			// gets the other fields
			foreach (FieldInfo field in fieldproperties)
			{
				str = str + GenerateFieldandProperty(field, Classname,out defaultconstructorgeneratedfromlist, keepdefaultconstructorgeneratedfromlist);
				if (defaultconstructorgeneratedfromlist)
				{
					keepdefaultconstructorgeneratedfromlist = defaultconstructorgeneratedfromlist;
				}

			}
			if (!keepdefaultconstructorgeneratedfromlist)
			{
				str = str + "\n public " + type.Name + "() \n { } \n ";
			}
			return str;
		}

		/// <summary>
		/// this will generate the Empty constructor, Full Constructor , MakeCopy,and the other Copy paramiters
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private string GenerateMakeCopyandFullConstructorMethodsless(Type type, string Classname)
		{///
			//defaultconstructorgeneratedfromlist
			// this is used to return true from GenerateFieldandProperty if a default constructor 
			// has been generated from a list member to that we dont need to generated default construcotr again
			///
			bool defaultconstructorgeneratedfromlist, keepdefaultconstructorgeneratedfromlist = false;
			string str = "";
			FieldInfo[] fieldproperties = type.GetFields();

			// create the constructor
			str = "\n";

			//  str = str +"public " + type.Name + "(Object T):base(\"\") \n { } \n \n ";
			// create other constructor
			//str = str + GenerateFullConstructor(type) + "\n \n " + GenerateFullConstructorListRemove(type) + "\n \n  ";
			str = str + "\n \n " + GenerateFullConstructorListRemoveless(type) + "\n"+ GenerateConvertToOriginalless(type) + " \n  " + GenerateRevertToXXX( type) + " \n  " + GenerateRevertToXXXList(type) +" \n  ";

			//str = str + GenerateMakeCopyMethod(type) + "\n \n ";
			// gets the other fields
			foreach (FieldInfo field in fieldproperties)
			{
				str = str + GenerateFieldandPropertyless(field,  out defaultconstructorgeneratedfromlist, keepdefaultconstructorgeneratedfromlist);
				if (defaultconstructorgeneratedfromlist)
				{
					keepdefaultconstructorgeneratedfromlist = defaultconstructorgeneratedfromlist;
				}

			}
			//if (!keepdefaultconstructorgeneratedfromlist)
			//{
			//	str = str + "\n public xxx_" + type.Name + "() \n { } \n ";
			//}
			return str;
		}


		private string GenerateCreateConstructor(Type type)
		{

			return " \n public static " + type.Name + "  Create() \n { \n   " + type.Name + "  New" + type.Name + " = new " + type.Name + "\n { \n }; \n   return New" + type.Name + "; \n }";

		}
		private string GenerateCreateConstructorless(Type type)
		{

			return " \n public static xxx_" + type.Name + "  Create() \n { \n   xxx_" + type.Name + "  Newxxx_" + type.Name + " = new xxx_" + type.Name + "\n { \n }; \n   return Newxxx_" + type.Name + "; \n }";

		}
		
		private string GenerateCreateConstructorwtList(Type type, string ListIsAdescendantofwhichclass)
		{
			String str = "";
			if (ListIsAdescendantofwhichclass != null)
			{
				str = "public  static " + type.Name + " Create (" + ListIsAdescendantofwhichclass + " New" + ListIsAdescendantofwhichclass + " ) \n  ";

				String str2 = "";
				string s = " \n return  New" + type.Name + ";\n \n ";

				FieldInfo[] fieldproperties = type.GetFields();
				foreach (FieldInfo field in fieldproperties)
				{
					DataFieldAttribute[] Afield = (DataFieldAttribute[])field.GetCustomAttributes(typeof(DataFieldAttribute), true);

					if (Afield.Length > 0)
					{
						// note 
						if (Afield[0].IsPartofmasterfield)
						{
							str2 = str2 + " New" + type.Name + "." + field.Name.Remove(0, 2) + " = " + " New" + ListIsAdescendantofwhichclass + "." + field.Name.Remove(0, 2) + " ;\n";
						}

					}
				}


				str = str + "\n { \n " + type.Name + " New" + type.Name + " = new " + type.Name + "();\n  " + str2 + s + "\n}";
			}
			else
				str = " ";
			return str;
		}
	}
}
