using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace W32local
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            this.AutoLog = false;
            
            
            if (!EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource(
                "MySource", "MyLog");
            }
            eventLog1.Source = "MySource";
            
        }

        protected override void OnStart(string[] args)
        {
            ProcessStartInfo p = new ProcessStartInfo();
            DateTime.Now.ToShortTimeString();
            DateTime dt = DateTime.Now;
            eventLog1.WriteEntry("Active: active (running) since " + dt.DayOfWeek.ToString() + dt.ToLocalTime() + " " + dt.Kind.ToString());
            try
            {  
                p.FileName = "W32local1.exe";
                p.WorkingDirectory = @"C:\WService";
                Process.Start(p);
            }
            catch (Exception e)
            {
                eventLog1.WriteEntry(e.Message);
            }

        }

        protected override void OnStop()
        {
            DateTime.Now.ToShortTimeString();
            DateTime dt = DateTime.Now;
            try
            {
                foreach (Process proc in Process.GetProcessesByName("W32local1"))
                {
                    proc.Kill();
                }
            }
            catch(Exception e)
            {
                eventLog1.WriteEntry(e.Message);
            }
            eventLog1.WriteEntry("Active: active (stop) since " + dt.DayOfWeek.ToString() + dt.ToLocalTime() + " " + dt.Kind.ToString());
        }
    }
}
