using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedWebApiBuilder
{
	public static class GeneralClass
	{
		public static string ClassName { get; internal set; }

		public static string ReturnDefaultType(string type)
		{
			string str = "";
			switch (type)
			{
				case "VarChar":
				case "AnsiString":
				case "AnsiStringFixedLength":
				case "string":

					str = "string";

					break;
				case "String":

					str = "String";

					break;
				case "Binary2":
					str = "VarBinary";
					break;
				case "Boolean":
					str = "bit";
					break;

				case "Byte":
					str = "Byte";
					break;

				case "Currency":
					str = "Currency";
					break;

				case "Date":
				case "DateTime":
				case "DateTime2":
				case "DateTimeOffset":
					str = "Datetime";
					break;

				case "Decimal":
					str = "decimal";
					break;
				case "Double":
					str = "Double";
					break;
				case "Guid":
					str = "Guid";
					break;

				case "Int":
				case "Int16":
				case "Int32":
				case "Int64":
					str = "int";
					break;

				case "Object":
					str = "Object";
					break;
				case "SByte":
					break;

				case "Single":
					break;


				case "VarNumeric":
					break;
				case "Xml":
					str = "Xml";
					break;
				default:
					str = type;
					break;
			}
			return str;
		}


	
		internal static string GenerateRepositoryClass(string Appname)
		{
			string ResultLine = "";

			ResultLine = GeneralClass.Repo_header(Appname)
					   + GeneralClass.Repo_Prop_Contr(Appname)

						+ GeneralClass.Repo_Add()

						 + GeneralClass.Repo_delete()
						  + GeneralClass.Repo_Update()
						   + GeneralClass.Repo_addList()
						+ GeneralClass.Repo_FindById()

							 + GeneralClass.Repo_List()
							  + GeneralClass.Repo_Entity()
							   + GeneralClass.Repo_Table()
					   + GeneralClass.Repo_Closing();

			return ResultLine;
		}

		private static string Repo_List()
		{
			return "public IEnumerable<T> List=> this.Entities;";

		}

		private static string Repo_Entity()
		{
			return " private DbSet<T> Entities {get {if (_entities == null)	{	_entities = _context.Set<T>();} return _entities;}	}\n";

		}

		private static string Repo_Table()
		{
			return "	public virtual IQueryable<T> Table { get { return this.Entities; } }\n";

		}

		private static string Repo_FindById()
		{
			return "public T FindById(Guid id) { return this.Entities.Find(id); }\n";


		}
		internal static string GenerateintefaceService(string appname, string classname)
		{
			return GenerateIServiveHeader(appname) + GenerateintefaceServicebody(appname, classname);
		}
		internal static string GenerateServive(string appname, string classname)
		{
			return GenerateServiveHeader(appname) + GenerateServivebody(appname, classname);
		}
		internal static string GenerateRepositoryActionStatus(string appname)
		{
			string ResultLine = "";
			ResultLine = ResultLine + " namespace " + appname + ".Repository.Repository\n";
			ResultLine = ResultLine + "{\n";
			ResultLine = ResultLine + "public enum RepositoryActionStatus\n";
			ResultLine = ResultLine + "{\n";
			ResultLine = ResultLine + "Ok,\n";
			ResultLine = ResultLine + "Created,\n";
			ResultLine = ResultLine + "Updated,\n";
			ResultLine = ResultLine + "NotFound,\n";
			ResultLine = ResultLine + "Deleted,\n";
			ResultLine = ResultLine + "NothingModified,\n";
			ResultLine = ResultLine + "Error\n";
			ResultLine = ResultLine + "}\n";
			ResultLine = ResultLine + "}\n";
			return ResultLine;
		}

		internal static string GenerateIdentityUser(string Library_Name_Space)
		{
			return "using Microsoft.AspNetCore.Identity;\n" +

				   "namespace " + Library_Name_Space + "\n{\n" +

					"public class " + Library_Name_Space + "User : IdentityUser\n{\n\n}\n}\n";


		}


		internal static string GenerateControllers(string appname, string className)
		{

			return ControllerHeaderConstructorplusstarter(appname, className)
				+ GenerateGetControllers(className)
				+ GenerateGetIDControllers(className)
				+ GenerateDeleteControllers(className)
				+ GeneratePutControllers(className)
				 + GeneratePostControllers(className)
				  + GenerateAddListControllers(className)
				+ GenerateControllersFooter();

		}

		private static string GenerateAddListControllers(string classname)
		{
			return "  public RepositoryActionResult<" + classname + "> Create" + classname + "(List<" + classname + "> entity) => _" + classname + "Service.AddList(entity);";
		}

		private static string GeneratePostControllers(string classname)
		{
			string ResultLine = "";
			ResultLine = ResultLine + "       [HttpPost]\n";
			ResultLine = ResultLine + "       public IActionResult Post([FromBody] " + classname + " entity)\n";
			ResultLine = ResultLine + "       {\n";
			ResultLine = ResultLine + "           try\n";
			ResultLine = ResultLine + "           {\n";
			ResultLine = ResultLine + "              if (entity == null) { return BadRequest(); }\n";
			ResultLine = ResultLine + "               var result = _" + classname + "Service.Add(entity);\n";
			ResultLine = ResultLine + "               if (result.Status == RepositoryActionStatus.Created) { var retVal = Request.QueryString + \" / \" + entity." + classname + "Id; return Created(retVal, entity); }\n";
			ResultLine = ResultLine + "               return BadRequest();\n";
			ResultLine = ResultLine + "           }\n";
			ResultLine = ResultLine + "           catch (Exception ex) { return BadRequest(); }\n";
			ResultLine = ResultLine + "       }\n\n";
			return ResultLine;
		}

		internal static string ControllerHeaderConstructorplusstarter(string appname, string classname)
		{
			string ResultLine = "";
			ResultLine = ResultLine + "using System;\n";
			ResultLine = ResultLine + "using Microsoft.AspNetCore.Mvc;\n";
			ResultLine = ResultLine + "using Microsoft.AspNetCore.Cors;\n";
			ResultLine = ResultLine + "using System.Collections.Generic;\n";
			ResultLine = ResultLine + "using " + appname + ".Service.IService;\n";
			ResultLine = ResultLine + "using " + appname + ".Repository.Repository;\n";
			ResultLine = ResultLine + "using " + appname + ".Model.Models;\n";

			// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

			ResultLine = ResultLine + "namespace " + appname + ".WebApi.Controllers\n";
			ResultLine = ResultLine + "{\n";

			ResultLine = ResultLine + "[EnableCors(\"AllowCors\"), Route(\"api/[controller]\")]\n";
			ResultLine = ResultLine + "       public partial class " + classname + "Controller : Controller\n";
			ResultLine = ResultLine + "       {\n";
			ResultLine = ResultLine + "       private readonly I" + classname + "Service _" + classname + "Service;\n";

			ResultLine = ResultLine + "       public  " + classname + "Controller(I" + classname + "Service _" + classname + "Service) { this._" + classname + "Service = _" + classname + "Service; }\n";
			return ResultLine;

		}

		internal static string GenerateAdditionsTo_AppDbContext_maps(string classname)
		{
			return "  new " + classname + "Map(builder.Entity<" + classname + ">());\n";
		}

		internal static string GenerateAdditionsTo_AppDbContext_DbSet(string classname)
		{
			return " public virtual DbSet<" + classname + "> " + classname + "s { get; set; }\n";
		}

		internal static string GenerateAdditionsToStatUpFile(string classname)
		{
			return "     services.AddTransient<I" + classname + "Service, " + classname + "Service>();\n";
		}

		internal static string GenerateGetControllers(string classname)
		{
			string ResultLine = "";

			ResultLine = ResultLine + "       [HttpGet]\n";
			ResultLine = ResultLine + "       public ActionResult Get() { var " + classname + "List = _" + classname + "Service.List; return Ok(" + classname + "List); }\n";


			return ResultLine;

		}
		internal static string GenerateGetIDControllers(string classname)
		{
			string ResultLine = "";
			ResultLine = ResultLine + "       [HttpGet(\"{ id}\")]\n";
			ResultLine = ResultLine + "       public ActionResult GetUsingID(Guid Id) { var A" + classname + "= _" + classname + "Service.FindById(Id); return Ok(A" + classname + "); }\n";

			return ResultLine;

		}
		internal static string GeneratePutControllers(string classname)
		{
			string ResultLine = "";
			ResultLine = ResultLine + "       [HttpPut]\n";
			ResultLine = ResultLine + "       public IActionResult Put([FromBody] " + classname + " entity)\n";
			ResultLine = ResultLine + "       {\n";
			ResultLine = ResultLine + "         try\n";
			ResultLine = ResultLine + "          {\n";
			ResultLine = ResultLine + "             if (entity == null) { return BadRequest(); }\n";
			ResultLine = ResultLine + "             var result = _" + classname + "Service.Update(entity);\n";
			ResultLine = ResultLine + "              if (result.Status == RepositoryActionStatus.Updated) { return Ok(result.Entity); }\n";
			ResultLine = ResultLine + "              return BadRequest();\n";
			ResultLine = ResultLine + "          }\n";
			ResultLine = ResultLine + "         catch (Exception ex) { return BadRequest(); }\n";
			ResultLine = ResultLine + "       }\n\n";

			return ResultLine;

		}
		internal static string GenerateDeleteControllers(string classname)
		{
			string ResultLine = "";
			ResultLine = ResultLine + "       [HttpDelete(\"{ id}\")]\n";
			ResultLine = ResultLine + "       public IActionResult Delete(Guid id)\n";
			ResultLine = ResultLine + "       {\n";
			ResultLine = ResultLine + "         var result = _" + classname + "Service.Delete(id);\n           if (result.Status == RepositoryActionStatus.Deleted)\n";
			ResultLine = ResultLine + "         {\n";
			ResultLine = ResultLine + "          return Ok(new { message = result.Entity });\n";
			ResultLine = ResultLine + "        }\n";
			ResultLine = ResultLine + "       else { return BadRequest(); }\n";
			ResultLine = ResultLine + "       }\n";
			return ResultLine;

		}

		internal static string GenerateControllersFooter()
		{
			return "       } \n}\n";


		}

		internal static string GeneratePostControllersFooter(string classname)
		{
			string ResultLine = "";
			ResultLine = ResultLine + "[HttpPost]\n";
			ResultLine = ResultLine + "public IActionResult Post([FromBody] " + classname + " entity)\n";
			ResultLine = ResultLine + "{\n";
			ResultLine = ResultLine + "    try\n";
			ResultLine = ResultLine + "    {\n";
			ResultLine = ResultLine + "        if (entity == null) { return BadRequest(); }\n";
			ResultLine = ResultLine + "        var result = _" + classname + "Service.Add(entity);\n";
			ResultLine = ResultLine + "        if (result.Status == RepositoryActionStatus.Created) { var retVal = Request.QueryString + \"\\ / \" + entity." + classname + "Id; return Created(retVal, entity); }\n";
			ResultLine = ResultLine + "       return BadRequest();\n";
			ResultLine = ResultLine + "   }\n";
			ResultLine = ResultLine + "    catch (Exception ex) { return BadRequest(); }\n";
			ResultLine = ResultLine + "}\n";

			return ResultLine;

		}


		internal static string GenerateRepositoryActionResult(string appname)
		{
			string ResultLine = "";
			ResultLine = ResultLine + "using System;\n";
			ResultLine = ResultLine + "using System.Collections.Generic;\n";

			ResultLine = ResultLine + "namespace " + appname + ".Repository.Repository\n";
			ResultLine = ResultLine + "{\n";
			ResultLine = ResultLine + "   public class RepositoryActionResult<T> where T : class\n";
			ResultLine = ResultLine + "   {\n";
			ResultLine = ResultLine + "       private IEnumerable<T> entity;\n";
			ResultLine = ResultLine + "        private RepositoryActionStatus created;\n";
			ResultLine = ResultLine + "       private IEnumerable<T> entity1;\n";
			ResultLine = ResultLine + "       private RepositoryActionStatus nothingModified;\n";
			ResultLine = ResultLine + "       private object p;\n";
			ResultLine = ResultLine + "       public T Entity { get; private set; }\n";
			ResultLine = ResultLine + "       public RepositoryActionStatus Status { get; private set; }\n";
			ResultLine = ResultLine + "       public Exception Exception { get; private set; }\n";
			ResultLine = ResultLine + "       public RepositoryActionResult(T entity, RepositoryActionStatus status) { Entity = entity; Status = status; }\n";
			ResultLine = ResultLine + "       public RepositoryActionResult(T entity, RepositoryActionStatus status, Exception exception) : this(entity, status) { Exception = exception; }\n";
			ResultLine = ResultLine + "       public RepositoryActionResult(IEnumerable<T> entity, RepositoryActionStatus created) { this.entity = entity; this.created = created; }\n";
			ResultLine = ResultLine + "       public RepositoryActionResult(IEnumerable<T> entity1, RepositoryActionStatus nothingModified, object p)\n";
			ResultLine = ResultLine + "       { this.entity1 = entity1; this.nothingModified = nothingModified; this.p = p; }\n";

			ResultLine = ResultLine + "   }\n";
			ResultLine = ResultLine + "}\n";
			return ResultLine;
		}

		internal static string GenerateintefaceServicebody(string appname, string classname)
		{
			string ResultLine = "";

			ResultLine = ResultLine + "namespace " + appname + ".Service.IService\n";
			ResultLine = ResultLine + "{\n";
			ResultLine = ResultLine + "public interface I" + classname + "Service\n";
			ResultLine = ResultLine + "{\n";


			ResultLine = ResultLine + "IEnumerable<" + classname + "> List { get; }\n";
			ResultLine = ResultLine + "RepositoryActionResult<" + classname + "> Add(" + classname + " entity);\n";
			ResultLine = ResultLine + "RepositoryActionResult<" + classname + "> AddList(IEnumerable<" + classname + "> entity);\n";
			ResultLine = ResultLine + "RepositoryActionResult<" + classname + "> Delete(Guid Id);\n";
			ResultLine = ResultLine + "RepositoryActionResult<" + classname + "> Update(" + classname + " entity);\n";
			ResultLine = ResultLine + "" + classname + " FindById(Guid Id);\n";
			ResultLine = ResultLine + "}\n";
			ResultLine = ResultLine + "}\n";

			return ResultLine;
		}

		internal static string GenerateServivebody(string appname, string classname)
		{
			string ResultLine = "";
			ResultLine = ResultLine + "namespace " + appname + ".Service.Service\n";
			ResultLine = ResultLine + "{\n";
			ResultLine = ResultLine + "public class " + classname + "Service : I" + classname + "Service\n";
			ResultLine = ResultLine + "{\n";
			ResultLine = ResultLine + "private readonly IRepository<" + classname + "> _" + classname + "Repository;\n";
			ResultLine = ResultLine + "public " + classname + "Service(IRepository<" + classname + "> " + classname + "Repository) { _" + classname + "Repository = " + classname + "Repository; }\n";
			ResultLine = ResultLine + "public IEnumerable<" + classname + "> List => _" + classname + "Repository.List;\n";
			ResultLine = ResultLine + "public RepositoryActionResult<" + classname + "> Add(" + classname + " entity) => _" + classname + "Repository.Add(entity);\n";
			ResultLine = ResultLine + "public RepositoryActionResult<" + classname + "> AddList(IEnumerable<" + classname + "> entity) => _" + classname + "Repository.AddList(entity);\n";
			ResultLine = ResultLine + "public RepositoryActionResult<" + classname + "> Delete(Guid Id) => _" + classname + "Repository.Delete(Id);\n";
			ResultLine = ResultLine + "public " + classname + " FindById(Guid Id) => _" + classname + "Repository.FindById(Id);\n";
			ResultLine = ResultLine + "public RepositoryActionResult<" + classname + "> Update(" + classname + " entity) => _" + classname + "Repository.Update(entity);\n";
			ResultLine = ResultLine + "}\n";
			ResultLine = ResultLine + "}\n";
			return ResultLine;
		}
		internal static string GenerateServiveHeader(string appname)
		{
			string ResultLine = "";
			ResultLine = ResultLine + "using " + appname + ".Model.Models;\n";
			ResultLine = ResultLine + "using " + appname + ".Repository.Repository;\n";
			ResultLine = ResultLine + "using " + appname + ".Service.IService;\n";
			ResultLine = ResultLine + "using System;\n";
			ResultLine = ResultLine + "using System.Collections.Generic;\n";
			ResultLine = ResultLine + "using System.Linq;\n";
			return ResultLine;
		}
		internal static string GenerateIServiveHeader(string appname)
		{
			string ResultLine = "";
			ResultLine = ResultLine + "using " + appname + ".Model.Models;\n";
			ResultLine = ResultLine + "using " + appname + ".Repository.Repository;\n";
			ResultLine = ResultLine + "using System;\n";
			ResultLine = ResultLine + "using System.Collections.Generic;\n";
			return ResultLine;
		}
		private static string Repo_addList()
		{
			string ResultLine = "";
			ResultLine = ResultLine + "           public RepositoryActionResult<T> AddList(IEnumerable<T> entity)\n";
			ResultLine = ResultLine + "           {\n";
			ResultLine = ResultLine + "               this.Entities.AddRange(entity); int result = _context.SaveChanges();\n";
			ResultLine = ResultLine + "               if (result > 0) { return new RepositoryActionResult<T>(entity, RepositoryActionStatus.Created); }\n";
			ResultLine = ResultLine + "               return new RepositoryActionResult<T>(entity, RepositoryActionStatus.NothingModified, null);\n";
			ResultLine = ResultLine + "          }\n";
			return ResultLine;
		}

		private static string Repo_Update()
		{
			string ResultLine = "";

			ResultLine = ResultLine + "         public RepositoryActionResult<T> Update(T entity)\n";
			ResultLine = ResultLine + "           {\n";
			ResultLine = ResultLine + "               try\n";
			ResultLine = ResultLine + "               {\n";
			ResultLine = ResultLine + "                  if (entity == null) { return new RepositoryActionResult<T>(null, RepositoryActionStatus.NotFound, 0); }\n";
			ResultLine = ResultLine + "                   _context.Entry(entity).State = EntityState.Detached; this.Entities.Attach(entity); _context.Entry(entity).State = EntityState.Modified;\n";
			ResultLine = ResultLine + "                   var result = _context.SaveChanges(); if (result > 0) { return new RepositoryActionResult<T>(entity, RepositoryActionStatus.Updated); }\n";
			ResultLine = ResultLine + "                   return new RepositoryActionResult<T>(entity, RepositoryActionStatus.NothingModified, null);\n";
			ResultLine = ResultLine + "               }\n";
			ResultLine = ResultLine + "               catch (Exception ex) { return new RepositoryActionResult<T>(null, RepositoryActionStatus.Error, ex); }\n";
			ResultLine = ResultLine + "          }\n";
			return ResultLine;
		}

		private static string Repo_delete()
		{
			string ResultLine = "";

			ResultLine = ResultLine + "           public RepositoryActionResult<T> Delete(Guid id)\n";
			ResultLine = ResultLine + "            {\n";
			ResultLine = ResultLine + "               var entity = this.FindById(id); if (entity != null)\n";
			ResultLine = ResultLine + "               {\n";
			ResultLine = ResultLine + "                   this.Entities.Remove(entity); _context.SaveChanges();\n";
			ResultLine = ResultLine + "                   return new RepositoryActionResult<T>(null, RepositoryActionStatus.Deleted, 0);\n";
			ResultLine = ResultLine + "               }\n";
			ResultLine = ResultLine + "                return new RepositoryActionResult<T>(null, RepositoryActionStatus.NotFound, 0);\n";
			ResultLine = ResultLine + "           }\n";
			return ResultLine;
		}

		private static string Repo_Add()
		{
			string ResultLine = "";
			ResultLine = ResultLine + "            public RepositoryActionResult<T> Add(T entity)\n";
			ResultLine = ResultLine + "            {\n";
			ResultLine = ResultLine + "             this.Entities.Add(entity); int result = _context.SaveChanges();\n";
			ResultLine = ResultLine + "             if (result > 0) { return new RepositoryActionResult<T>(entity, RepositoryActionStatus.Created); }\n";
			ResultLine = ResultLine + "              return new RepositoryActionResult<T>(entity, RepositoryActionStatus.NothingModified, null);\n";
			ResultLine = ResultLine + "             }\n";

			return ResultLine;
		}

		private static string Repo_Prop_Contr(string appname)
		{
			string ResultLine = "";
			ResultLine = ResultLine + "            public " + appname + "DbContext _context { get; private set; }\n";
			ResultLine = ResultLine + "            private DbSet<T> _entities;\n";

			ResultLine = ResultLine + "            public Repository(" + appname + "DbContext dbct) { this._context = dbct; }\n";
			return ResultLine;
		}

		private static string Repo_header(string appname)
		{
			string ResultLine = "";


			ResultLine = ResultLine + "using Microsoft.EntityFrameworkCore;\n";
			ResultLine = ResultLine + "using " + appname + ".Core;\n";
			ResultLine = ResultLine + "using " + appname + ".Model.Models;\n";
			ResultLine = ResultLine + "using System;\n";
			ResultLine = ResultLine + "using System.Collections.Generic;\n";
			ResultLine = ResultLine + "using System.Linq;\n";
			ResultLine = ResultLine + "namespace " + appname + ".Repository.Repository\n";
			ResultLine = ResultLine + "    {\n";
			ResultLine = ResultLine + "        public class Repository<T> : IRepository<T> where T : IEntity\n";
			ResultLine = ResultLine + "        {\n";
			return ResultLine;
		}

		private static string Repo_Closing()
		{
			return "	}\n }";
		}

		internal static string GenerateRepositoryInterface(string AppName)
		{
			string ResultLine = "";

			ResultLine = ResultLine + "    using " + AppName + ".Model.Models;\n";
			ResultLine = ResultLine + "    using System;\n";
			ResultLine = ResultLine + "    using System.Collections.Generic;\n";

			ResultLine = ResultLine + "    namespace " + AppName + ".Repository.Repository\n";
			ResultLine = ResultLine + "    {\n";
			ResultLine = ResultLine + "    public interface IRepository<T> where T : IEntity\n";
			ResultLine = ResultLine + "    {\n";
			ResultLine = ResultLine + "               IEnumerable<T> List { get; }\n";
			ResultLine = ResultLine + "               RepositoryActionResult<T> Add(T entity);\n";
			ResultLine = ResultLine + "               RepositoryActionResult<T> Delete(Guid Id);\n";
			ResultLine = ResultLine + "               RepositoryActionResult<T> Update(T entity);\n";
			ResultLine = ResultLine + "               T FindById(Guid Id);\n";
			ResultLine = ResultLine + "               RepositoryActionResult<T> AddList(IEnumerable<T> entity);\n";
			ResultLine = ResultLine + "           }\n";
			ResultLine = ResultLine + "       }\n";

			return ResultLine;
		}
	}
}
