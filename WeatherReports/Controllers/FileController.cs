using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using WeatherReports.logic;
using Microsoft.Data.SqlClient;

namespace WeatherReports.Controllers
{
    public class FileController : Controller
    {
        [HttpPost]
        public ActionResult Import(IFormFileCollection uploads)
        {
            foreach (var uploadedFile in uploads)
            {

                //string path = Path.GetDirectoryName(uploadedFile.op);
                /*var fileName = file.FileName;
                var filePath = Server.MapPath(string.Format("~/{0}", "Files"));
                string path = Path.Combine(filePath, fileName);
                file.SaveAs(path);*/

                IWorkbook Workbook;
                DataTable table = new DataTable();
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
                ISheet sheet = Workbook.GetSheetAt(0);
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
                dbdata.Columns.Add("email");
                dbdata.Columns.Add("pwd");
                dbdata.Columns.Add("logintime");

                for (int i = 0; i < excelTable.Rows.Count; i++)
                {
                    DataRow dr = excelTable.Rows[i];
                    DataRow dr_ = dbdata.NewRow();
                    dr_["email"] = dr["почтовый ящик"];
                    dr_["pwd"] = dr["пароль"];
                    dr_["logintime"] = dr["время"];
                    dbdata.Rows.Add(dr_);
                }
                RemoveEmpty(dbdata);

                string constr = System.Configuration.ConfigurationManager.AppSettings["meixinEntities_"];

                SqlBulkCopyByDatatable(constr, "m_user1", dbdata);
            }
            return View();
        }
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
    }
}
