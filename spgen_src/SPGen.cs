using System;
using System.Text;

namespace Bluegrass.Data
{	
	/// <summary>
	/// Supported Stored Procedure types
	/// </summary>
	public enum StoredProcedureTypes
	{
		UPDATE,
		INSERT,
		DELETE,
		SELECT
	}

	/// <summary>
	/// Stored Procedure Helper class
	/// </summary>
	public class StoredProcedure
	{		
		/// <summary>
		/// Generates code for an UPDATE or INSERT or DELETE Stored Procedure
		/// </summary>
		/// <param name="sptypeGenerate">The type of SP to generate, INSERT or UPDATE</param>
		/// <param name="colsFields">A SQLDMO.Columns collection</param>
		/// <returns>The SP code</returns>

		public string GenerateInsert (SQLDMO.Columns colsFields, string sTableName)
		{
			StringBuilder sp = new StringBuilder();

			sp.Append ("CREATE PROCEDURE sp_" + sTableName + "_ins");
			sp.Append (Environment.NewLine + "(" + Environment.NewLine);

			// deklaracija varijabli
			foreach (SQLDMO.Column colCurrent in colsFields)
			{
				if (colCurrent.Identity != true)
				{
					sp.Append ("	@" + colCurrent.Name + " " + colCurrent.Datatype);

					if (
						colCurrent.Datatype == "binary" || 
						colCurrent.Datatype == "char" || 
						colCurrent.Datatype == "nchar" || 
						colCurrent.Datatype == "nvarchar" || 
						colCurrent.Datatype == "varbinary" || 
						colCurrent.Datatype == "varchar"
						)
						sp.AppendFormat("(" + colCurrent.Length + ")");

					sp.Append (",");
					sp.Append (Environment.NewLine);
				}
			}
			sp.Remove (sp.Length - 3, 3);		// maknimo zadnji zarez

			sp.Append (Environment.NewLine + ")" + Environment.NewLine);
			sp.Append ("AS" + Environment.NewLine);
			sp.Append ("	SET NOCOUNT ON" + Environment.NewLine);

			sp.Append (Environment.NewLine);

			// konstrukcija "INSERT INTO ..."
			sp.Append ("	INSERT INTO " + sTableName + " (");
			foreach (SQLDMO.Column colCurrent in colsFields)
			{
				if (colCurrent.Identity != true)
				{
					sp.Append (colCurrent.Name + ", ");
				}
			}
			sp.Remove (sp.Length - 2, 2);		// maknimo zadnji zarez
			sp.Append (")" + Environment.NewLine);

			// konstrukcija "VALUES ..."
			sp.Append ("	VALUES (");
			foreach (SQLDMO.Column colCurrent in colsFields)
			{
				if (colCurrent.Identity != true)
				{
					sp.Append ("@" + colCurrent.Name + ", ");
				}
			}
			sp.Remove (sp.Length - 2, 2);		// maknimo zadnji zarez
			sp.Append (");" + Environment.NewLine);

			sp.Append (Environment.NewLine);

			sp.Append ("	RETURN @@IDENTITY");

			return sp.ToString();
		}


