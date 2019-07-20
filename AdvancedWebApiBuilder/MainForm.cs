using DBAttribLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AdvancedWebApiBuilder
{
	public partial class MainForm : Form
	{
	//	bool ranonce = false;
		//  int Times = 0;
		int nooftimes = 0; int nooftimesb = 0; int nooftimesc = 0;
		int nooftimesbackp = 0;
	//	string webapilibname = "";
		private bool firsttime = true;
		public MainForm()
		{
			InitializeComponent();
		}
		public string GetInfo(string path, string Value)
		{
			XmlReader rdr = XmlReader.Create(path);
			string lib = "";
			while (rdr.Read())
			{
				//  if (rdr.MoveToContent() == XmlNodeType.Element && rdr.Name == "Campus")
				if (rdr.NodeType == XmlNodeType.Element)
				{
					if (rdr.Name == Value)
					{
						lib = rdr.ReadElementString();
					}
				}//else rdr.Read();  
			}
			return lib;
		}
		private void button1_Click(object sender, EventArgs e)
		{

			Type[] tp;

				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{

					ModeldllPath.Text = openFileDialog1.FileName;

					try
					{
					Library_Name_Space.Text = GetInfo(Path.GetDirectoryName(openFileDialog1.FileName) + ("\\GenInfo.xml"), "LibraryName");
					FolderLocation.Text = GetInfo(Path.GetDirectoryName(openFileDialog1.FileName) + ("\\GenInfo.xml"), "OutPutDir");
						//webapilibname = GetInfo(Path.GetDirectoryName(openFileDialog1.FileName) + ("\\GenInfo.xml"), "Library_Name_Space");
					}
					catch (Exception)
					{

						MessageBox.Show("Please include the library name file");

					}

					try
					{
						tp = Assembly.LoadFile(openFileDialog1.FileName).GetTypes();
						foreach (Type item in tp)
						{
							if (item.IsClass)
							{

								DataTableAttribute[] dataTable = (DataTableAttribute[])item.GetCustomAttributes(typeof(DataTableAttribute), true);

								if (dataTable.Length > 0)
								{
									listBox1.Items.Add(item);

								}
								else
								{
									
									NonDbAttribute[] data2 = (NonDbAttribute[])item.GetCustomAttributes(typeof(NonDbAttribute), true);

									if (data2.Length > 0)
									{
										listBox1.Items.Add(item);

									}
								}


							}
						}
						// listBox1..DataSource = Assembly.LoadFile(openFileDialog1.FileName).GetTypes();
					}
					catch (Exception ee)
					{

						MessageBox.Show("Problem with File" + ee.ToString());

					}

				}
			}


private void button2_Click(object sender, EventArgs e)
		   {

			 nooftimes = 0;  nooftimesb = 0;  nooftimesc = 0;
			nooftimesbackp = 0;
					 firsttime = true;
			for (int i = 0; i < listBox1.Items.Count; i++)
			{
				listBox1.SelectedIndex = i;
			}
		  }

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

			richTextBox14.Clear();
			richTextBox15.Clear();
			richTextBox19.Clear();

			string thenamespace = Library_Name_Space.Text;
			if (listBox1.SelectedItem != null)
			{

				if (!Directory.Exists(FolderLocation.Text + "\\Def"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Def");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\Serialize"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Serialize");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\Models"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Models");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\Modelsless"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Modelsless");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\Dev"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Dev");
				}

				if (!Directory.Exists(FolderLocation.Text + "\\AutoController"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\AutoController");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\Repository"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Repository");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\IService"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\IService");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\Service"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Service");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\Others"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Others");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\Core"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Core");
				}
			
				if (!Directory.Exists(FolderLocation.Text + "\\AutoController"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\AutoController");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\Repository"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Repository");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\IService"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\IService");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\Service"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Service");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\Others"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Others");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\Core"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\Core");
				}
				if (!Directory.Exists(FolderLocation.Text + "\\BackUps"))
				{
					Directory.CreateDirectory(FolderLocation.Text + "\\BackUps");
				}


				
				string classname, Classlistname, result,resultless;
				Type tp = (Type)listBox1.SelectedItem;
				GeneralClass.ClassName = tp.Name;
				if (firsttime)
				{
					/// this is just to write the IEntity class once
					result = "namespace " + Library_Name_Space.Text + ".Model.Models \n{\npublic class IEntity\n{\n" + "\n}\n}\n";

					// "public int idz { get; set; }\n}\n}\n";

					resultless = "using " + Library_Name_Space.Text + ".Model.Models;\n" + "namespace " + Library_Name_Space.Text + ".Model.Models \n{\npublic class IEntity\n{\n" + "\n}\n}\n";
					resultless = "";
					richTextBox10.Text = result;
					richTextBox2.Text = resultless;
					//label1.Text = richTextBox10.Text;
					richTextBox10.SaveFile(FolderLocation.Text + "\\Models\\IEntity.cs", RichTextBoxStreamType.PlainText);



					//Generate Repository Folder
					richTextBox11.AppendText(GeneralClass.GenerateRepositoryClass(Library_Name_Space.Text));
					richTextBox12.AppendText(GeneralClass.GenerateRepositoryInterface(Library_Name_Space.Text));


					richTextBox11.SaveFile(FolderLocation.Text + "\\Repository\\" + "Repository" + ".cs", RichTextBoxStreamType.PlainText);
					richTextBox12.SaveFile(FolderLocation.Text + "\\Repository\\" + "IRepository" + ".cs", RichTextBoxStreamType.PlainText);

					richTextBox13.AppendText(GeneralClass.GenerateRepositoryActionStatus(Library_Name_Space.Text));
					richTextBox14.AppendText(GeneralClass.GenerateRepositoryActionResult(Library_Name_Space.Text));

					richTextBox13.SaveFile(FolderLocation.Text + "\\Repository\\" + "RepositoryActionStatus" + ".cs", RichTextBoxStreamType.PlainText);
					richTextBox14.SaveFile(FolderLocation.Text + "\\Repository\\" + "RepositoryActionResult" + ".cs", RichTextBoxStreamType.PlainText);
					richTextBox14.Clear();
					richTextBox17.Clear();
					richTextBox17.AppendText(GeneralClass.GenerateIdentityUser(Library_Name_Space.Text));

					richTextBox17.SaveFile(FolderLocation.Text + "\\Core\\" + Library_Name_Space.Text + "User" + ".cs", RichTextBoxStreamType.PlainText);
					richTextBox17.Clear();

					if ((!File.Exists(FolderLocation.Text + "\\Core\\" + Library_Name_Space.Text + "DbContext" + ".cs")) || (nooftimesb == 0))
					{
						richTextBox17.SaveFile(FolderLocation.Text + "\\Core\\" + Library_Name_Space.Text + "DbContext" + ".cs", RichTextBoxStreamType.PlainText);
						++nooftimesb;
					}
					richTextBox17.LoadFile(FolderLocation.Text + "\\Core\\" + Library_Name_Space.Text + "DbContext" + ".cs", RichTextBoxStreamType.PlainText);



					if (listBox1.SelectedIndex == 0)
					{
						richTextBox17.AppendText("using Microsoft.AspNetCore.Identity.EntityFrameworkCore;\n" +
							"using Microsoft.EntityFrameworkCore; \n" +
							"using " + Library_Name_Space.Text + ".Model.Models;\n" +
							"namespace   " + thenamespace +
							"\n{\n   public   class " + Library_Name_Space.Text + "DbContext : IdentityDbContext<" + Library_Name_Space.Text + "User> " +
							 "\n {\n    public " + Library_Name_Space.Text + "DbContext(DbContextOptions<" + Library_Name_Space.Text + "DbContext> options)\n    : base(options)\n     {\n    }\n" +

								"\nprotected override void OnModelCreating(ModelBuilder builder)\n" +
								   "\n{  base.OnModelCreating(builder); \n    {\n    }\n \n}\n"//);
						+ GeneralClass.GenerateAdditionsTo_AppDbContext_DbSet(GeneralClass.ClassName));
					}







					firsttime = false;
				}
				//else
				//{


					result = GenerateBasicPropertiesandMethods(out classname);
				resultless = GenerateBasicPropertiesandMethodsless(out classname);
				//string ConvertToOriginal = GenerateBasicPropertiesandMethodsless( classname);
				//1 richTextBox1.Text = AppendClassStater(classname, thenamespace) + result + AppendFinishing();
				richTextBox10.Text = AppendClassStaterWithoutSeriAttr(classname, thenamespace + ".Model.Models") + result + AppendFinishing();
				   richTextBox2.Text = AppendClassStaterWithoutSeriAttrless(classname, thenamespace + ".Model.Modelsless") + resultless + AppendFinishing();

				if (classname.Trim().Length != 0)
					{

						// MessageBox.Show(Times.ToString() + "" + listBox1.SelectedValue);

						if (tp.IsClass)
						{

							richTextBox6.Text = AppendSerialClassStater(classname, thenamespace) + "\n" + GenerateSerialisationCodes(tp) + GenerateSerialiserCodes(tp) + GenerateDeSerialiserCodes(tp) + AppendFinishing(); ;
						}
						result = GenerateBasicRetrieveMethodForClassList(out Classlistname);

						richTextBox20.Text = AppendClassListStater(classname, Classlistname, thenamespace) + result + AppendFinishing();

						richTextBox10.SaveFile(FolderLocation.Text + "\\Models\\" + classname + "Auto" + ".cs", RichTextBoxStreamType.PlainText);
					    richTextBox2.SaveFile(FolderLocation.Text + "\\Modelsless\\xxx_" + classname + ".cs", RichTextBoxStreamType.PlainText);

					//richTextBox20.SaveFile(FolderLocation.Text + "\\Models\\" + Classlistname + "Auto" + ".cs", RichTextBoxStreamType.PlainText);
					File.WriteAllText(FolderLocation.Text + "\\Models\\" + Classlistname + "Auto" + ".cs", richTextBox20.Text);

					//	richTextBox6.SaveFile(FolderLocation.Text + "\\Serialize\\" + Classlistname + "Serialize" + ".cs", RichTextBoxStreamType.PlainText);
					File.WriteAllText(FolderLocation.Text + "\\Serialize\\" + Classlistname + "Serialize" + ".cs", richTextBox6.Text);

					if (!File.Exists(FolderLocation.Text + "\\Def\\DataDefFile.dj"))
						{
							richTextBox3.SaveFile(FolderLocation.Text + "\\Def\\DataDefFile.dj", RichTextBoxStreamType.PlainText);

						}
						richTextBox3.LoadFile(FolderLocation.Text + "\\Def\\DataDefFile.dj", RichTextBoxStreamType.PlainText);
						richTextBox3.AppendText("\n -- Query Created  on " + DateTime.Now.ToLongDateString() + "\n");
						richTextBox3.AppendText(GenerateBasicCreateTblQuery());

						richTextBox3.SaveFile(FolderLocation.Text + "\\Def\\DataDefFile.dj", RichTextBoxStreamType.PlainText);


						//

						if ((!File.Exists(FolderLocation.Text + "\\BackUps\\BackUps.cs")) || (nooftimes == 0))
						{
							richTextBox7.SaveFile(FolderLocation.Text + "\\BackUps\\BackUps.cs", RichTextBoxStreamType.PlainText);
							++nooftimes;
						}
						richTextBox7.LoadFile(FolderLocation.Text + "\\BackUps\\BackUps.cs", RichTextBoxStreamType.PlainText);



						if (listBox1.SelectedIndex == 0)
						{
							richTextBox7.AppendText("using System; \nusing System.Collections.Generic; \nusing System.Linq;\nusing System.Text; \nusing System.Data.Common;\n  \nnamespace   " + thenamespace + ".Model.Models  \n { \n    public   class  Backups \n {" + GenerateBasicBackUps(classname));
						}
						else

							if (listBox1.SelectedIndex == listBox1.Items.Count - 1)
						{
							richTextBox7.AppendText(GenerateBasicBackUps(classname) + AppendFinishing());
						}
						else
							richTextBox7.AppendText(GenerateBasicBackUps(classname));

						if ((listBox1.SelectedIndex == listBox1.Items.Count - 1) && (listBox1.Items.Count == 1))
						{
							richTextBox7.AppendText(AppendFinishing());// + "\n }");
						}

						richTextBox7.SaveFile(FolderLocation.Text + "\\BackUps\\BackUps.cs", RichTextBoxStreamType.PlainText);

						//

						//backupallonce
						if ((!File.Exists(FolderLocation.Text + "\\BackUps\\BackUpsAll.cs")) || (nooftimesbackp == 0))
						{
							richTextBox8.SaveFile(FolderLocation.Text + "\\BackUps\\BackUpsAll.cs", RichTextBoxStreamType.PlainText);
							++nooftimesbackp;
						}
						richTextBox8.LoadFile(FolderLocation.Text + "\\BackUps\\BackUpsAll.cs", RichTextBoxStreamType.PlainText);



						if (listBox1.SelectedIndex == 0)
						{
							richTextBox8.AppendText("using System; \nusing System.Collections.Generic; \nusing System.Linq;\nusing System.Text; \nusing System.Data.Common;\n  \n\nnamespace   " + thenamespace + ".Model.Models \n { \n public   class  BackupsAll \n {\npublic  BackupsAll(string Filename ,Object mylist )\n {\n" + GenerateBasicAllBackUpOnce(classname));
						}
						else

							if (listBox1.SelectedIndex == listBox1.Items.Count - 1)
						{
							richTextBox8.AppendText(GenerateBasicAllBackUpOnce(classname) + AppendFinishing() + "\n }");
						}
						else
							richTextBox8.AppendText(GenerateBasicAllBackUpOnce(classname));
						if ((listBox1.SelectedIndex == listBox1.Items.Count - 1) && (listBox1.Items.Count == 1))
						{
							richTextBox8.AppendText(AppendFinishing() + "\n }");
						}





						richTextBox8.SaveFile(FolderLocation.Text + "\\BackUps\\BackUpsAll.cs", RichTextBoxStreamType.PlainText);



						//Restoreonce
						if ((!File.Exists(FolderLocation.Text + "\\BackUps\\RestoreBackUp.cs")) || (nooftimesc == 0))
						{
							richTextBox9.SaveFile(FolderLocation.Text + "\\BackUps\\RestoreBackUp.cs", RichTextBoxStreamType.PlainText);
							++nooftimesc;
						}
						richTextBox9.LoadFile(FolderLocation.Text + "\\BackUps\\RestoreBackUp.cs", RichTextBoxStreamType.PlainText);



						if (listBox1.SelectedIndex == 0)
						{
							richTextBox9.AppendText("using System; \nusing System.Collections.Generic; \nusing System.Linq;\nusing System.Text; \nusing System.Data.Common;\n\nnamespace   " + thenamespace + ".Model.Models  \n { \n public   class  RestoreBackUp \n {  " + GenerateRestore(classname));
						}
						else

						 if (listBox1.SelectedIndex == listBox1.Items.Count - 1)
						{
							richTextBox9.AppendText(GenerateRestore(classname) + AppendFinishing());// + "\n }");
						}

						else
							richTextBox9.AppendText(GenerateRestore(classname));


						if ((listBox1.SelectedIndex == listBox1.Items.Count - 1) && (listBox1.Items.Count == 1))
						{
							richTextBox9.AppendText(AppendFinishing());// + "\n }");
						}

						richTextBox9.SaveFile(FolderLocation.Text + "\\BackUps\\RestoreBackUp.cs", RichTextBoxStreamType.PlainText);



						richTextBox4.Text = AppendClassStaterWithoutSeriAttr(classname, thenamespace) + AppendFinishing();
						richTextBox4.SaveFile(FolderLocation.Text + "\\Dev\\" + classname + ".cs", RichTextBoxStreamType.PlainText);

						richTextBox5.Text = AppendClassListStaterWithoutSeriAttr(classname, Classlistname, thenamespace + ".Model.Models") + AppendFinishing();
						richTextBox5.SaveFile(FolderLocation.Text + "\\Dev\\" + Classlistname + ".cs", RichTextBoxStreamType.PlainText);
						//webapi part

						richTextBox19.AppendText(GeneralClass.GenerateintefaceService(Library_Name_Space.Text, GeneralClass.ClassName));
						richTextBox14.AppendText(GeneralClass.GenerateServive(Library_Name_Space.Text, GeneralClass.ClassName));

						richTextBox19.SaveFile(FolderLocation.Text + "\\IService\\I" + GeneralClass.ClassName + "Service" + ".cs", RichTextBoxStreamType.PlainText);
						richTextBox14.SaveFile(FolderLocation.Text + "\\Service\\" + GeneralClass.ClassName + "Service" + ".cs", RichTextBoxStreamType.PlainText);

						richTextBox15.AppendText(GeneralClass.GenerateControllers(Library_Name_Space.Text, GeneralClass.ClassName));

						richTextBox15.SaveFile(FolderLocation.Text + "\\AutoController\\" + GeneralClass.ClassName + "Controller" + ".cs", RichTextBoxStreamType.PlainText);

						richTextBox16.AppendText(GeneralClass.GenerateAdditionsToStatUpFile(GeneralClass.ClassName));

						richTextBox18.AppendText(GeneralClass.GenerateAdditionsTo_AppDbContext_maps(GeneralClass.ClassName));


						if (listBox1.SelectedIndex == listBox1.Items.Count - 1)
						{
							richTextBox17.AppendText(GeneralClass.GenerateAdditionsTo_AppDbContext_DbSet(GeneralClass.ClassName) + AppendAppDbContextFinishing());
						}
						else
							richTextBox17.AppendText(GeneralClass.GenerateAdditionsTo_AppDbContext_DbSet(GeneralClass.ClassName));
						if ((listBox1.SelectedIndex == listBox1.Items.Count - 1) && (listBox1.Items.Count == 1))
						{
							richTextBox17.AppendText(AppendAppDbContextFinishing() + "\n }");
						}
					//} // end of else fisrt time
					richTextBox18.SaveFile(FolderLocation.Text + "\\Core\\" + Library_Name_Space.Text + "_AppDbContext_maps" + ".txt", RichTextBoxStreamType.PlainText);
					richTextBox16.SaveFile(FolderLocation.Text + "\\Others\\" + "StattUpAAdditions" + ".txt", RichTextBoxStreamType.PlainText);
					richTextBox17.SaveFile(FolderLocation.Text + "\\Core\\" + Library_Name_Space.Text + "DbContext" + ".cs", RichTextBoxStreamType.PlainText);

				} //end of listbox

				//ranonce = true;

			}
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
		ModeldllPath	 .Text = Application.ExecutablePath.ToString();
			folderBrowserDialog1.SelectedPath = Application.StartupPath;
			FolderLocation.Text = Application.StartupPath;
		}

		private void button3_Click(object sender, EventArgs e)
		{

			folderBrowserDialog1.ShowDialog();
			FolderLocation.Text = folderBrowserDialog1.SelectedPath;
		}

		private string AppendAppDbContextFinishing()
		{
			return " }  \n    } ";
		}

		private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
