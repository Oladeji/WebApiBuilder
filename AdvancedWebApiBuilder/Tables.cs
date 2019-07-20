using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBAttribLib;
using System.Reflection;
using System.Data;

namespace AdvancedWebApiBuilder
{
 public partial class MainForm
    {
        private string GenerateBasicCreateTblQuery()
        {
            String sb2 = "";

            Type tp = (Type)listBox1.SelectedItem;

            if (tp.IsClass)
            {
                DataTableAttribute[] dataTable = (DataTableAttribute[])tp.GetCustomAttributes(typeof(DataTableAttribute), true);

                if (dataTable.Length > 0)
                {

                    sb2 = ParseClassForCreate(tp, dataTable[0].TableName);

                }
            }
            return sb2;
        }



        private string ParseClassForCreate(Type type, string tblname)
        {
            string s = " \n ";
            FieldInfo[] fieldproperties = type.GetFields();
            // gets the other fields
            if (fieldproperties.Length > 0)
            {
                string s2 = "";
                s = "Create Table  " + tblname + " ( \n ";
                foreach (FieldInfo field in fieldproperties)
                {


                    s = s + ParsefieldtForCreateTblPurpose(field);

                    s2 = s2 + ParsefieldtForCreateTblKeyPurpose(field);

                }
                if (s2.TrimEnd() != "") { s2 = "primary key ( " + s2.Remove(s2.Length - 1, 1) + " ) \n"; }
                s = s + s2 + " );\n";
            }
            return s;
        }
        private string ParsefieldtForCreateTblKeyPurpose(FieldInfo thefield)
        {
            string s = "";
            DataFieldAttribute[] field = (DataFieldAttribute[])thefield.GetCustomAttributes(typeof(DataFieldAttribute), true);
            if (field.Length > 0)
            {
                if ((field[0] is DataFieldAttribute))
                {


                    if (field[0].IsKey)
                    {

                        s = thefield.Name.Remove(0, 2) + ",";
                    }
                }

            }

            return s;
        }