		public string GenerateUpdate (SQLDMO.Columns colsFields, string sTableName)
		{
			StringBuilder sp = new StringBuilder();

			sp.Append ("CREATE PROCEDURE sp_" + sTableName + "_upd");
			sp.Append (Environment.NewLine + "(" + Environment.NewLine);

			string IdentityCol_Name = "";

			// deklaracija varijabli
			foreach (SQLDMO.Column colCurrent in colsFields)
			{
				if (colCurrent.Identity == true)
				{
					IdentityCol_Name = colCurrent.Name;
				}

				sp.Append ("	@" + colCurrent.Name + " " + colCurrent.Datatype);

				if (
					colCurrent.Datatype == "binary" || 
					colCurrent.Datatype == "char" || 
					colCurrent.Datatype == "nchar" || 
					colCurrent.Datatype == "nvarchar" || 
					colCurrent.Datatype == "varbinary" || 
					colCurrent.Datatype == "varchar"
					)
					sp.AppendFormat("(" + colCurrent.Length + ")");

				sp.Append (",");
				sp.Append (Environment.NewLine);

			}
			sp.Remove (sp.Length - 3, 3);		// maknimo zadnji zarez

			sp.Append (Environment.NewLine + ")" + Environment.NewLine);
			sp.Append ("AS" + Environment.NewLine);
			sp.Append ("	SET NOCOUNT ON" + Environment.NewLine);

			sp.Append (Environment.NewLine);

			// konstrukcija "UPDATE ..."
			sp.Append ("	UPDATE " + sTableName + " SET" + Environment.NewLine);
			foreach (SQLDMO.Column colCurrent in colsFields)
			{
				if (colCurrent.Identity != true)
				{
					sp.Append ("		" + colCurrent.Name + " = @" + colCurrent.Name + "," + Environment.NewLine);
				}
			}
			sp.Remove (sp.Length - 3, 3);		// maknimo zadnji zarez

			sp.Append (Environment.NewLine + "	WHERE " + IdentityCol_Name + " = @" + IdentityCol_Name);

			return sp.ToString();
		}

		public string GenerateDelete (SQLDMO.Columns colsFields, string sTableName)
		{
			// ovo je nova opcija

			StringBuilder sp = new StringBuilder();
			sp.Append ("CREATE PROCEDURE sp_" + sTableName + "_del");
			sp.Append (Environment.NewLine + "(" + Environment.NewLine);
			
			// deklaracija varijabli
			string IdentityCol_Name = "";

			foreach (SQLDMO.Column colCurrent in colsFields)
			{
				if (colCurrent.InPrimaryKey)
				{
					IdentityCol_Name = colCurrent.Name;
					sp.Append ("	@" + colCurrent.Name + " " + colCurrent.Datatype);
					if (
						colCurrent.Datatype == "binary" || 
						colCurrent.Datatype == "char" || 
						colCurrent.Datatype == "nchar" || 
						colCurrent.Datatype == "nvarchar" || 
						colCurrent.Datatype == "varbinary" || 
						colCurrent.Datatype == "varchar"
						)
						sp.AppendFormat("(" + colCurrent.Length + ")");
					sp.Append (",");
					sp.Append (Environment.NewLine);				
				}
			}
			sp.Remove (sp.Length - 3, 3);		// maknimo zadnji zarez

			sp.Append (Environment.NewLine + ")" + Environment.NewLine);
			sp.Append ("AS" + Environment.NewLine);
			sp.Append ("	SET NOCOUNT ON" + Environment.NewLine);
            
			sp.Append (Environment.NewLine);
			sp.Append ("	DELETE FROM [" + sTableName + "]");
			sp.Append (Environment.NewLine + "	WHERE " + IdentityCol_Name + " = @" + IdentityCol_Name);

			return sp.ToString();

		}

		public string GenerateSelect (SQLDMO.Columns colsFields, string sTableName)
		{
			// nova dodatna procedura
			StringBuilder sp = new StringBuilder();
			sp.Append ("CREATE PROCEDURE sp_" + sTableName + "_sel");
			sp.Append (Environment.NewLine + "(" + Environment.NewLine);
			sp.Append (Environment.NewLine + ")" + Environment.NewLine);
			sp.Append ("AS" + Environment.NewLine);
			sp.Append ("	SET NOCOUNT ON" + Environment.NewLine);
			sp.Append (Environment.NewLine);

			//string IdentityCol_Name = "";		

			//deklaracija varijabli
			sp.Append("		SELECT ");
			foreach (SQLDMO.Column colCurrent in colsFields)
			{
				sp.Append (Environment.NewLine + "		[" + colCurrent.Name + "], ");
			}
			sp.Remove (sp.Length - 2, 2);		// maknimo zadnji zarez
			sp.Append ( Environment.NewLine + "FROM [" + sTableName + "]");
			
			return sp.ToString();
		}



