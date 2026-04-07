using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using BookCatalogAPI.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;


namespace BookCatalogAPI.Logs
{
    public static class LogTxt
    {
        public static void LogToFile(string request, string requestBody, int responseStatus, string responseBody)
        {

            try
            {
                string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs-app");
                Directory.CreateDirectory(basePath);


                string suffix = responseStatus >= 400 ? "-error" : ""; 
                string fileName = Path.Combine(basePath, DateTime.Now.ToString("ddMMyyyy") + suffix + ".txt");

                StreamWriter swLog = File.Exists(fileName)
                    ? File.AppendText(fileName)
                    : new StreamWriter(fileName);


                swLog.WriteLine("Log: ");
                swLog.WriteLine(DateTime.Now.ToLocalTime());

                swLog.WriteLine(request);
                swLog.WriteLine(requestBody);

                swLog.WriteLine($"StatusCode: {responseStatus}");
                swLog.WriteLine($"{responseBody}");

                swLog.WriteLine("");
                swLog.Close(); 

            }
            catch(Exception ex)
            {
                Console.WriteLine("Error ao gravar log: " + ex.Message);
            }

        }       
    }
}