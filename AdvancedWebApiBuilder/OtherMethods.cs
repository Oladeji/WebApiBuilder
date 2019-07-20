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
		private string CreateWhereclause4Unique(Type type, string KeyOROwner, string Nature)
		{

			string str = "";

			FieldInfo[] fieldproperties = type.GetFields();

			if (fieldproperties.Length > 0)
			{
				string str3 = ""; string str33 = "";
				//string col = ""; string colval = "";


				foreach (FieldInfo field in fieldproperties)
				{
					DataFieldAttribute[] fieldattrib = (DataFieldAttribute[])field.GetCustomAttributes(typeof(DataFieldAttribute), true);
					if (fieldattrib.Length > 0)
					{
						if ((KeyOROwner == "KEY") && (Nature == "FULL"))
						{
							if (fieldattrib[0].IsKey)
							{
								// str3 = str3 + " ( " + field.Name.Remove(0, 2) + " ='\" +" + field.Name.Remove(0, 2) + "+ \"') and ";
								str3 = str3 + " ( " + field.Name.Remove(0, 2) + " =@" + field.Name.Remove(0, 2) + ") and ";
								str33 = str33 + "new ParameterValue( \"@" + field.Name.Remove(0, 2) + "\" ," + ReturnDbType(fieldattrib[0].Type) + ", " + field.Name.Remove(0, 2) + ") ,";


							}
						}
						else
							if ((KeyOROwner == "KEY") && (Nature == "PARTIAL"))
						{
							if (fieldattrib[0].IsKey)
							{
								// str3 = str3 + " ( " + field.Name.Remove(0, 2) + " ='\" +" + field.Name.Remove(0, 2) + "+ \"') and ";
								str3 = str3 + " ( " + field.Name.Remove(0, 2) + "  LIKE @" + field.Name.Remove(0, 2) + ") and ";
								str33 = str33 + "new ParameterValue( \"@" + field.Name.Remove(0, 2) + "\" ," + ReturnDbType(fieldattrib[0].Type) + ",'%' + " + field.Name.Remove(0, 2) + "+'%') ,";


							}
						}
						else
								if ((KeyOROwner == "OWNER") && (Nature == "FULL"))
						{
							if (fieldattrib[0].IsDataOwner)
							{
								// str3 = str3 + " ( " + field.Name.Remove(0, 2) + " ='\" +" + field.Name.Remove(0, 2) + "+ \"') and ";
								str3 = str3 + " ( " + field.Name.Remove(0, 2) + " =@" + field.Name.Remove(0, 2) + ") and ";
								str33 = str33 + "new ParameterValue( \"@" + field.Name.Remove(0, 2) + "\" ," + ReturnDbType(fieldattrib[0].Type) + ", " + field.Name.Remove(0, 2) + ") ,";

							}

						}
						else
									if ((KeyOROwner == "OWNER") && (Nature == "PARTIAL"))
						{
							if (fieldattrib[0].IsDataOwner)
							{
								str3 = str3 + " ( " + field.Name.Remove(0, 2) + " like  @" + field.Name.Remove(0, 2) + " ) and ";
								str33 = str33 + "new ParameterValue( \"@" + field.Name.Remove(0, 2) + "\" ," + ReturnDbType(fieldattrib[0].Type) + ",'%' + " + field.Name.Remove(0, 2) + "+'%') ,";

							}

						}
					}
				}
				if (str3.Trim().Length > 2)
				{
					str3 = str3.Remove(str3.Length - 4, 4) + " \"";
					str33 = str33.Remove(str33.Length - 2, 2) + " ";
					str = "  where " + str3 + ", new ParameterValue[] { " + str33 + " }  ";
				}
				// else str3 = "";
				else str = " \",BigGDLLib.ParameterValue.EmptyParas";








				//


			}
			return str;

		}
		private string CreateConditionForItemInList(Type type)
		{
			string str3 = "";
			FieldInfo[] fieldproperties = type.GetFields();
			if (fieldproperties.Length > 0)
			{

				foreach (FieldInfo field in fieldproperties)
				{
					DataFieldAttribute[] fieldattrib = (DataFieldAttribute[])field.GetCustomAttributes(typeof(DataFieldAttribute), true);
					if (fieldattrib.Length > 0)
					{
						if (fieldattrib[0].IsKey)
						{
							// str3 = str3 + " ( My" + type.Name + "." + field.Name.Remove(0, 2) + ".ToString() == this[i]." + field.Name.Remove(0, 2) + ".ToString() ) &&";

							str3 = str3 + " ( " + field.Name.Remove(0, 2) + ".ToString().Trim().ToUpper() == this[i]." + field.Name.Remove(0, 2) + ".ToString().Trim().ToUpper() ) &&";
						}

					}
				}
				if (str3.Trim().Length > 2)
				{
					str3 = str3.Remove(str3.Length - 2, 2);

				}
				// else str3 = "";
				else str3 = " ";

			}
			return str3;

		}
		private string CreateWhereclause(Type type, string Param, bool IncludedSubQuery)
		{

			string str = "";

			FieldInfo[] fieldproperties = type.GetFields();

			if (fieldproperties.Length > 0)
			{
				string str3 = ""; string str33 = "";

				foreach (FieldInfo field in fieldproperties)
				{
					DataFieldAttribute[] fieldattrib = (DataFieldAttribute[])field.GetCustomAttributes(typeof(DataFieldAttribute), true);
					if (fieldattrib.Length > 0)
					{
						if (fieldattrib[0].IsPartofmasterfield)
						{

							str3 = str3 + " ( " + field.Name.Remove(0, 2) + " =@" + field.Name.Remove(0, 2) + ") and ";
							str33 = str33 + "new ParameterValue( \"@" + field.Name.Remove(0, 2) + "\" ," + ReturnDbType(fieldattrib[0].Type) + ", " + Param + "." + field.Name.Remove(0, 2) + ") ,";


						}
					}
				}
				if (str3.Trim().Length > 2)
				{
					if (IncludedSubQuery)
					{
						str3 = str3.Remove(str3.Length - 4, 4) + " \"+ SubQuery ";
					}
					else
						str3 = str3.Remove(str3.Length - 4, 4) + " \"";


					str33 = str33.Remove(str33.Length - 2, 2) + " ";
					str = "  where " + str3 + ", new ParameterValue[] { " + str33 + " }  ";

				}

				else str = " \",BigGDLLib.ParameterValue.EmptyParas";






			}


			return str;


		}

		private string ReturnSize(int p)
		{
			if (p != -1)
			{
				return p.ToString();
			}
			else return "Max";
		}

		/// <summary>
		/// This will generate a where clause for a select statement 
		/// KeyOROwner is the string we pass to know if the where should use only fields labbelled as key or all that are identified 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="KeyOROwner"></param>
		/// <returns></returns>
		/// Nature is to bring in  the idea of like ie wild character in seaching so we have partial or full

	}
}
