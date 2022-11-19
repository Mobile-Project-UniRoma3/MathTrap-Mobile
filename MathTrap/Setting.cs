using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTrap
{
    public class OperatorBonus
    {
        private string bonus;
        public string Bonus   // property
        {
            get { return bonus; }   // get method
            set { bonus = value; }  // set method
        }
    }

    public class Setting
    {
        private ClassSQL value;       
        private OperatorBonus bonus;
        const string BONUS_DEFAULT = "x";
        private string[] operators;

        public Setting(ClassSQL value)
        {
            //assegno la connessione aperta
            this.value = value;
            this.bonus = new OperatorBonus();
        }

        public OperatorBonus getBonus() {
            return this.bonus;
        }

        public string ConstBonus() {
            return BONUS_DEFAULT;
        }

        async public Task<List<string>> scomposed()
        {
            List<string> l = new List<string>();

            //Esiste un recordo di setting per la partita
            
            var setting_ = await value.GetSettingLoad(this.value.score.IdSetting);
            
            if (setting_ != null)
            {
                l= await getListSetting(setting_.text);
                //bonus
                this.bonus.Bonus = setting_.bonus;
            }
            else
            {
                l = await getListOperator();
                this.bonus.Bonus = BONUS_DEFAULT;
            }

            return l;
        }

        async public Task<List<TableTempSetting>> disposeListOperator()
        {
            //azzero la tab temporanea dei settaggi
            this.value.DeleteTempSettAllAsync();

            //evoco i settaggi reali della partita
            var listTempA = new List<string>(await scomposed());

            //inserisco i settaggi in una tab temp
            foreach (var l in listTempA)
            {
                this.value.settTemp.ID = 0;
                this.value.settTemp.text = l.ToString();
                this.value.settTemp.check = true;
                this.value.settTemp.bonus = this.bonus.Bonus;
                //-->settaggi selezionati true
                this.value.SaveTempSettingAsync(this.value.settTemp);
            }
            
            var operazioni = await this.value.GetAllOperAsync(); 
            

            foreach (var o in operazioni)
            {
                if (!listTempA.Contains(o.text))
                {
                    this.value.settTemp.ID = 0;
                    this.value.settTemp.text = o.text;
                    this.value.settTemp.check = false;
                    this.value.settTemp.bonus = this.bonus.Bonus;
                    //-->settaggi selezionati false
                    this.value.SaveTempSettingAsync(this.value.settTemp);
                }
            }
            var ritorno = await this.value.GetTempSettingAllAsync();
            return ritorno;
        }

        async public Task<List<string>> getListOperator()
        {
            List<string> l = new List<string>();
           
            var operazioni =  await this.value.GetSqlAllOperAsync();

            foreach (var o in operazioni)
            {
                l.Add(o.text);
            }

            return l;
        }

        async public Task<List<string>> getListSetting(string s)
        {
            List<string> l= new List<string>();
            string str;

            await this.value.getTaskDelayAsync(10);

            str = s;
            int i = str.Length;
            int k = 0;
            //operandi
            while (i > 0)
            {
                l.Add(str.Substring(k, 1));
                i--;
                k++;
            }

            return l;
        }

        async public Task AggiornaSettings()
        {
            try { 
            var l = new List<string>();

            var setting_ = await this.value.GetSettingLoad(this.value.score.IdSetting);

            int count = 0;

            //Esiste un recordo di setting per la partita
            if (setting_ != null)
            {
                l = await this.getListSetting(setting_.text);
                this.operators = new string[(l.Count)];
                //bonus
                foreach (var o in l)
                {
                    this.operators[count] = l.ToString();
                    count++;
                }
                this.getBonus().Bonus = setting_.bonus;
            }
            else
            {
                await this.AggOper();
            }
            } catch (System.NullReferenceException e) { _ = Task.CompletedTask; }
        }

        async public Task AggOper()
        {
            try { 
            var list = await this.getListOperator();
            
            if (list != null) { 
                int i = list.Count(); 
            }

            var lst = new List<string>();
            int count = 0;

            foreach (var o in list)
            {
                //i primi 4 operandi sono di default
                if (count <= 3)
                {
                    lst.Add(o.ToString());
                }
                count++;
            }
            count = 0;
            this.operators = new string[(lst.Count + 1)];
            this.getBonus().Bonus = this.ConstBonus();
            foreach (var b in lst)
            {
                this.operators[count] = (b.ToString());
                count++;
            }

            //-->operatore bonus di default
            this.operators[count] = (this.ConstBonus());
            } catch (System.NullReferenceException e) { _ = Task.CompletedTask; }
        }
        public string[] returnOper() { return this.operators; }
    }
}
