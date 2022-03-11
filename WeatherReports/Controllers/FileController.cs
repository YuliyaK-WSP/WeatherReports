using Microsoft.AspNetCore.Mvc;
using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using WeatherReports.Domain;
using WeatherReports.ViewModels;
using NPOI.HSSF.UserModel;
using WeatherReports.DAL;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using WeatherReports.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using WeatherReports.DAL.Context;
using WeatherReports.Domain.Entities.Base;

namespace WeatherReports.Controllers
{
    public class FileController : Controller
    {
       private IConfiguration Configuration;
        WeatherReportsContext db;
       public FileController(IConfiguration _configuration, WeatherReportsContext context)
        {
            Configuration = _configuration;
            this.db = context;
        }
        /*public FileController(WeatherReportsContext context)
        {
            this.db = context;
        }*/

        [HttpPost]
        public ActionResult Import(IFormFileCollection uploads)
        {
            foreach (var uploadedFile in uploads)
            {



                IWorkbook Workbook;


               
                try
                {
                    using (var fileStream = uploadedFile.OpenReadStream())
                    {
                        //XSSFWorkbook подходит для формата XLSX, HSSFWorkbook подходит для формата XLS.
                        //string fileExt = uploadedFile.FileName.Substring(uploadedFile.FileName.LastIndexOf('.') + 1, uploadedFile.FileName.Length)

                        //var strFileType = strFileName.substring(strFileName.lastIndexOf('.') + 1, strFileName.length);
                        string fileExt = Path.GetExtension(uploadedFile.FileName).ToLower();
                        if (fileExt == ".xls")
                        {
                            Workbook = new HSSFWorkbook(fileStream);
                        }
                        else if (fileExt == ".xlsx")
                        {
                            Workbook = new XSSFWorkbook(fileStream);
                        }
                        else
                        {
                            Workbook = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //Положение на первом листе
                
                
                   
                    int count = Workbook.NumberOfSheets;
                for(int k = 0; k < count; k++)
                {
                    ISheet sheet = Workbook.GetSheetAt(k);
                    DataTable table = new DataTable();
                    //Первая строка - это строка заголовка
                    IRow headerRow = sheet.GetRow(2);
                    int cellCount = headerRow.LastCellNum;
                    int rowCount = sheet.LastRowNum;
                    //Добавить столбец заголовка в цикл
                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                        table.Columns.Add(column);
                    }

                    //данные
                    for (int i = (sheet.FirstRowNum + 4); i <= rowCount; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        DataRow dataRow = table.NewRow();
                        if (row != null)
                        {
                            for (int j = row.FirstCellNum; j < cellCount; j++)
                            {
                                if (row.GetCell(j) != null)
                                {
                                    dataRow[j] = GetCellValue(row.GetCell(j));
                                }
                            }
                        }
                        table.Rows.Add(dataRow);
                    }

                    DataTable excelTable = new DataTable();
                    excelTable = table;

                    DataTable dbdata = new DataTable();
                    dbdata.Columns.Add("Data");
                    dbdata.Columns.Add("Time");
                    dbdata.Columns.Add("Temperature");
                    dbdata.Columns.Add("Humidity");
                    dbdata.Columns.Add("Td");
                    dbdata.Columns.Add("AtmosphericPressure");
                    dbdata.Columns.Add("WindDirection");
                    dbdata.Columns.Add("WindSpeed");
                    dbdata.Columns.Add("Cloudiness");
                    dbdata.Columns.Add("H");
                    dbdata.Columns.Add("VV");
                    dbdata.Columns.Add("WeatherPhenomena");

                    for (int i = 0; i < excelTable.Rows.Count; i++)
                    {
                        DataRow dr = excelTable.Rows[i];
                        DataRow dr_ = dbdata.NewRow();
                        dr_["Data"] = dr["Дата"];
                        dr_["Time"] = dr["Время"];
                        dr_["Temperature"] = dr["Т"];
                        dr_["Humidity"] = dr["Отн. влажность"];
                        dr_["Td"] = dr["Td"];
                        dr_["AtmosphericPressure"] = dr["Атм. давление,"];
                        dr_["WindDirection"] = dr["Направление"];
                        if (dr["Скорость"].ToString().Trim() == "")
                        {
                            dr_["WindSpeed"] = 0;
                        }
                        else
                        {
                            dr_["WindSpeed"] = dr["Скорость"];
                        }
                        
                        if (dr["Облачность,"].ToString().Trim() == "")
                        {
                            dr_["Cloudiness"] = 0;
                        }
                        else
                        {
                            dr_["Cloudiness"] = Convert.ToInt32(dr["Облачность,"]);
                        }
                        //dr_["Cloudiness"] = dr["Облачность,"];
                        if (dr["h"].ToString().Trim() == "")
                        {
                            dr_["H"] = 0;
                        }
                        else
                        {
                            dr_["H"] = Convert.ToInt32(dr["h"]);
                        }
                        if (dr["VV"].ToString().Trim() == "")
                        {
                            dr_["VV"] = 0;
                        }
                        else
                        {
                            dr_["VV"] = Convert.ToString(dr["VV"]);
                        }

                        dr_["WeatherPhenomena"] = Convert.ToString(dr["Погодные явления"]);


                        dbdata.Rows.Add(dr_);
                    }
                    RemoveEmpty(dbdata);

                    //string constr = System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString;
                    //string constr = System.Configuration.ConfigurationManager.AppSettings["MyDefaultConnection"];
                    //string constr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog= WeatherReports.db;";
                    string constr = Configuration.GetConnectionString("DefaultConnection");
                    SqlBulkCopyByDatatable(constr, "Weather", dbdata);

                }

            }
            return View();
        }
        /*public ActionResult Archive(string month, string year)
        {
            string s1 = month;//
            string s2 = year;//
            using (WeatherReportsContext db = new WeatherReportsContext())
            {
                var wdwd = db.WeatherReports;
            }
                //string constr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog= WeatherReports.db;";
                string constr = Configuration.GetConnectionString("DefaultConnection");
            string inSql;
            if(s1 == null && s2 == null)
            {
                inSql = "SELECT * FROM Weather ORDER BY Data, Time";
            }
            else
            {
                s1 = s1 ?? "null";
                s2 = s2 ?? "null";
                inSql = $"SELECT * FROM Weather WHERE ({s1} IS NULL OR SUBSTRING(data, 4, 2) = {s1}) AND ({s2} IS NULL OR SUBSTRING(data, 7, 4) = {s2}) ORDER BY Data, Time";
            }
            var weatherList = new List<WeatherViewModel>();
            using(SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand(inSql, conn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    var weatherRow = new WeatherViewModel();
                    weatherRow.Data = Convert.ToString(sqlReader["Data"]);
                    weatherRow.Time = Convert.ToString(sqlReader["Time"]);
                    weatherRow.Temperature = Convert.ToString(sqlReader["Temperature"]);
                    weatherRow.Humidity= Convert.ToString(sqlReader["Humidity"]);
                    weatherRow.Td = Convert.ToString(sqlReader["Td"]);
                    weatherRow.AtmosphericPressure = Convert.ToString(sqlReader["AtmosphericPressure"]);
                    weatherRow.WindDirection = (string)sqlReader["WindDirection"];
                    weatherRow.WindSpeed = (string)sqlReader["WindSpeed"];
                    weatherRow.Cloudiness = (string)sqlReader["Cloudiness"];
                    weatherRow.H = (string)sqlReader["H"];
                    weatherRow.VV = (string)sqlReader["VV"];
                    weatherRow.WeatherPhenomena = Convert.ToString(sqlReader["WeatherPhenomena"]);



                    weatherList.Add(weatherRow);
                }
            }
            IEnumerable<WeatherViewModel> model = weatherList as IEnumerable<WeatherViewModel>;
            return View(model);
        }*/
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
            {
                return string.Empty;
            }

            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                case CellType.Unknown:
                default:
                    return cell.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }
        protected void RemoveEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool IsNull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        IsNull = false;
                    }
                }
                if (IsNull)
                {
                    removelist.Add(dt.Rows[i]);
                }
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
        }
        public static void SqlBulkCopyByDatatable(string connectionString, string TableName, DataTable dtSelect)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    try
                    {
                        sqlbulkcopy.DestinationTableName = TableName;
                        sqlbulkcopy.BatchSize = 20000;
                        sqlbulkcopy.BulkCopyTimeout = 0;//Неограниченное время
                        for (int i = 0; i < dtSelect.Columns.Count; i++)
                        {
                            sqlbulkcopy.ColumnMappings.Add(dtSelect.Columns[i].ColumnName, dtSelect.Columns[i].ColumnName);
                        }
                        sqlbulkcopy.WriteToServer(dtSelect);
                    }
                    catch (System.Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }
        public async Task<IActionResult> Archive(int page = 1, string month = "0", string year = "0")
        {
            string s1 = month;
            string s2 = year;
            int pageSize = 10;

            IQueryable<Weather> source = db.WeatherReports;
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            ArchiveViewModel viewModel = new ArchiveViewModel
            {
                PageViewModel = pageViewModel,
                Weathers = items
            };
            return View(viewModel);
        }
    }
}
