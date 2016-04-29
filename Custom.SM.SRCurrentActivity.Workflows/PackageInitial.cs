using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Microsoft.EnterpriseManagement;
using Microsoft.EnterpriseManagement.Common;
using Microsoft.EnterpriseManagement.Configuration;
using System.Collections.Generic;
using Microsoft.EnterpriseManagement.Workflow.Common;
using System.Diagnostics;

namespace Custom.SM.SRCurrentActivity.Workflows
{
    public partial class PackageInitial : System.Workflow.Activities.SequentialWorkflowActivity
    {
        public static DependencyProperty ActivityIdProperty = DependencyProperty.Register("ActivityId", typeof(string), typeof(PackageInitial));
        public string ActivityId {
            get
            {
                return (string)this.GetValue(ActivityIdProperty);
            }
            set
            {
                this.SetValue(ActivityIdProperty, (object)value);
            }
        }

        public string Log = "";
        public PackageInitial()
        {
            InitializeComponent();
        }

        private void Code_ExecuteCode(object sender, EventArgs e)
        {
            try {
                LogEvent("Connecting to SCSM");
                EnterpriseManagementGroup emg = new EnterpriseManagementGroup("localhost");
                LogEvent("Retrieving activity " + this.GetValue(ActivityIdProperty).ToString());
                ManagementPackClass ActivityClass = emg.EntityTypes.GetClass(new Guid("42642d4f-d342-3f1b-965c-628a0f4119e2"));
                ManagementPackClass RequestClass = emg.EntityTypes.GetClass(new Guid("04b69835-6343-4de2-4b19-6be08c612989"));
                EnterpriseManagementObject ActivityObject = emg.EntityObjects.GetObject<EnterpriseManagementObject>(new Guid((string)this.GetValue(ActivityIdProperty)), ObjectQueryOptions.Default);
                IList<EnterpriseManagementRelationshipObject<EnterpriseManagementObject>> Objects = emg.EntityObjects.GetRelationshipObjectsWhereTarget<EnterpriseManagementObject>(ActivityObject.Id, ObjectQueryOptions.Default);
                foreach (EnterpriseManagementRelationshipObject<EnterpriseManagementObject> Object in Objects)
                {
                    if (Object.RelationshipId == new Guid("2da498be-0485-b2b2-d520-6ebd1698e61b"))
                    {
                        LogEvent("Found related SR " + Object.SourceObject.Id.ToString());

                        Object.SourceObject[new Guid("9ca6666e-e25e-244c-5d2d-94268017f4b1")].Value = ActivityObject[new Guid("029dd446-c76c-ab37-a105-235da4f979dd")].Value;
                        LogEvent("Activity Field Updated");
                        Object.Commit();
                    }
                }
                
            } catch (Exception ex)
            {
                LogEvent(ex.Message, 1);
            }
            finally
            {
                FlushLog(Log, 0);
            }



        }

        private void LogEvent(string message,int severity)
        {
            Log += message + "\r\n";
            if(severity == 1)
            {
                FlushLog(message, severity);
            }
        }

        private void LogEvent(string message)
        {
            LogEvent(message, 0);
        }

        private void FlushLog(string log,int severity)
        {
            if (!EventLog.SourceExists(this.Name))
                EventLog.CreateEventSource(this.Name, "Operations Manager");
            EventLogEntryType type;
            if(severity == 0)
            {
                type = EventLogEntryType.Information;
            }
            else
            {
                type = EventLogEntryType.Error;
            }
            EventLog.WriteEntry(this.Name, log, type, severity);
        }

        
    }

}
