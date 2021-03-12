using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using UniversalTimerTool.Model;
using System.Data;

namespace UniversalTimerTool.DataController
{
    class DataController
    {
        public Project projectFromDataset(DataSet dataSet)
        {
            List<Update> updates = new List<Update>();

            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName != "ProjectMain")
                {
                    DataRow row = dataTable.Rows[0];
                    //Try Catch!!!
                    updates.Add(new Update(new Time((string)row["WorkTime"]), new Time((string)row["TrainTime"]), Convert.ToInt32(row["LastPricePerHour"]),row["UpdateName"].ToString()));
                }
            }
            foreach (DataTable dataTable in dataSet.Tables)
            {
                if (dataTable.TableName == "ProjectMain")
                {
                    DataRow row = dataTable.Rows[0];
                    return new Project((row["ProjectName"].ToString()), Convert.ToDateTime(row["Created"]), updates,row["Description"].ToString());
                }
            }
            return null;
        }
    }
}
