using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Configuration;
using Bluegrass.Data;


using System.Data;
using System.Data.SqlClient;

namespace SPGen
{
	/// <summary>
	/// Main UI for SPGen
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{		
		protected SQLDMOHelper dmoMain = new SQLDMOHelper();

		private System.Windows.Forms.StatusBar statbarMain;
		private System.Windows.Forms.StatusBarPanel statbarpnlMain;
		private System.Windows.Forms.Button cmdConnect;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.ComboBox selServers;
		private System.Windows.Forms.Splitter spltrMain;
		private System.Windows.Forms.Panel pnlConnectTo;
		private System.Windows.Forms.TreeView tvwServerExplorer;
		private System.Windows.Forms.TextBox txtGeneratedCode;
		private System.Windows.Forms.ImageList imglstMain;
		private System.Windows.Forms.Button btnExec;
		private System.ComponentModel.IContainer components;

		// u svrhu pokretanje procedura na bazi
		public SqlConnection conn = new SqlConnection();
		public SqlCommand sqlComm = new SqlCommand();
        public ConfigurationSettings conf;



		public frmMain()
		{			
			InitializeComponent();			

			// List Registered Servers
			object[] objServers = (object[])dmoMain.RegisteredServers;
			selServers.Items.AddRange(objServers);

			// Default connection details, if provided
			//NameValueCollection settingsAppSettings = (NameValueCollection)ConfigurationSettings.AppSettings;
            NameValueCollection settingsAppSettings = new NameValueCollection();             

			if (settingsAppSettings["ServerName"] != null && settingsAppSettings["ServerName"] != "")
			{
				selServers.Text = settingsAppSettings["ServerName"];
				dmoMain.ServerName = settingsAppSettings["ServerName"];
			}
			if (settingsAppSettings["UserName"] != null && settingsAppSettings["UserName"] != "")
			{
				txtUser.Text = settingsAppSettings["UserName"];
				dmoMain.UserName = settingsAppSettings["UserName"];
			}
			if (settingsAppSettings["Password"] != null && settingsAppSettings["Password"] != "")
			{
				char chPassword = '*';
				txtPassword.PasswordChar = chPassword;
				txtPassword.Text = settingsAppSettings["Password"];
				dmoMain.Password = settingsAppSettings["Password"];
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statbarMain = new System.Windows.Forms.StatusBar();
            this.statbarpnlMain = new System.Windows.Forms.StatusBarPanel();
            this.pnlConnectTo = new System.Windows.Forms.Panel();
            this.btnExec = new System.Windows.Forms.Button();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.selServers = new System.Windows.Forms.ComboBox();
            this.tvwServerExplorer = new System.Windows.Forms.TreeView();
            this.imglstMain = new System.Windows.Forms.ImageList(this.components);
            this.spltrMain = new System.Windows.Forms.Splitter();
            this.txtGeneratedCode = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.statbarpnlMain)).BeginInit();
            this.pnlConnectTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // statbarMain
            // 
            this.statbarMain.Location = new System.Drawing.Point(0, 603);
            this.statbarMain.Name = "statbarMain";
            this.statbarMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statbarpnlMain});
            this.statbarMain.ShowPanels = true;
            this.statbarMain.Size = new System.Drawing.Size(896, 26);
            this.statbarMain.TabIndex = 5;
            // 
            // statbarpnlMain
            // 
            this.statbarpnlMain.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statbarpnlMain.Name = "statbarpnlMain";
            this.statbarpnlMain.Text = "Awaiting your orders...";
            this.statbarpnlMain.Width = 879;
            // 
            // pnlConnectTo
            // 
            this.pnlConnectTo.Controls.Add(this.btnExec);
            this.pnlConnectTo.Controls.Add(this.cmdConnect);
            this.pnlConnectTo.Controls.Add(this.txtPassword);
            this.pnlConnectTo.Controls.Add(this.txtUser);
            this.pnlConnectTo.Controls.Add(this.selServers);
            this.pnlConnectTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlConnectTo.Location = new System.Drawing.Point(0, 0);
            this.pnlConnectTo.Name = "pnlConnectTo";
            this.pnlConnectTo.Size = new System.Drawing.Size(896, 48);
            this.pnlConnectTo.TabIndex = 9;
            // 
            // btnExec
            // 
            this.btnExec.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExec.Location = new System.Drawing.Point(720, 18);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(77, 25);
            this.btnExec.TabIndex = 8;
            this.btnExec.Text = "Execute";
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // cmdConnect
            // 
            this.cmdConnect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmdConnect.Location = new System.Drawing.Point(634, 18);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(76, 25);
            this.cmdConnect.TabIndex = 7;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(422, 18);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(192, 22);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.Text = "Password";
            this.txtPassword.Enter += new System.EventHandler(this.txtPassword_Enter);
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // selServers
            // 
            this.selServers.Location = new System.Drawing.Point(10, 18);
            this.selServers.Name = "selServers";
            this.selServers.Size = new System.Drawing.Size(192, 24);
            this.selServers.TabIndex = 4;
            this.selServers.Text = "Server Name";
            this.selServers.Leave += new System.EventHandler(this.selServers_Leave);
            // 
            // tvwServerExplorer
            // 
            this.tvwServerExplorer.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvwServerExplorer.FullRowSelect = true;
            this.tvwServerExplorer.ImageIndex = 0;
            this.tvwServerExplorer.ImageList = this.imglstMain;
            this.tvwServerExplorer.Location = new System.Drawing.Point(0, 48);
            this.tvwServerExplorer.Name = "tvwServerExplorer";
            this.tvwServerExplorer.SelectedImageIndex = 0;
            this.tvwServerExplorer.Size = new System.Drawing.Size(355, 555);
            this.tvwServerExplorer.TabIndex = 10;
            this.tvwServerExplorer.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwServerExplorer_BeforeExpand);
            this.tvwServerExplorer.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwServerExplorer_AfterSelect);
            // 
            // imglstMain
            // 
            this.imglstMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstMain.ImageStream")));
            this.imglstMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imglstMain.Images.SetKeyName(0, "");
            this.imglstMain.Images.SetKeyName(1, "");
            this.imglstMain.Images.SetKeyName(2, "");
            // 
            // spltrMain
            // 
            this.spltrMain.Location = new System.Drawing.Point(355, 48);
            this.spltrMain.Name = "spltrMain";
            this.spltrMain.Size = new System.Drawing.Size(4, 555);
            this.spltrMain.TabIndex = 11;
            this.spltrMain.TabStop = false;
            // 
            // txtGeneratedCode
            // 
            this.txtGeneratedCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGeneratedCode.HideSelection = false;
            this.txtGeneratedCode.Location = new System.Drawing.Point(359, 48);
            this.txtGeneratedCode.Multiline = true;
            this.txtGeneratedCode.Name = "txtGeneratedCode";
            this.txtGeneratedCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGeneratedCode.Size = new System.Drawing.Size(537, 555);
            this.txtGeneratedCode.TabIndex = 12;
            // 
            // txtUser
            // 
            this.txtUser.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SPGen.Properties.Settings.Default, "sa", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtUser.Location = new System.Drawing.Point(221, 18);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(192, 22);
            this.txtUser.TabIndex = 5;
            this.txtUser.Text = global::SPGen.Properties.Settings.Default.sa;
            this.txtUser.Enter += new System.EventHandler(this.txtUser_Enter);
            this.txtUser.Leave += new System.EventHandler(this.txtUser_Leave);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(896, 629);
            this.Controls.Add(this.txtGeneratedCode);
            this.Controls.Add(this.spltrMain);
            this.Controls.Add(this.tvwServerExplorer);
            this.Controls.Add(this.pnlConnectTo);
            this.Controls.Add(this.statbarMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "SPGen: Stored Procedure Generator";
            ((System.ComponentModel.ISupportInitialize)(this.statbarpnlMain)).EndInit();
            this.pnlConnectTo.ResumeLayout(false);
            this.pnlConnectTo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		private void cmdConnect_Click(object sender, System.EventArgs e)
		{						
			// First ensure connection details are valid
			if (dmoMain.ServerName == "" || dmoMain.UserName == "")
			{
				MessageBox.Show("Please enter in valid connection details.",this.Text);				
			}
			else
			{
				this.Cursor = Cursors.WaitCursor;
				statbarpnlMain.Text = "Connecting to SQL Server...";

				//Valid connection details				
				tvwServerExplorer.Nodes.Clear();

				// List Databases
				try
				{				
					dmoMain.Connect();
					Array aDatabases = (Array)dmoMain.Databases;
					dmoMain.DisConnect();

					for (int i = 0; i < aDatabases.Length; i++)
					{
						TreeNode treenodeDatabase = new TreeNode(aDatabases.GetValue(i).ToString(), 0, 0);
						treenodeDatabase.Nodes.Add("");
						tvwServerExplorer.Nodes.Add(treenodeDatabase);
					}

					this.Cursor = Cursors.Default;
					statbarpnlMain.Text = "Connectiong successful, databases listed...";
				}
				catch
				{				
					this.Cursor = Cursors.Default;
					statbarpnlMain.Text = "Connectiong un-successful...";
					MessageBox.Show("Connection to database failed. Please check your Server Name, User and Password.", this.Text);
				}
			}
		}

		private void txtPassword_Enter(object sender, System.EventArgs e)
		{
			if (txtPassword.Text == "Password") 
			{
				txtPassword.Text = "";
				char chPassword = '*';
				txtPassword.PasswordChar = chPassword;
			}
		}

		private void txtUser_Enter(object sender, System.EventArgs e)
		{
			if (txtUser.Text == "User") txtUser.Text = "";
		}

		private void tvwServerExplorer_BeforeExpand(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			// List all Tables for selected Database

			if (e.Node.ImageIndex == 0)
			{
				this.Cursor = Cursors.WaitCursor;
				statbarpnlMain.Text = "Listing Tables...";
				// Set database to get tables from						
				dmoMain.Database = e.Node.Text;				
						
				// Clear dummy node
				e.Node.Nodes.Clear();

				try
				{
					// List Tables
					dmoMain.Connect();
					Array aTables = (Array)dmoMain.Tables;				
					dmoMain.DisConnect();

					for (int i = 0; i < aTables.Length; i++)
					{
						TreeNode treenodeTable = new TreeNode(aTables.GetValue(i).ToString(), 1, 1);
						TreeNode treenodeTableUPDATE = new TreeNode("UPDATE Stored Procedure", 2, 2);
						TreeNode treenodeTableINSERT = new TreeNode("INSERT Stored Procedure", 2, 2);
						TreeNode treenodeTableDELETE = new TreeNode("DELETE Stored Procedure", 2, 2);
						TreeNode treenodeTableSELECT = new TreeNode("SELECT Stored Procedure", 2, 2);

						treenodeTable.Nodes.Add(treenodeTableUPDATE);
						treenodeTable.Nodes.Add(treenodeTableINSERT);
						treenodeTable.Nodes.Add(treenodeTableDELETE);
						treenodeTable.Nodes.Add(treenodeTableSELECT);
						e.Node.Nodes.Add(treenodeTable);
					}
					
					this.Cursor = Cursors.Default;
					statbarpnlMain.Text = "Tables listed...";
				}
				catch
				{
					this.Cursor = Cursors.Default;
					statbarpnlMain.Text = "Problem listing Tables...";

					MessageBox.Show("Problem connecting to database. Cannot list tables, reconnect advised.", this.Text);
				}
			}
		}

		private void tvwServerExplorer_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			TreeNode tnodeSelected = (TreeNode)e.Node;
			
			if (tnodeSelected.ImageIndex == 2)
			{
				this.Cursor = Cursors.WaitCursor;
				statbarpnlMain.Text = "Generating Stored Procedure, please wait...";
				// SP selected, generate SP
				TreeNode tnodeTable = (TreeNode)tnodeSelected.Parent;				
				dmoMain.Table = tnodeTable.Text;

				StoredProcedure spgen = new StoredProcedure();
				//StoredProcedureTypes spType = new StoredProcedureTypes();
				dmoMain.Connect();
				if (tnodeSelected.Text.IndexOf("INSERT") != -1)
					txtGeneratedCode.Text = spgen.GenerateInsert (dmoMain.Fields, dmoMain.Table);
				if (tnodeSelected.Text.IndexOf("UPDATE") != -1)
					txtGeneratedCode.Text = spgen.GenerateUpdate (dmoMain.Fields, dmoMain.Table);
				if (tnodeSelected.Text.IndexOf("DELETE") != -1)
					txtGeneratedCode.Text = spgen.GenerateDelete (dmoMain.Fields, dmoMain.Table);
				if (tnodeSelected.Text.IndexOf("SELECT") != -1)
					txtGeneratedCode.Text = spgen.GenerateSelect (dmoMain.Fields, dmoMain.Table);
				
				
				dmoMain.DisConnect();

				this.Cursor = Cursors.Default;
				statbarpnlMain.Text = "Stored Procedure generated...";

				txtGeneratedCode.Focus();
				txtGeneratedCode.SelectAll();
			}
		}

		private void txtUser_Leave(object sender, System.EventArgs e)
		{
			if (txtUser.Text == "")
				txtUser.Text = "User";
			else
				dmoMain.UserName = txtUser.Text;
		}

		private void txtPassword_Leave(object sender, System.EventArgs e)
		{
			if (txtPassword.Text == "")
			{
				txtPassword.Text = "Password";
				char chResetPassword = (char)0;
				txtPassword.PasswordChar = chResetPassword;
				dmoMain.Password = "";
			}
			else
				dmoMain.Password = txtPassword.Text;
		}

		private void selServers_Leave(object sender, System.EventArgs e)
		{			
			if (selServers.Text == "")
				selServers.Text = "Select server";
			else
				dmoMain.ServerName = selServers.Text;
		}

		private void btnExec_Click(object sender, System.EventArgs e)
		{
			// izvrši postavljenu proceduru
            if (tvwServerExplorer.SelectedNode.Text.IndexOf("UPDATE") == -1 && tvwServerExplorer.SelectedNode.Text.IndexOf("DELETE") == -1 &&  tvwServerExplorer.SelectedNode.Text.IndexOf("INSERT") == -1 && tvwServerExplorer.SelectedNode.Text.IndexOf("SELECT") == -1)
				return;

			string sDataBase = "";
			TreeNode TN = new TreeNode();
			TN = tvwServerExplorer.SelectedNode;
			sDataBase = TN.Parent.Parent.Text;

			conn.ConnectionString = "Server=" + selServers.SelectedText + ";Database=" + sDataBase + ";User ID=" + txtUser.Text + ";Password=" + txtPassword.Text;
			conn.Open();			  
			sqlComm = new SqlCommand(txtGeneratedCode.Text,conn);        			
			sqlComm.ExecuteNonQuery();
			conn.Close();
		}		
	}
}
