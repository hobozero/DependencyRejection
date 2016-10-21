﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationCatalog
{
    public enum SolarWinds
    {
        Yes,
        No,
        Available,
        NotAvailable
    }

    public enum AppType
    {
        Unknown,
        ScheduledTask,
        ManualConsole,
        Web,
        WindowsService,
        WPF,
        WebService,
    }
    
    public class DeployedApplication
    {
        static Regex regexIp = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
        public DeployedApplication()
        {
            this.DeployedType = AppType.Unknown;
            this.Schedules = new List<string>();
            this.DeployedLocations = new List<string>();
            this.SolarWinds = SolarWinds.NotAvailable;
            Ip = String.Empty;
            Port = 0;
        }
        public string OctoName { get; set; }
        public string BusinessArea { get; set; }
        public AppType DeployedType { get; set; }
        public string Description { get; set; }
        public SolarWinds SolarWinds { get; set; }
        public string Logs { get; set; }
        public string UserAccount { get; set; }
        public string Ip { get; set; }
        public int? Port { get; set; }
        public List<string> Schedules { get; set; }
        public List<string> DeployedLocations { get; set; }



        public void SetAppType(string field)
        {
            field = field.ToLower().Trim();

            if (field.Contains("scheduled"))
            {
                this.DeployedType = AppType.ScheduledTask;
            }
            else if (field.Contains("windows") && field.Contains("service"))
            {
                this.DeployedType = AppType.WindowsService;
            }
            else if (field.Contains("web") && field.Contains("service"))
            {
                this.DeployedType = AppType.WebService;
            }
            else if (field.Contains("website") || field.Contains("iis") || field.Contains("subapplication"))
            {
                this.DeployedType = AppType.Web;
            }
            else if (field.Contains("console"))
            {
                this.DeployedType = AppType.ManualConsole;
            }
            else
            {
                throw new Exception($"Undefined type {field}");
            }
        }

        public void SetIpPort(string Ip, string port)
        {
            if (regexIp.IsMatch(Ip))
            {
                this.Ip = Ip;
            }

            if (this.DeployedType != AppType.ScheduledTask)
            {
                int portNum = 80;
                int.TryParse(port, out portNum);
                this.Port = portNum;
            }
        }

        public void AddSchedule(string schedule)
        {
            if (this.DeployedType == AppType.ScheduledTask)
            {
                this.Schedules.Add(schedule);
            }
        }
        public void SetSolarWinds(string solarWinds)
        {
            solarWinds = solarWinds.ToLower().Trim();
            if (this.DeployedType != AppType.ScheduledTask)
            {
                if ("true" == solarWinds || "yes" == solarWinds)
                {
                    this.SolarWinds = SolarWinds.Yes;
                }
                else if ("false" == solarWinds || "no" == solarWinds)
                {
                    this.SolarWinds = SolarWinds.No;
                }
                else if ("available" == solarWinds )
                {
                    this.SolarWinds = SolarWinds.Available;
                }
            }
        }
    }
    
}