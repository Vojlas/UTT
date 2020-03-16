using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalTimerTool.Model;

namespace UniversalTimerTool.Controller
{
    class MoneyManagerController
    {
        private MainWindow mainWindow { get; set; }
        public MoneyManagerController(MainWindow window)
        {
            this.mainWindow = window;
        }
        
        public void ReCalculateMoney(int updateNumber, List<Update> updates)
        {
            int multiplier = checkNumber(mainWindow.textBoxMoneyManagerMoneyInput.Text);
            mainWindow.labelMoneyManager_PriceWork.Content = updates.ElementAt(updateNumber).WorkTime.Hour + updates.ElementAt(updateNumber).WorkTime.Day*24 + updates.ElementAt(updateNumber).WorkTime.Minute/60;  //(updates.ElementAt(updateNumber).WorkTime.Day * 24 + updates.ElementAt(updateNumber).WorkTime.Hour + updates.ElementAt(updateNumber).WorkTime.Minute / 60 + updates.ElementAt(updateNumber).WorkTime.Second / 3600) * multiplier;
            mainWindow.labelMoneyManager_PriceTrain.Content = (updates.ElementAt(updateNumber).TrainTime.Day*24 + updates.ElementAt(updateNumber).TrainTime.Hour + updates.ElementAt(updateNumber).TrainTime.Minute/60 + updates.ElementAt(updateNumber).TrainTime.Second/3600) * multiplier;
        }

        private int checkNumber(string input)
        {
            int i = 0;  
            try{i = Convert.ToInt32(input);}
            catch (Exception){}
            return i;
        }
    }
}
