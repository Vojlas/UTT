using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using System.IO;
using System.Data;
using UniversalTimerTool.Model;
using System.Windows.Forms;

namespace UniversalTimerTool.FilesController
{
    class FilesController
    {
        private string path { get; set; }
        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="path"></param>
        public FilesController()
        {
            this.path = @"projects\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Read XML at "path" as Dataset
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Dataset</returns>
        private DataSet readXMLasDataset(string path)
        {
            DataSet dataSet = new DataSet();
            try
            {
                dataSet.ReadXml(path);
            }
            catch (Exception)
            {
                return null;
            }
            return dataSet;
        }

        public int WriteProjecToProjectFolder(Project project)
        {
            DataSet ds = new DataSet { DataSetName = "Project" };
            DataTable dt = new DataTable { TableName = "ProjectMain" };
            DataColumn dc1 = new DataColumn("ProjectName"); dt.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn("Created"); dt.Columns.Add(dc2);
            DataColumn dc3 = new DataColumn("FileName"); dt.Columns.Add(dc3);
            DataColumn dc4 = new DataColumn("Description"); dt.Columns.Add(dc4);
            dt.Rows.Add(project.ProjektName, project.Created, project.FileName, project.Description);
            try
            {
                ds.Tables.Add(dt);
                addTables(ds, project.Updates);
                ds.WriteXml(path + project.FileName);
            }
            catch { return 1; }
            return 0;
        }

        /// <summary>
        /// Add tables to dataset
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="updates"></param>
        private void addTables(DataSet dataSet, List<Update> updates)
        {
            List<DataTable> dataTables = new List<DataTable>();
            int counter = 0;
            foreach (Update update in updates)
            {
                DataTable dt = new DataTable { TableName = "Update"+counter };
                DataColumn dc1 = new DataColumn("WorkTime"); dt.Columns.Add(dc1);
                DataColumn dc2 = new DataColumn("TrainTime"); dt.Columns.Add(dc2);
                DataColumn dc3 = new DataColumn("LastPricePerHour"); dt.Columns.Add(dc3);
                DataColumn dc4 = new DataColumn("UpdateName"); dt.Columns.Add(dc4);
                dt.Rows.Add(update.WorkTime.Ticks, update.TrainTime.Ticks, update.LastPricePerHour, update.UpdateName);

                dataTables.Add(dt);
                counter++;
            }

            foreach (DataTable dataTable in dataTables)
            {
                dataSet.Tables.Add(dataTable);
            }
        }

        public List<DataSet> Loadprojects()
        {
            string[] paths = Directory.GetFiles(path, "*.xml");
            List<DataSet> datasets = new List<DataSet>(path.Length);
            foreach (string path in paths)
            {
                if (readXMLasDataset(path) != null)
                {
                    datasets.Add(readXMLasDataset(path));
                }
            }
            return datasets;
        }

        
        //TODO: public void ImportProject()
        public Project ImportProject(){
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "XML soubory (*.xml) | *.xml";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!(readXMLasDataset(openFileDialog.FileName) == null))
                    {
                        try
                        {
                            DataSet dt = readXMLasDataset(openFileDialog.FileName);
                            return projectFromDataset(dt);
                        }
                        catch (Exception){}   
                    }
                }
                return null;
            }
        }

        private Project projectFromDataset(DataSet dataSet)
        {
            List<Update> updates = new List<Update>();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName != "ProjectMain")
                {
                    DataRow row = dataTable.Rows[0];
                    updates.Add(new Update(new DateTime(Convert.ToInt32(row["WorkTime"])), new DateTime(Convert.ToInt32(row["TrainTime"])), Convert.ToInt32(row["LastPricePerHour"]), Convert.ToString(row["UpdateName"])));
                }
            }

            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName == "ProjectMain")
                {
                    DataRow row = dataTable.Rows[0];
                    MessageBox.Show("Project: \""+ Convert.ToString(row["ProjectName"]) + "\" - has been imported");
                    return new Project(Convert.ToString(row["ProjectName"]), Convert.ToDateTime(row["Created"]), updates, Convert.ToString(row["Description"]));
                }
            }
            return null;
        }
    }
}
