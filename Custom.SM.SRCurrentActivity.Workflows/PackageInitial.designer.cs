using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Custom.SM.SRCurrentActivity.Workflows
{
    partial class PackageInitial
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("", "")]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            this.Code = new System.Workflow.Activities.CodeActivity();
            // 
            // Code
            // 
            this.Code.Name = "Code";
            this.Code.ExecuteCode += new System.EventHandler(this.Code_ExecuteCode);
            // 
            // PackageInitial
            // 
            this.Activities.Add(this.Code);
            this.Name = "PackageInitial";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity Code;
    }
}