		public string Generate(StoredProcedureTypes sptypeGenerate, SQLDMO.Columns colsFields, string sTableName)
		{
			StringBuilder sGeneratedCode = new StringBuilder();
			StringBuilder sParamDeclaration = new StringBuilder();
			StringBuilder sBody = new StringBuilder();			
			StringBuilder sINSERTValues = new StringBuilder();

			// Setup SP code, begining is the same no matter the type
			sGeneratedCode.AppendFormat("CREATE PROCEDURE {0}_{1}", new string[]{sTableName, sptypeGenerate.ToString()});			
			sGeneratedCode.Append(Environment.NewLine);

			// Setup body code, different for UPDATE and INSERT
			switch (sptypeGenerate)
			{
				case StoredProcedureTypes.INSERT:
					sBody.AppendFormat("INSERT INTO [{0}] (", sTableName);
					sBody.Append(Environment.NewLine);


					sINSERTValues.Append("VALUES (");
					sINSERTValues.Append(Environment.NewLine);
					break;
				
				case StoredProcedureTypes.UPDATE:
					sBody.AppendFormat("UPDATE [{0}]", sTableName);					
					sBody.Append(Environment.NewLine);
					sBody.Append("SET");
					sBody.Append(Environment.NewLine);
					break;
			}


			// Param Declaration construction
			sParamDeclaration.Append ("(");
			sParamDeclaration.Append (Environment.NewLine);
			foreach (SQLDMO.Column colCurrent in colsFields)
			{
				sParamDeclaration.AppendFormat("    @{0} {1}", new string[]{colCurrent.Name, colCurrent.Datatype});				
								
				// Only binary, char, nchar, nvarchar, varbinary and varchar may have their length declared								
				
				if (
					colCurrent.Datatype == "binary" || 
					colCurrent.Datatype == "char" || 
					colCurrent.Datatype == "nchar" || 
					colCurrent.Datatype == "nvarchar" || 
					colCurrent.Datatype == "varbinary" || 
					colCurrent.Datatype == "varchar")
					sParamDeclaration.AppendFormat("({0})", colCurrent.Length);
				
				sParamDeclaration.Append(",");
				sParamDeclaration.Append(Environment.NewLine);

				// Body construction, different for INSERT and UPDATE
				switch (sptypeGenerate)
				{
					case StoredProcedureTypes.INSERT:						
						sINSERTValues.AppendFormat("    @{0},", colCurrent.Name);						
						sINSERTValues.Append(Environment.NewLine);

						sBody.AppendFormat("    {0},", colCurrent.Name);						
						sBody.Append(Environment.NewLine);
						break;

					case StoredProcedureTypes.UPDATE:
						sBody.AppendFormat("    {0} = @{0},", new string[]{colCurrent.Name, });											
						sBody.Append(Environment.NewLine);
						break;
				}
			}

			// Now stitch the body parts together into the SP whole			
			sGeneratedCode.Append(sParamDeclaration.Remove(sParamDeclaration.Length - 3, 3));
			sGeneratedCode.Append (")");
			sGeneratedCode.Append (Environment.NewLine);
			sGeneratedCode.Append(Environment.NewLine);
			sGeneratedCode.Append("AS");
			sGeneratedCode.Append(Environment.NewLine);
			sGeneratedCode.Append(sBody.Remove(sBody.Length -3, 3));			
			if (sptypeGenerate == StoredProcedureTypes.INSERT)
			{
				sGeneratedCode.Append(")");
				sGeneratedCode.Append(Environment.NewLine);
				sGeneratedCode.Append(sINSERTValues.Remove(sINSERTValues.Length - 3, 3));
				sGeneratedCode.Append(")");				
			}
			sGeneratedCode.Append(Environment.NewLine);
			sGeneratedCode.Append("GO");
					
			return sGeneratedCode.ToString();
		}
	}
}
