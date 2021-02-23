using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalTimerTool.Model;

namespace UniversalTimerTool.CryptoController
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
            Update currUpd = updates.ElementAt(updateNumber);
            double PriceWork = currUpd.WorkTime.Hour +
                               (currUpd.WorkTime.Day-1)*24 +
                               currUpd.WorkTime.Minute/60.0;
                            
            mainWindow.labelMoneyManager_PriceWork.Content = PriceWork*multiplier;

            double PriceTrain = (currUpd.TrainTime.Day-1) * 24 +
                                currUpd.TrainTime.Hour +
                                currUpd.TrainTime.Minute / 60.0;
            mainWindow.labelMoneyManager_PriceTrain.Content = PriceTrain*multiplier;
        }

        private int checkNumber(string input)
        { 
            int i = 0;
            try { i = Convert.ToInt32(input); }    //input);}
            catch (Exception) { }
            return i;
        }
    }
}
