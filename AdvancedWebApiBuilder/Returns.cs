using DBAttribLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWebApiBuilder
{
	public partial class MainForm
	{
		private string ReturnDatatypeandField(FieldInfo field, string classname)
		{
			string str = "";
			IsAListAttribute[] fields = (IsAListAttribute[])field.GetCustomAttributes(typeof(IsAListAttribute), true);

			if (field.FieldType.Name.ToLower() == ("string"))
			{
				//old  str = "(" + field.FieldType.Name + ")rd[\"" + field.Name.Remove(0, 2) + "\"] , ";
				str = " rd[\"" + field.Name.Remove(0, 2) + "\"].ToString().Trim() , ";
				//  sample of old NewCandidates.Astatus = (String)rd["Astatus"];
				// sample of new NewCandidates.Astatus = rd["Astatus"].ToString(); 

			}
			else
				if (field.FieldType.Name.ToLower() == ("boolean"))
			{
				//old  str = "(" + field.FieldType.Name + ")rd[\"" + field.Name.Remove(0, 2) + "\"] , ";
				//  str = " rd[\"" + field.Name.Remove(0, 2) + "\"].ToString().Trim() , ";
				str = " rd.GetBoolean (rd.GetOrdinal (\"" + field.Name.Remove(0, 2) + "\")) , ";

				//  sample of old NewCandidates.Astatus = (String)rd["Astatus"];
				// sample of new NewCandidates.Astatus = rd["Astatus"].ToString(); 

			}
			else
				if (field.FieldType.Name.ToLower() == ("decimal"))
			{
				str = "decimal.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";

			}
			else
					if (field.FieldType.Name.ToLower() == ("datetime"))
			{
				//   (rd["CreateDate"].ToString().Trim() == "") ? DateTime.Now: DateTime.Parse(rd["CreateDate"].ToString())
				//    str = " DateTime.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";
				str = " rd[\"" + field.Name.Remove(0, 2) + "\"].ToString().Trim()==\"\"  ? DateTime.Now.Date:  DateTime.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";


			}

			else
						if ((field.FieldType.Name.ToLower() == ("int")) || (field.FieldType.Name.ToLower() == ("int32")) || (field.FieldType.Name.ToLower() == ("int64")))
			{
				str = "int.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";

			}

			else
							if ((field.FieldType.Name.ToLower() == ("int16")))
			{
				str = "short.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";

			}
			else
								if ((field.FieldType.Name.ToLower() == ("guid")))
			{

				str = "Guid.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";

			}
			else
									if ((field.FieldType.Name.ToLower() == ("byte[]")))
			{

				str = "(byte[]) ( rd[\"" + field.Name.Remove(0, 2) + "\"]) , ";

			}

			else if ((fields.Length > 0) && (fields[0] is IsAListAttribute))
			{

				// str = "int.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";
				if (classname.Trim() == "")
				{
					//  MessageBox.Show("Wait");
					// MessageBox.Show("Wait");
				}
				str = field.Name.Remove(0, 2) + ".Retrieve ( New" + classname + "), ";

				// FacList.Retrieves(Camps )
				//   str = "Pls get the datatype =>" + field.FieldType.Name + "  and include it in ReturnDatatypeandFieldNew in ProjectMaker ";
			}
			else { str = "ReturnDatatypeandFieldNew :field.FieldType.Name Problem"; throw new Exception(str); }
			//   Facultys.Add(new Faculty((string)rd["Name"], (string)rd["ParentInstitutionInstitutionName"], int.Parse(rd["LastDepartmentCode"].ToString()), (byte[])rd["Security"], dt));

			//  str2 = str2 + "(" + field.FieldType.Name + ")rd[\"" + field.Name.Remove(0, 2) + "\"] , ";
			//str2 = str2 + ReturnDatatypeandFieldNew(field); 
			return str;
		}
		private string ReturnDatatypeandFieldNew(FieldInfo field, string classname)
		{
			string str = "";
			IsAListAttribute[] fields = (IsAListAttribute[])field.GetCustomAttributes(typeof(IsAListAttribute), true);

			if (field.FieldType.Name.ToLower() == ("string"))
			{
				//old  str = "(" + field.FieldType.Name + ")rd[\"" + field.Name.Remove(0, 2) + "\"] , ";
				//old2    str = " rd[\"" + field.Name.Remove(0, 2) + "\"].ToString().Trim() , ";
				str = "New" + classname + "." + field.Name.Remove(0, 2) + " , ";
				//  sample of old NewCandidates.Astatus = (String)rd["Astatus"];
				// sample of new NewCandidates.Astatus = rd["Astatus"].ToString(); 
				// sample of latest now gives  NewCandidates.Astatus =Candidates.Astatus ; 

			}
			else
				if (field.FieldType.Name.ToLower() == ("boolean"))
			{
				//old  str = "(" + field.FieldType.Name + ")rd[\"" + field.Name.Remove(0, 2) + "\"] , ";
				//old2    str = " rd[\"" + field.Name.Remove(0, 2) + "\"].ToString().Trim() , ";
				str = "New" + classname + "." + field.Name.Remove(0, 2) + " , ";
				//  sample of old NewCandidates.Astatus = (String)rd["Astatus"];
				// sample of new NewCandidates.Astatus = rd["Astatus"].ToString(); 
				// sample of latest now gives  NewCandidates.Astatus =Candidates.Astatus ; 

			}
			else
				if (field.FieldType.Name.ToLower() == ("decimal"))
			{
				// str = "decimal.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";
				str = "New" + classname + "." + field.Name.Remove(0, 2) + " , ";

			}
			else
					if (field.FieldType.Name.ToLower() == ("datetime"))
			{
				//   (rd["CreateDate"].ToString().Trim() == "") ? DateTime.Now: DateTime.Parse(rd["CreateDate"].ToString())
				//    str = " DateTime.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";
				// str = " rd[\"" + field.Name.Remove(0, 2) + "\"].ToString().Trim()==\"\"  ? DateTime.Now.Date:  DateTime.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";
				str = "New" + classname + "." + field.Name.Remove(0, 2) + " , ";

			}

			else
						if ((field.FieldType.Name.ToLower() == ("int")) || (field.FieldType.Name.ToLower() == ("int32")) || (field.FieldType.Name.ToLower() == ("int64")))
			{
				//str = "int.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";
				str = "New" + classname + "." + field.Name.Remove(0, 2) + " , ";
			}

			else
							if ((field.FieldType.Name.ToLower() == ("int16")))
			{
				// str = "short.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";
				str = "New" + classname + "." + field.Name.Remove(0, 2) + " , ";
			}
			else
								if ((field.FieldType.Name.ToLower() == ("guid")))
			{

				//  str = "Guid.Parse ( rd[\"" + field.Name.Remove(0, 2) + "\"].ToString()) , ";
				str = "New" + classname + "." + field.Name.Remove(0, 2) + " , ";

			}
			else
									if ((field.FieldType.Name.ToLower() == ("byte[]")))
			{

				// str = "(byte[]) ( rd[\"" + field.Name.Remove(0, 2) + "\"]) , ";
				str = "New" + classname + "." + field.Name.Remove(0, 2) + " , ";

			}


			else if ((fields.Length > 0) && (fields[0] is IsAListAttribute))
			{


				//if (classname.Trim() == "")
				//{

				//}
				//str = field.Name.Remove(0, 2) + ".Retrieve ( New" + classname + "), ";

				// FacList.Retrieves(Camps )
				//   str = "Pls get the datatype =>" + field.FieldType.Name + "  and include it in ReturnDatatypeandFieldNew in ProjectMaker ";
			}
			else { str = "ReturnDatatypeandFieldNew :field.FieldType.Name Problem"; throw new Exception(str); }
			//   Facultys.Add(new Faculty((string)rd["Name"], (string)rd["ParentInstitutionInstitutionName"], int.Parse(rd["LastDepartmentCode"].ToString()), (byte[])rd["Security"], dt));

			//  str2 = str2 + "(" + field.FieldType.Name + ")rd[\"" + field.Name.Remove(0, 2) + "\"] , ";
			//str2 = str2 + ReturnDatatypeandFieldNew(field); 
			return str;
		}

		/// <summary>
		/// this will return a field if it is marked as key with dataattrib or " " if not a key
		/// </summary>
		/// <param name="thefield"></param>
		/// <returns></returns>
		private string ReturnfieldtIfLabelledKey(FieldInfo thefield)
		{
			string s = "";
			DataFieldAttribute[] field = (DataFieldAttribute[])thefield.GetCustomAttributes(typeof(DataFieldAttribute), true);
			if (field.Length > 0)
			{
				if ((field[0] is DataFieldAttribute))
				{


					if (field[0].IsKey)
					{
						/// remove 2 xters from start is to remove my from the field name e.g myCamp will returm Camp
						//  s = thefield.Name.Remove(0, 2) + ","; //note in the old this coma is there but relocate to the callling function in new
						// so add +"," to ParseClassForCreate and ParsefieldtForCreateTblKeyPurpose -- named as so in 2008 version
						s = thefield.Name.Remove(0, 2);
					}
				}

			}

			return s;
		}
		public string ReturnSqlType(DbType type)
		{
			string str = "";
			switch (type)
			{

				case DbType.AnsiString:
					str = "VarChar";
					break;
				case DbType.AnsiStringFixedLength:
					break;
				case DbType.Binary:
					str = "VarBinary";
					break;
				case DbType.Boolean:
					str = "BIT";
					break;
				case DbType.Byte:
					str = "Byte";
					break;
				case DbType.Currency:
					break;

				case DbType.Date:
					break;
				case DbType.DateTime:
					str = "Datetime";
					break;
				case DbType.DateTime2:
					break;
				case DbType.DateTimeOffset:
					break;
				case DbType.Decimal:
					str = "decimal";
					break;
				case DbType.Double:
					str = "float";
					break;
				case DbType.Guid:
					str = "uniqueidentifier";
					break;

				case DbType.Int16:
					str = "int";
					break;
				case DbType.Int32:
					str = "int";
					break;
				case DbType.Int64:
					str = "bigint";
					break;
				case DbType.Object:
					break;
				case DbType.SByte:
					break;
				case DbType.Single:
					break;
				case DbType.String:
					str = "VarChar";
					break;
				case DbType.StringFixedLength:
					str = "Char";
					break;
				case DbType.Time:
					break;
				case DbType.UInt16:
					break;
				case DbType.UInt32:
					break;
				case DbType.UInt64:
					break;
				case DbType.VarNumeric:
					break;
				case DbType.Xml:
					str = "Xml";
					break;
				default:
					break;
			}
			return str;
		}
		public string ReturnDbType(DbType type)
		{
			string str = "";
			switch (type)
			{
				case DbType.AnsiString:
				case DbType.AnsiStringFixedLength:
				case DbType.String:
				case DbType.StringFixedLength:

					str = "System.Data.DbType.String";
					break;
				case DbType.Int16:
				case DbType.Int32:
				case DbType.Int64:
					str = "System.Data.DbType.Int64";
					break;

				case DbType.Date:
				case DbType.DateTime:
				case DbType.DateTime2:
				case DbType.DateTimeOffset:
					str = "System.Data.DbType.DateTime";
					break;
				case DbType.Decimal:

				case DbType.Double:
					str = "System.Data.DbType.Double";
					break;
				case DbType.Binary:
					str = "System.Data.DbType.Binary";
					break;
				case DbType.Boolean:
					str = "System.Data.DbType.Boolean";
					break;
				case DbType.Byte:
					str = "System.Data.DbType.Byte";
					break;
				case DbType.Currency:
					str = "System.Data.DbType.Currency";
					break;


				case DbType.Guid:
					str = "System.Data.DbType.Guid";
					break;
				//case DbType.Binary :
				//    str = "System.Data.DbType.Binary";
				//    break;


				case DbType.Object:
					break;
				case DbType.SByte:
					break;
				case DbType.Single:
					break;

				case DbType.Time:
					break;
				case DbType.UInt16:
					break;
				case DbType.UInt32:
					break;
				case DbType.UInt64:
					break;
				case DbType.VarNumeric:
					break;
				case DbType.Xml:
					str = "Xml";
					break;
				default:
					break;
			}
			return str;
		}

	}
}
