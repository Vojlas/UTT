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
            Update currentUpdate = updates.ElementAt(updateNumber);

            double PriceWork = currentUpdate.Work.Hours + currentUpdate.Work.Minutes / 60.0;                            
            mainWindow.labelMoneyManager_PriceWork.Content = Math.Round(PriceWork*multiplier,2) + " Kč";

            double PriceTrain = currentUpdate.Train.Hours + currentUpdate.Train.Minutes / 60;
            mainWindow.labelMoneyManager_PriceTrain.Content = Math.Round(PriceTrain*multiplier,2) + " Kč";
        }

        private int checkNumber(string input)
        {
            string b = String.Join("", input.Where(char.IsDigit));
            int i = 0;
            try { i = Convert.ToInt32(b); }
            catch (Exception) { }
            return i;
        }
    }
}
