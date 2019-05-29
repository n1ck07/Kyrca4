using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace Baggage_FlightDB
{
    public partial class StateFormDb : Form
    {
        /*Создаем объект нашей базы данных который будет хранит наши ведденые свденья о Багажах*/
        DataBase BaggageDB = new DataBase();
        /*это перменная нужная для таблиц которая отоброжает текущий номер багажа*/
        int currBaggageNumber;

        public StateFormDb()
        {
            InitializeComponent();
            // присваииваем начальное значение 0
            currBaggageNumber = 0;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Выводим информацию о том, кто сделал работу 
            MessageBox.Show("Baggage info program");
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            /*Проверка на заполнение полей, если хотябы одно не заполнено то выдаст сообщение о том, что поля не заполенены*/
            if (txtName.Text == "" || txtSurname.Text == "" || txtLastName.Text == "" ||
                txtFlightNumber.Text == "" || txtBaggageTicket.Text == "" || txtBaggageAmount.Text == ""
                || txtBaggageWeight.Text == "")

            {
                /*В данных ифах как раз проверется каждое поле по отдельности*/

                if (txtName.Text == "")
                    MessageBox.Show("Input Name");

                if (txtSurname.Text == "")
                    MessageBox.Show("Input SurName");

                if (txtLastName.Text == "")
                    MessageBox.Show("Input LastName");

                if (txtFlightNumber.Text == "")
                    MessageBox.Show("Input Flight Number");

                if (txtBaggageTicket.Text == "")
                    MessageBox.Show("Input Ticket");

                if (txtBaggageAmount.Text == "")
                    MessageBox.Show("Input Baggage amount");

                if (txtBaggageWeight.Text == "")
                    MessageBox.Show("Input Baggage weight");

            }
            /*Етот блок кода отвечает за корректность вводимых значений, ниже буду булевые функции которые принимают параметр в виде 
             строки и использует проверки корректности.*/
            else if (CorrectName(txtName.Text) || CorrectName(txtSurname.Text) ||
                CorrectName(txtLastName.Text) || CorrectFlightNumber(txtFlightNumber.Text) ||
                CorrectTicket(txtBaggageTicket.Text) || CorrectBaggagePlace(txtBaggageAmount.Text)
                || CorrectBaggageWeight(txtBaggageWeight.Text) || GenerateUniqueData(Baggage) == true)
            {
                if (CorrectName(txtName.Text))
                    MessageBox.Show($"Here {txtName.ToString()}");

                if (CorrectName(txtSurname.Text))
                    MessageBox.Show($"Here {txtSurname.ToString()}");

                if (CorrectName(txtLastName.Text))
                    MessageBox.Show($"Here {txtLastName.ToString()}");

                if (CorrectFlightNumber(txtFlightNumber.Text))
                    MessageBox.Show($"Here {txtFlightNumber.ToString()}");

                if (CorrectTicket(txtBaggageTicket.Text))
                    MessageBox.Show($"Here {txtBaggageTicket.ToString()}");

                if (CorrectBaggagePlace(txtBaggageAmount.Text))
                    MessageBox.Show($"Here {txtBaggageAmount.ToString()}");

                if (CorrectBaggageWeight(txtBaggageWeight.Text))
                    MessageBox.Show($"Here {txtBaggageWeight.ToString()}");

                if (GenerateUniqueData(Baggage) == true)
                    MessageBox.Show("We have duplicate in Ticket");
            }
            /*Представим что у нас все хорошо и мы заполнили все и корректно. То тогда данные отправляются в две таблицы которые связаны
             между собой, таблица для просмотра и таблица для редактирования во второй вкладке формы, там ты сможешь отредактировать
             если что не так. В первой таблице где ты и заполняешь поля только смотреть можешь*/
            else
            {
                currBaggageNumber++;

                Baggage.Rows.Add(currBaggageNumber, txtName.Text, txtSurname.Text, txtLastName.Text,
                    txtFlightNumber.Text, txtBaggageTicket.Text,txtBaggageAmount.Text,txtBaggageWeight.Text);

                BaggageEdit.Rows.Add(currBaggageNumber, txtName.Text, txtSurname.Text, txtLastName.Text,
                    txtFlightNumber.Text, txtBaggageTicket.Text, txtBaggageWeight.Text, txtBaggageWeight.Text);

                BaggageDB.Db.Add(new BaggageOwnerInfo(txtName.Text, txtSurname.Text, txtLastName.Text,
                     Convert.ToUInt32(txtFlightNumber.Text), txtBaggageTicket.Text,Convert.ToUInt32(txtBaggageAmount.Text),
                     Convert.ToInt32(txtBaggageWeight.Text)));                
            }
        }
        /*Этот метод создан для проверки входной строки на число, знак пунктуации и системный символ*/
        bool CorrectName(string input)
        {
            bool result = false;
            foreach (char item in input)
            {
                if (Char.IsNumber(item))
                    result = true;
                else if (Char.IsPunctuation(item))
                    result = true;
                else if (Char.IsSymbol(item))
                    result = true;
            }
            return result;
        }
        /*Этот метод нужен для проверки строки полетного номера. Входное значение не должно содержать символов, знаков пунктуаций, 
         * букв*/
        bool CorrectFlightNumber(string input)
        {
            bool result = false;
            foreach (char item in input)
            {
                if (Char.IsPunctuation(item))
                    result = true;
                else if (Char.IsSymbol(item))
                    result = true;
                else if (Char.IsLetter(item))
                    result = true;
                else if (input.Length > 10)
                    result = true;
            }
            return result;
        }

        bool CorrectBaggagePlace(string input)
        {
            bool result = false;
            foreach (char item in input)
            {
                if (Char.IsPunctuation(item))
                    result = true;
                else if (Char.IsSymbol(item))
                    result = true;
                else if (Char.IsLetter(item))
                    result = true;
                else if (input.Length > 2)
                    result = true;
                else if (Convert.ToUInt32(txtBaggageAmount.Text) > 10 || Convert.ToUInt32(txtBaggageAmount.Text) <= 0)
                    result = true;
            }
            return result;
        }

        bool CorrectBaggageWeight(string input)
        {
            bool result = false;
            foreach (char item in input)
            {
                if (Char.IsPunctuation(item))
                    result = true;
                else if (Char.IsSymbol(item))
                    result = true;
                else if (Char.IsLetter(item))
                    result = true;
                else if (input.Length > 3)
                    result = true;
                else if (Convert.ToInt32(txtBaggageWeight.Text) > 100 || Convert.ToInt32(txtBaggageWeight.Text) <= 0)
                    result = true;
            }
            return result;
        }
        /*Метод проверки на номера билета(первый символ должна быть буква), сверяться с высше указанными комментами для методов*/
        bool CorrectTicket(string input)
        {
            bool result = false;
            foreach (char item in input)
            {
                if (Char.IsPunctuation(item))
                    result = true;
                else if (Char.IsSymbol(item))
                    result = true;
                else if (Char.IsLetter(item))
                    result = true;
                else if (input.Length > 7)
                    result = true;
                if (Char.IsLetter(input[0]))
                    result = false;
                else
                    result = true;
            }
            return result;
        }
        /*Метод для сохранения отредактированных данных с таблицы во второй вкладке(точнее функция в кнопке)*/
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            /*Тут мы через цикл получаем конкретно выбранную строку и записываем все измененные данные в массив str, тем самым 
             если все корректно исправленно отправляется в таблицу для просмотра. Тут так же есть булевская перменная которая отвечает
             за то, выполняется ли проверка, если true так и отсается поле испрввляется путем формирования в массиве str новой строки 
             и отправляет ее в новом виде в таблицу просмотра.*/
            for (int i = 0; i < currBaggageNumber; i++)
            {
                bool result = true;
                string[] str = new string[8];
                for (int j = 1; j < Baggage.ColumnCount; j++)
                {
                    /*Каждый иф проверяет одну из 7 ячеек на итерации, если проверки типа CorrectName не выполняется то происходит 
                     вызов бокса сообщений, и так для кажого поля в таблице пока не введешь правильно значения*/

                    if ((j == 1 || j == 2 || j == 3) && CorrectName(BaggageEdit.Rows[i].Cells[j].Value.ToString()))
                    {
                        result = false;
                        MessageBox.Show("Invalid Name");
                    }

                    if (j == 4 && CorrectFlightNumber(BaggageEdit.Rows[i].Cells[j].Value.ToString()))
                    {
                        result = false;
                        MessageBox.Show("Invalid Flight Number");
                    }

                    if (j == 5 && CorrectTicket(BaggageEdit.Rows[i].Cells[j].Value.ToString()))
                    {
                        result = false;
                        MessageBox.Show("Invalid Ticket Number");
                    }

                    if (j == 6 && CorrectBaggagePlace(BaggageEdit.Rows[i].Cells[j].Value.ToString()))
                    {
                        result = false;
                        MessageBox.Show("Invalid Baggege Places");
                    }

                    if (j == 7 && CorrectBaggageWeight(BaggageEdit.Rows[i].Cells[j].Value.ToString()))
                    {
                        result = false;
                        MessageBox.Show("Invalid Baggage weight");
                    }
                    /*В конце цикла формируется массив с всеми новыми полями для опредленной строки в которой мы исправляли значения*/
                    str[j] = BaggageEdit.Rows[i].Cells[j].Value.ToString();
                }
                /*Если ошибки нигде не возникло то новые данные для таблицы просмотра мы берем с массива str и присваиваем его конкретным
                 полям нашей таблицы для просмтотра (ещё раз скажу, эта та, которая идет первая при загрузке приложения)*/
                if (result)
                {
                    for (int k = 1; k < 8; k++)
                    {
                        Baggage.Rows[i].Cells[k].Value = str[k];
                    }
                    BaggageDB.Db.ElementAt(i).Name = BaggageEdit.Rows[i].Cells[1].Value.ToString();
                    BaggageDB.Db.ElementAt(i).SurName = BaggageEdit.Rows[i].Cells[2].Value.ToString();
                    BaggageDB.Db.ElementAt(i).LastName = BaggageEdit.Rows[i].Cells[3].Value.ToString();
                    BaggageDB.Db.ElementAt(i).FlightNumber = Convert.ToUInt32(BaggageEdit.Rows[i].Cells[4].Value.ToString());
                    BaggageDB.Db.ElementAt(i).BaggageTicket = BaggageEdit.Rows[i].Cells[5].Value.ToString();
                    BaggageDB.Db.ElementAt(i).AmountOfBaggage = Convert.ToUInt32(BaggageEdit.Rows[i].Cells[6].Value.ToString());
                    BaggageDB.Db.ElementAt(i).BaggageWeight = Convert.ToInt32(BaggageEdit.Rows[i].Cells[7].Value.ToString());
                }
            }
        }
        /*Эта штука нужна чтобы выйти с приложения и не более*/
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BaggageSearch.Rows.Clear();
            /*Этот метод ищет челов у которых один рейс, если такие есть выводит их в таблицу,
             * если нет, то ничего
             В конце поле ввода очищается от информации в нем. */
            if (!String.IsNullOrWhiteSpace(txtFlightSearch.Text))
            {
                int count = 0;
                int weightSum = 0;
                for (int i = 0; i < currBaggageNumber; i++)
                {
                    if (txtFlightSearch.Text == BaggageDB.Db.ElementAt(i).FlightNumber.ToString())
                    {
                        count++;
                        BaggageSearch.Rows.Add(count,
                            BaggageDB.Db.ElementAt(i).Name,
                            BaggageDB.Db.ElementAt(i).SurName,
                            BaggageDB.Db.ElementAt(i).LastName,
                            BaggageDB.Db.ElementAt(i).FlightNumber,
                            BaggageDB.Db.ElementAt(i).BaggageTicket,
                            BaggageDB.Db.ElementAt(i).AmountOfBaggage,
                            BaggageDB.Db.ElementAt(i).BaggageWeight);
                    }

                    weightSum += int.Parse(BaggageSearch.Rows[i].Cells["BaggageWeightSearch"].Value.ToString());
                }
                MessageBox.Show($"{weightSum} overall weight for this flight");
                txtBaggageTicketSearch.Text = "";
            }
            /*Этот поиск делает Номеру тикета багажа, делает все тоже самое что и прошлый метод, с различием в то, что ищет по номеру.*/
            else if (!String.IsNullOrWhiteSpace(txtBaggageTicketSearch.Text))
            {
                int count = 0;
                uint str = 0;
                for (int i = 0; i < currBaggageNumber; i++)
                {
                    if (txtBaggageTicketSearch.Text == BaggageDB.Db.ElementAt(i).BaggageTicket)
                    {
                        count++;
                        BaggageSearch.Rows.Add(count,
                            BaggageDB.Db.ElementAt(i).Name,
                            BaggageDB.Db.ElementAt(i).SurName,
                            BaggageDB.Db.ElementAt(i).LastName,
                            BaggageDB.Db.ElementAt(i).FlightNumber,
                            BaggageDB.Db.ElementAt(i).BaggageTicket,
                            BaggageDB.Db.ElementAt(i).AmountOfBaggage,
                            BaggageDB.Db.ElementAt(i).BaggageWeight);
                    }
                }
                str = Convert.ToUInt32(BaggageSearch.Rows[0].Cells["FlightSearch"].Value);

                MessageBox.Show($"In this flight {str} you can find this baggage {txtBaggageTicket.Text.ToString()}");
                txtBaggageTicketSearch.Text = "";
            }
        }
        /*Эта кнопка самая интересная, она удаляет челов по заданной LastName*/
        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < currBaggageNumber; i++)
            {
                currBaggageNumber--;
            }

            Baggage.Rows.Clear();
            BaggageEdit.Rows.Clear();
            BaggageSearch.Rows.Clear();

            if (!String.IsNullOrWhiteSpace(txtDeleteLastName_RemakeFlag.Text))
            {
                for (int j = 0; j < currBaggageNumber; j++)
                {
                    if (txtDeleteLastName_RemakeFlag.Text == BaggageDB.Db.ElementAt(j).LastName)
                    {
                        Baggage.Rows.Add(j + 1, BaggageDB.Db.ElementAt(j).Name, BaggageDB.Db.ElementAt(j).SurName, BaggageDB.Db.ElementAt(j).LastName,
                                BaggageDB.Db.ElementAt(j).FlightNumber, BaggageDB.Db.ElementAt(j).BaggageTicket, BaggageDB.Db.ElementAt(j).AmountOfBaggage,
                                BaggageDB.Db.ElementAt(j).BaggageWeight);

                        BaggageEdit.Rows.Add(j + 1, BaggageDB.Db.ElementAt(j).Name, BaggageDB.Db.ElementAt(j).SurName, BaggageDB.Db.ElementAt(j).LastName,
                                          BaggageDB.Db.ElementAt(j).FlightNumber, BaggageDB.Db.ElementAt(j).BaggageTicket, BaggageDB.Db.ElementAt(j).AmountOfBaggage,
                                          BaggageDB.Db.ElementAt(j).BaggageWeight);
                    }
                }
            }
        }

        /*Эта кнопка служит для записи информации в файл*/
        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Ключевое слово using нужно не только для включения пространства имен, но и ещё для штук которые рализуют
             интерфейс IDisposable. Тебе попадает объект с которым ты делаешь все нужные действия и потом 
             освобождаешь память для него, как бы резирвируя постоянную под объекты которые ты реализуешь.
             В скобка[ ты открываешь поток для записи текста, создавая файл на рабочем столе info со стандартной кодировкой для 
             твоей ОС*/
            using (TextWriter writer = new StreamWriter(File.Create($@"C:\Users\{SystemInformation.UserName}\Desktop\info.txt"),Encoding.Default))
            {
                /*В блоке try/catch/finally мы отлавливаем все возмодные ошибки которые можем во время выполнения.
                 Одного из самых важных, что если таблица у тебя пустая то запись не будет происходить, будет выдавать бокс 
                 оповещения о том , что необходимо заполнить таблицу*/
                try
                {
                    if(Baggage.Rows.Count - 1 != 0)
                    {
                        for (int i = 0; i < Baggage.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j < Baggage.Columns.Count; j++)
                            {
                                /*Тут когда доходим до последнего столбца то записываем элемент и после него начинаес с новой строки, 
                                 а так записываем в виде таблицы*/
                                if (j == 6)
                                    writer.Write(Baggage.Rows[i].Cells[j].Value.ToString() + '\n');
                                else
                                    writer.Write(Baggage.Rows[i].Cells[j].Value.ToString() + '\t');
                            }
                        }
                        MessageBox.Show("Info cashed");
                    }
                    else
                    {
                        MessageBox.Show("Need more info");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                /*Этот блок выполняется в любом случае, независимо от того, была ли успешно выполнена оперция или нет*/
                finally
                {
                    writer.Close();
                }
            }
        }
        /*Тут просто считываешь данные из этого файла, если надумаешь его перенести в другую папку то выбьет ошибку
         так делать не стоит! Потом ты открываешь потоk считки информации и записывает в отдельный кеш-лист данные с таблицы (прошлого
         запуска)*/
        private void readFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TextReader text = new StreamReader(File.OpenRead($@"C:\Users\{SystemInformation.UserName}\Desktop\info.txt"), Encoding.Default))
            {
                try
                {
                    // слово скип означает пропускать все табулирующие знаки, которые мы ставили при записи между элементами,
                    // кроме последнего, там мы начинали с новой строки
                    foreach (var item in text.ReadLine().Skip('\t'))
                    {
                        cacheBoxSession.Text += item.ToString();
                    }
                    MessageBox.Show("Information has been added");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);              
                }
                finally
                {
                    text.Close();
                }
            }            
        }

        private void btnSearchPassenger_Click(object sender, EventArgs e)
        {
            BaggageSearch.Rows.Clear();
           // ищешь по весу
                int count = 0;
                for (int i = 0; i < currBaggageNumber; i++)
                {
                    if (Convert.ToInt32(BaggageDB.Db.ElementAt(i).BaggageWeight) >= 30)
                    {
                        count++;
                        BaggageSearch.Rows.Add(count,
                            BaggageDB.Db.ElementAt(i).Name,
                            BaggageDB.Db.ElementAt(i).SurName,
                            BaggageDB.Db.ElementAt(i).LastName,
                            BaggageDB.Db.ElementAt(i).FlightNumber,
                            BaggageDB.Db.ElementAt(i).BaggageTicket,
                            BaggageDB.Db.ElementAt(i).AmountOfBaggage,
                            BaggageDB.Db.ElementAt(i).BaggageWeight);
                    }
                }
                txtFlightSearch.Text = "";
        }
        // методы для проверки на корректность тикетов, а именно предотвращение сходести тикетов
        private bool GenerateUniqueData(DataGridView dataBase)
        {
            bool check = false;
            for (int i = 0; i < dataBase.Rows.Count; i++)
            {
                for (int j = 0; j < dataBase.Columns.Count; j++)
                {
                    if (dataBase.Rows[i].Cells[j].Value != null && txtBaggageTicket.Text == dataBase.Rows[i].Cells[j].Value.ToString())
                    {
                        bool temp = true;
                        check = temp;
                        break;
                    }
                }
            }
            return check;
        }        
    }
}