        private string ParsefieldtForCreateTblPurpose(FieldInfo thefield)
        {
            string s = "";
            int thecase = 0;
            DataFieldAttribute[] field = (DataFieldAttribute[])thefield.GetCustomAttributes(typeof(DataFieldAttribute), true);
            if (field.Length > 0)
            {
                if ((field[0] is DataFieldAttribute))
                {

                    switch (field[0].Type)
                    {


                        case DbType.AnsiString:
                        case DbType.AnsiStringFixedLength:
                        case DbType.String:
                        case DbType.StringFixedLength:
                            if ((field[0].Size >0)&&(field[0].Size < 255))
                            {
                                  s = s + thefield.Name.Remove(0, 2) + "  " + ReturnSqlType(field[0].Type) + "(" + field[0].Size + ") ";
                            }
                            else if  (field[0].Size > 255)
                            {
                                s = s + thefield.Name.Remove(0, 2) + "  " + ReturnSqlType(field[0].Type) + "(MAX) ";
                            }
                            else 
                            {
                                s = s + thefield.Name.Remove(0, 2) + "  " + ReturnSqlType(field[0].Type) + "(1) ";
                            }
                            thecase = 1;// meaning stringdefaultvalue
                            break;
                        case DbType.Binary:
                           
                            s = s + thefield.Name.Remove(0, 2) + "  " + ReturnSqlType(field[0].Type) + "(" + ReturnSize(field[0].Size) + ") ";
                            break;
                        case DbType.Boolean:
                              s = s + thefield.Name.Remove(0, 2) + "  " + ReturnSqlType(field[0].Type);
                            thecase = 6;
                            break;
                        case DbType.Byte: 
                              s = s + thefield.Name.Remove(0, 2) + "  " + ReturnSqlType(field[0].Type);
                            thecase = 7;
                            break;
                        case DbType.Currency: break;
                        case DbType.Date:
                        case DbType.DateTime:
                            s = s + thefield.Name.Remove(0, 2) + "  " + ReturnSqlType(field[0].Type);
                            thecase = 4;
                            break;
                        case DbType.DateTime2: break;
                        case DbType.DateTimeOffset: break;
                        case DbType.Decimal:
                            s = s + thefield.Name.Remove(0, 2) + "  " + ReturnSqlType(field[0].Type) + "(" + field[0].Size + "," + field[0].Size2 + ") ";
                            thecase = 2; // meaning decimal defaultvalue
                            break;
                        case DbType.Double:
                            s = s + thefield.Name.Remove(0, 2) + "  " + ReturnSqlType(field[0].Type) + "(" + field[0].Size + "," + field[0].Size2 + ") ";
                            thecase = 8; // meaning decimal defaultvalue
                            break;
                        
                            break;
                        case DbType.Guid:
                            s = s + thefield.Name.Remove(0, 2) + "  " + ReturnSqlType(field[0].Type) + " ";
                            thecase = 5; // meaning guid default values default newid()
                            break;
                        case DbType.Int16:
                        case DbType.Int32:
                        case DbType.Int64:
                            {
                                if (field[0].Isidentity)
                                {
                                    s = s + thefield.Name.Remove(0, 2) + "  " + ReturnSqlType(field[0].Type) + " IDENTITY(" + field[0].Size + "," + field[0].Size2 + ") "; 
                                }
                                else
                                    s = s + thefield.Name.Remove(0, 2) + "  " + ReturnSqlType(field[0].Type) + " ";
                                thecase = 3; // meaning interger defaultvalue
                                
                                break;
                            }
                        case DbType.Object: break;
                        case DbType.SByte: break;
                        case DbType.Single: break;
                        case DbType.Time: break;
                        case DbType.UInt16: break;
                        case DbType.UInt32: break;
                        case DbType.UInt64: break;
                        case DbType.VarNumeric: break;
                        case DbType.Xml: break;
                        default: break;

                    }// end switch
                    if (!field[0].IsNull)
                    {
                        s = s + " Not Null";
                        if (thecase == 1)
                        {  if (field[0].hasDefaultValue) s = s + " default '" + field[0].DefaultStringValue+"'";} else
                        if (thecase == 2)
                         { if (field[0].hasDefaultValue) s = s + " default " + field[0].DefaultFloatValue + ""; } else
                            if (thecase == 6)
                            {
                                if (field[0].hasDefaultValue)
                                {
                                    if( (field[0].DefaultStringValue.Trim().ToUpper()=="TRUE")||(field[0].DefaultStringValue.Trim().ToUpper()=="FALSE"))
                                    {
                                        s = s + " default '" + field[0].DefaultStringValue + "'";    
                                    } else 
                                    throw new Exception("DEFAULT VALUES FOR BOOLEAN MUST EITHER BE TRUE OR FALSE");
                                }
                            }
                            else
                                    if (thecase == 8)
                                    { if (field[0].hasDefaultValue) s = s + " default " + field[0].DefaultFloatValue + ""; }
                                    else
                      
                        if (thecase == 3)
                         { if (field[0].hasDefaultValue) s = s + " default " + field[0].DefaultIntValue + ""; } else
                        if (thecase == 4)
                        { if (field[0].hasDefaultValue) s = s + " default '01-01-1900'"; }
                        if (thecase == 5)
                        { if (field[0].hasDefaultValue) s = s + " default newid() "; }
                    }
                    s = s + ", \n";
                }

            }

            return s;
        }
        //private string GenerateClasstblKeyandtypeSupplyMatch(Type type)
        //{
        //    string s2 = " ";
        //    FieldInfo[] fieldproperties = type.GetFields();
        //    // gets the other fields
        //    if (fieldproperties.Length > 0)
        //    {
        //        string s = "";

        //        foreach (FieldInfo field in fieldproperties)
        //        {


        //            s = ReturnfieldtIfLabelledKey(field);
        //            if (s != "")
        //            {
        //                s2 = s2 + field.FieldType.Name.ToString() + " " + s + "IMPOSEDKEY,";


        //            }


        //        }
        //        // we try to return blank if not field and delete the last comma padded if s contains field
        //        if (s2.TrimEnd() != "") {
        //                     s2 = " " + s2.Remove(s2.Length - 1, 1) + "  "; }

        //    }
        //    return s2;
        //}
        private string GenerateClasstblKeyandtype(Type type)
        {
            string s2 = " ";
            FieldInfo[] fieldproperties = type.GetFields();
            // gets the other fields
            if (fieldproperties.Length > 0)
            {
                string s = "";

                foreach (FieldInfo field in fieldproperties)
                {


                    s = ReturnfieldtIfLabelledKey(field);
                    if (s != "")
                    {
                        s2 = s2 + field.FieldType.Name.ToString() + " " + s + ",";


                    }


                }
                // we try to return blank if not field and delete the last comma padded if s contains field
                if (s2.TrimEnd() != "") { s2 = " " + s2.Remove(s2.Length - 1, 1) + "  "; }

            }
            return s2;
        }
    }
}
