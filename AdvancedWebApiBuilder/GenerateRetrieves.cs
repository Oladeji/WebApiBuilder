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
		private string GenerateBasicRetrieveMethodForClassList(out string classlistname)
		{
			String sb2 = "";
			classlistname = "";
			Type tp = (Type)listBox1.SelectedItem;

			if (tp.IsClass)
			{
				DataTableAttribute[] dataTable = (DataTableAttribute[])tp.GetCustomAttributes(typeof(DataTableAttribute), true);

				if (dataTable.Length > 0)
				{
					classlistname = dataTable[0].ClassListName;

					sb2 = ParseGenerateUniqueSearchInListMethod(tp) + ParseGenerateUniqueFoundInListMethod(tp) + ParseGenerateUniquePositionInListMethod(tp);
				}
			}
			return sb2;
		}

		private string GenerateFieldandProperty(FieldInfo field, string Classname, out bool defaultconstructorgeneratedfromlist, bool notifyifdefaultalreadygenylistmember)
		{
			defaultconstructorgeneratedfromlist = false;
			string str = "";
			IsAListAttribute[] fields = (IsAListAttribute[])field.GetCustomAttributes(typeof(IsAListAttribute), true);

			if ((fields.Length > 0) && (fields[0] is IsAListAttribute))
			{
				str = str + " private  " + field.FieldType.Name + " " + field.Name + " = new " + field.FieldType.Name + "(); \n ";

				// keep str = str + "public    " + field.FieldType.Name + " " + field.Name.Remove(0, 2) + "\n { get  { \n return " + field.Name + "; }   \n   set { \n " + field.Name + "  = value; \n }\n} \n \n";
				str = str + " private    ObservableCollection<" + field.FieldType.Name.Replace("List", "") + "> my" + field.FieldType.Name.Replace("List", "") + "=   new  ObservableCollection<" + field.FieldType.Name.Replace("List", "") + "> ();\n";
				//			str = str + " public virtual  ObservableCollection<" + field.FieldType.Name.Replace("List", "") + "> " + field.FieldType.Name.Replace("List", "") + " { get { if (! " + field.FieldType.Name + "Bool &&" + field.Name + ".Count == 0) \n	foreach (var item in my" + field.FieldType.Name.Replace("List", "") + ")		{ "+ field.Name + ".Add(item); " + field.FieldType.Name + "Bool = true; \n} \n	return my" + field.FieldType.Name.Replace("List", "") + "; }\n set  { my" + field.FieldType.Name.Replace("List", "") + "= value; }\n }\n";
				str = str + " public virtual  ObservableCollection<" + field.FieldType.Name.Replace("List", "") + "> " + field.FieldType.Name.Replace("List", "") + " { get { my"+ field.FieldType.Name + ".Clear(); \n	foreach (var item in my" + field.FieldType.Name.Replace("List", "") + ")		{ " + field.Name + ".Add(item);  \n} \n	return my" + field.FieldType.Name.Replace("List", "") + "; }\n set  { my" + field.FieldType.Name.Replace("List", "") + "= value; }\n }\n";

				if (notifyifdefaultalreadygenylistmember)  //meaning that a former list attribute has generate a default constructor then 
														   // write out what the user must add to that constructor
														   // notifyifdefaultalreadygenylistmember was used instead of defaultconstructorgeneratedfromlist becase the method would have reset defaultconstructorgeneratedfromlist to false on entering
					str = str + "// PLEASE ! add this init  to the default construct named " + Classname + "()  ==>" + field.FieldType.Name.Replace("List", "") + " = new  ObservableCollection<" + field.FieldType.Name.Replace("List", "") + ">() ;     \n";

				else
					// a default constructor has been generated from the above so defaultconstructorgeneratedfromlist will be changed to true;
					str = str + " public " + Classname + "() \n {\n" + field.FieldType.Name.Replace("List", "") + " = new  ObservableCollection<" + field.FieldType.Name.Replace("List", "") + ">() ;\n     }";

				defaultconstructorgeneratedfromlist = true; // this will always be true as long as this is a list
			}
			else
			{





				str = str + " private  " + field.FieldType.Name + " " + field.Name + "; \n ";
				if (field.FieldType.Name.ToLower() == "string")
				{
					DataFieldAttribute[] pfield = (DataFieldAttribute[])field.GetCustomAttributes(typeof(DataFieldAttribute), true);
					if (pfield[0].Size > 0)
					{
						str = str + "[MaxLength(" + pfield[0].Size + ")]\n ";
					}
					str = str + "public    " + field.FieldType.Name + " " + field.Name.Remove(0, 2) + "\n { get { return " +
										field.Name + " ;} \n   set { \n  \n " + field.Name + "  = value; \n \n }\n} \n \n";

					// { get { return " +                          field.Name + " ;} \n   set { \n   if (value!= null )       { \n if (value.Length >" + pfield[0].Size + ")  \n   {\n   throw new Exception(\"The current lenght of  " + field.Name + " Is more than  Acceptable \");     }   \n}    \n    if (!Copied) this.Copy = this.MakeCopy();\n        if ( " + field.Name + " != value) \n {           string propertyName=\"" + field.Name.Remove(0, 2) + "\";\n             " + field.Name + "  = value; \n            this.SetEntityState(EntityStateType.Modified, propertyName);\n }\n }\n} \n \n";

				}
				else
				{
					str = str + "public    " + field.FieldType.Name + " " + field.Name.Remove(0, 2) + "\n { get { return " +
										 field.Name + " ;} \n   set { \n  \n " + field.Name + "  = value; \n \n }\n} \n \n";

					// { get { return " +                     field.Name + " ;} \n   set { \n        if (!Copied) this.Copy = this.MakeCopy();\n        if ( " + field.Name + " != value) \n {           string propertyName=\"" + field.Name.Remove(0, 2) + "\";\n             " + field.Name + "  = value; \n            this.SetEntityState(EntityStateType.Modified, propertyName);\n }\n }\n} \n \n";
				}

			}
			return str;
		}
		private string GenerateFieldandPropertyless(FieldInfo field,  out bool defaultconstructorgeneratedfromlist, bool notifyifdefaultalreadygenylistmember)
		{
			defaultconstructorgeneratedfromlist = false;
			string str = "";
			IsAListAttribute[] fields = (IsAListAttribute[])field.GetCustomAttributes(typeof(IsAListAttribute), true);

			if ((fields.Length > 0) && (fields[0] is IsAListAttribute))
			{
				//str = str + " private  " + field.FieldType.Name + " " + field.Name + " = new " + field.FieldType.Name + "(); \n ";

				//// keep str = str + "public    " + field.FieldType.Name + " " + field.Name.Remove(0, 2) + "\n { get  { \n return " + field.Name + "; }   \n   set { \n " + field.Name + "  = value; \n }\n} \n \n";
				//str = str + " private    ObservableCollection<" + field.FieldType.Name.Replace("List", "") + "> my" + field.FieldType.Name.Replace("List", "") + "=   new  ObservableCollection<" + field.FieldType.Name.Replace("List", "") + "> ();\n";
				////			str = str + " public virtual  ObservableCollection<" + field.FieldType.Name.Replace("List", "") + "> " + field.FieldType.Name.Replace("List", "") + " { get { if (! " + field.FieldType.Name + "Bool &&" + field.Name + ".Count == 0) \n	foreach (var item in my" + field.FieldType.Name.Replace("List", "") + ")		{ "+ field.Name + ".Add(item); " + field.FieldType.Name + "Bool = true; \n} \n	return my" + field.FieldType.Name.Replace("List", "") + "; }\n set  { my" + field.FieldType.Name.Replace("List", "") + "= value; }\n }\n";
				//str = str + " public virtual  ObservableCollection<" + field.FieldType.Name.Replace("List", "") + "> " + field.FieldType.Name.Replace("List", "") + " { get { my" + field.FieldType.Name + ".Clear(); \n	foreach (var item in my" + field.FieldType.Name.Replace("List", "") + ")		{ " + field.Name + ".Add(item);  \n} \n	return my" + field.FieldType.Name.Replace("List", "") + "; }\n set  { my" + field.FieldType.Name.Replace("List", "") + "= value; }\n }\n";

				//if (notifyifdefaultalreadygenylistmember)  //meaning that a former list attribute has generate a default constructor then 
				//										   // write out what the user must add to that constructor
				//										   // notifyifdefaultalreadygenylistmember was used instead of defaultconstructorgeneratedfromlist becase the method would have reset defaultconstructorgeneratedfromlist to false on entering
				//	str = str + "// PLEASE ! add this init  to the default construct named " + Classname + "()  ==>" + field.FieldType.Name.Replace("List", "") + " = new  ObservableCollection<" + field.FieldType.Name.Replace("List", "") + ">() ;     \n";

				//else
				//	// a default constructor has been generated from the above so defaultconstructorgeneratedfromlist will be changed to true;
				//	str = str + " public " + Classname + "() \n {\n" + field.FieldType.Name.Replace("List", "") + " = new  ObservableCollection<" + field.FieldType.Name.Replace("List", "") + ">() ;\n     }";

				//defaultconstructorgeneratedfromlist = true; // this will always be true as long as this is a list
			}
			else
			{





				//str = str + " private  " + field.FieldType.Name + " " + field.Name + "; \n ";
				str = str + " public  " + field.FieldType.Name + " " + field.Name + "; \n ";

				//hide properties
				//if (field.FieldType.Name.ToLower() == "string")
				//{
				//	DataFieldAttribute[] pfield = (DataFieldAttribute[])field.GetCustomAttributes(typeof(DataFieldAttribute), true);
				//	if (pfield[0].Size > 0)
				//	{
				//		str = str + "[MaxLength(" + pfield[0].Size + ")]\n ";
				//	}
				//	str = str + "public    " + field.FieldType.Name + " " + field.Name.Remove(0, 2) + "\n { get { return " +
				//						field.Name + " ;} \n   set { \n  \n " + field.Name + "  = value; \n \n }\n} \n \n";

				//	// { get { return " +                          field.Name + " ;} \n   set { \n   if (value!= null )       { \n if (value.Length >" + pfield[0].Size + ")  \n   {\n   throw new Exception(\"The current lenght of  " + field.Name + " Is more than  Acceptable \");     }   \n}    \n    if (!Copied) this.Copy = this.MakeCopy();\n        if ( " + field.Name + " != value) \n {           string propertyName=\"" + field.Name.Remove(0, 2) + "\";\n             " + field.Name + "  = value; \n            this.SetEntityState(EntityStateType.Modified, propertyName);\n }\n }\n} \n \n";

				//}
				//else
				//{
				//	str = str + "public    " + field.FieldType.Name + " " + field.Name.Remove(0, 2) + "\n { get { return " +
				//						 field.Name + " ;} \n   set { \n  \n " + field.Name + "  = value; \n \n }\n} \n \n";

				//	// { get { return " +                     field.Name + " ;} \n   set { \n        if (!Copied) this.Copy = this.MakeCopy();\n        if ( " + field.Name + " != value) \n {           string propertyName=\"" + field.Name.Remove(0, 2) + "\";\n             " + field.Name + "  = value; \n            this.SetEntityState(EntityStateType.Modified, propertyName);\n }\n }\n} \n \n";
				//}

			}
			return str;
		}



		private string GenerateBasicPropertiesandMethods(out string classname)
		{

			String sb2 = "";
			classname = "";
			Type tp = (Type)listBox1.SelectedItem;

			if (tp.IsClass)
			{
				DataTableAttribute[] dataTable = (DataTableAttribute[])tp.GetCustomAttributes(typeof(DataTableAttribute), true);

				if (dataTable.Length > 0)
				{
					classname = tp.Name;
					sb2 = GenerateCreateConstructorwtList(tp, dataTable[0].ListIsAdescendantofwhichclass) + GenerateMakeCopyandFullConstructorMethods(tp, classname) + GenerateCreateConstructor(tp);
				}
			}
			return sb2;// +sb.Remove(sb.Length - 2, 1).ToString() + sb1.Remove(sb1.Length - 2, 1).ToString();

		}


		private string GenerateBasicPropertiesandMethodsless(out string classname)
		{

			String sb2 = "";
			classname = "";
			Type tp = (Type)listBox1.SelectedItem;

			if (tp.IsClass)
			{
				DataTableAttribute[] dataTable = (DataTableAttribute[])tp.GetCustomAttributes(typeof(DataTableAttribute), true);

				if (dataTable.Length > 0)
				{
					classname =  tp.Name;
					//sb2 = GenerateCreateConstructorwtList(tp, dataTable[0].ListIsAdescendantofwhichclass) + GenerateMakeCopyandFullConstructorMethods(tp, classname) + GenerateCreateConstructor(tp);
					sb2 = GenerateMakeCopyandFullConstructorMethodsless(tp, classname);// + GenerateCreateConstructorless(tp);

				}
			}
			return sb2;// +sb.Remove(sb.Length - 2, 1).ToString() + sb1.Remove(sb1.Length - 2, 1).ToString();

		}

		private string GenerateConvertToOriginalless( string classname)
		{

			String sb2 = "";
			classname = "";
			Type tp = (Type)listBox1.SelectedItem;

			if (tp.IsClass)
			{
				DataTableAttribute[] dataTable = (DataTableAttribute[])tp.GetCustomAttributes(typeof(DataTableAttribute), true);

				if (dataTable.Length > 0)
				{
					classname = tp.Name;
					
					sb2 = GenerateMakeCopyandFullConstructorMethodsless(tp, classname);// + GenerateCreateConstructorless(tp);

				}
			}
			return sb2;// +sb.Remove(sb.Length - 2, 1).ToString() + sb1.Remove(sb1.Length - 2, 1).ToString();

		}



	}
}
