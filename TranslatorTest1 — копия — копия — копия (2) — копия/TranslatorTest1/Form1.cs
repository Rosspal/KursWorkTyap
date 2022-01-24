using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TranslatorTest1
{
    public partial class Form1 : Form
    {
        List<string> AlgCodeLines = new List<string>();

        bool NoKeys = true;
        bool SyntError = false;
        int count = 0;

        public Form1()
        {
            InitializeComponent();
            textBox_Pascal.ReadOnly = true;
        }

        private void buttonTranslate_Click(object sender, EventArgs e)
        {
            FromPascalToAlg(); //Перевести код с Pascal на Алгоритмический язык
        }

        /// <summary>
        /// Перевести код с Pascal на алгоритмический язык
        /// </summary>
        public void FromPascalToAlg()
        {
            count = 0;
            string copyCode = textBox_Alg.Text;
            textBox_Alg.Text = "";
            textBox_Alg.Text = copyCode;

            AlgCodeLines.Clear();

            textBox_Pascal.Text = "";


            int line = 0;
            string tempStr = "";
            foreach (string str in textBox_Alg.Lines)
            {
                tempStr = str;
                ChangeKeys_AlgToPascal(tempStr, line);

                if (NoKeys == true) //не найдено ни одно совпадение из списка ключей
                {
                    MarkMistake(textBox_Alg, tempStr);
                    MessageBox.Show("Похоже, что в строке №" + line.ToString() + " ' " + tempStr.ToString() + " ' ошибка " + "\n" + "Проверьте, пожалуйста, код"
                        , "Ошибка при лексическом анализе");

                    MessageBox.Show("Проверка лексического анализа не пройдена"
                    , "Ошибка при лексическом анализе");

                    break;
                }

                line++;
            }
            if (NoKeys == false)
            {

                if (SyntError == false) //если ошибок нет
                {
                    WriteAlgCodeToTextBox();
                }
                else
                {
                    MessageBox.Show("Проверка синтаксического анализа не пройдена"
                        , "Ошибка при синтаксическом анализе");
                }
            }
        }




        /// <summary>
        /// Подстветить ошибку красным цветом
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="line"></param>
        public void MarkMistake(RichTextBox rtb, string line)
        {
            int start = rtb.Find(line);     //найти начальную точку кода, где была ошибка
            rtb.Select(start, line.Length); //выделить код от начальной точки до конца ошибочного кода
            rtb.SelectionColor = Color.Red; //перекрасить выделенное в красный цвет
        }

        /// <summary>
        /// Проверка на синтаксические ошибки
        /// </summary>
        public void CheckSyntMistakes()
        {
            foreach (string line in textBox_Alg.Lines)
            {
                SyntError = false;
                if ((line.Contains(":=")) || (line.Contains("+=")) || (line.Contains("-="))) //проверка на правильность присваивания
                {
                    char[] words = line.ToCharArray();
                    int CharIndex = line.IndexOf(":=");
                    {

                        try
                        {
                            if (words[CharIndex + 2] == ';')
                            {
                                MarkMistake(textBox_Alg, line);
                                MessageBox.Show("Похоже, что в строке " + line + " ' ошибка присваивания" + "\n" + "Проверьте, пожалуйста, код"
                                , "Ошибка при синтаксическом анализе");
                                SyntError = true;
                                break;
                            }
                        }
                        catch
                        {
                            MarkMistake(textBox_Alg, line);
                            MessageBox.Show("Похоже, что в строке " + line + " ' ошибка присваивания" + "\n" + "Проверьте, пожалуйста, код"
                            , "Ошибка при синтаксическом анализе");
                            SyntError = true;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Вывести на экран полученный алгоритмический код
        /// </summary>
        public void WriteAlgCodeToTextBox()
        {
            textBox_Pascal.Text = "";
            for (int i = 0; i < AlgCodeLines.Count; i++)
            {
                textBox_Pascal.Text += AlgCodeLines[i] + "\r\n";
            }
        }

        /// <summary>
        /// пробельчики
        /// </summary>
        /// <param name="code"></param>
        public string RefactoringCode(string code)
        {
            code = code.Trim();

            for (int i = 0; i != count; i++)
            {
                    code = code.Insert(0, " ");
            }

            if (code.Contains("begin"))
            {
                count++;
            }
            else
            {
                if (code.Contains("end"))
                {
                    count--;
                }
            }
            return code;
        }



        /// <summary>
        /// лексический анализ кода Pascal
        /// </summary>
        /// <param name="code"></param>
        /// <param name="line"></param>
        public void ChangeKeys_AlgToPascal(string code, int line)
        {
            NoKeys = true;
            if (code.Contains("ЕСЛИ"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ЕСЛИ", "if");
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            if (code.Contains("ТО"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ТО", "then");
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            if (code.Contains("ИНАЧЕ"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ИНАЧЕ", "else");
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            if (code.Contains("И"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("И", "and");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            if (code.Contains("ИЛИ"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ИЛИ", "or");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            if (code.Contains("НЕ"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("НЕ", "not"); 
                    AlgCodeLines[line] = code;
                }
                catch
                {
                   
                    AlgCodeLines.Add(code);
                }
            }
            if (code.Contains("ДА"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ДА", "true");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code)); 
                }
            }

            if (code.Contains("НЦ ДЛЯ"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("НЦ ДЛЯ", "for");
                    code = code.Replace("НАЧ", "begin");
                    code = code.Replace(" ОТ ", ":=");
                    code = code.Replace("ДО", "to");
                    code = code + " do";
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code)); 
                }
            }
            if (code.Contains("НЦ ПОКА"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("НЦ ПОКА", "while");
                    code = code.Replace("НАЧ", "begin");
                    code = code.Replace("И", "and");
                    code = code.Replace("ИЛИ", "or");
                    code = code + " do";
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code)); 
                }
            }

            if (code.Contains("ЦЕЛТАБ"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ЦЕЛТАБ", "");
                    code = code.Replace(":", "..");
                    code = code.Replace("[", ":array[");
                    code = code.Replace("]", "] of integer");

                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            else
            if (code.Contains("ВЕЩТАБ"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ВЕЩТАБ", "");
                    code = code.Replace(":", "..");
                    code = code.Replace("[", ":array[");
                    code = code.Replace("]", "] of real");

                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            else

            if (code.Contains("АЛГ"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("АЛГ", "program");
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            else
            //типы переменных----------------------------
            if (code.Contains("ПЕР"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ПЕР", "var");
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            else
            if (code.Contains("ЦЕЛ"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ЦЕЛ", "");
                    code = code.Replace(";", ":integer;");
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            else
            if (code.Contains("ВЕЩ"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ВЕЩ", "");
                    code = code.Replace(";", ":real;");
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            else
            if (code.Contains("СТР"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("СТР", "");
                    code = code.Replace(";", ":string;");
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            else
            //----------------------------------

            //Начало/Конец-------------------------------------
            if (code.Contains("НАЧ"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("НАЧ", "begin");
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            else
            if (code.Contains("КОН"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("КОН", "end");
                    code = code.Replace("КЦ", "");
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            else
            //------------------------------------------


            //ввод/вывод 
            if ((code.Contains("ВЫВОД")) || (code.Contains("ВЫВОД_НС")))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ВЫВОД_НС", "writeln(");
                    code = code.Replace("ВЫВОД", "write(");
                    code = code.Replace(";", ");");
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            else
            if ((code.Contains("ВВОД")) || (code.Contains("ВВОД_НС")))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ВВОД_НС", "readln(");
                    code = code.Replace("ВВОД", "read(");
                    code = code.Replace(";", ");");
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            else
            //----------------------------------------------------------

            if (code.Contains(":="))
            {
                NoKeys = false;
                try
                {
                    AlgCodeLines[line] = RefactoringCode(code);
                }
                catch
                {
                    AlgCodeLines.Add(RefactoringCode(code));
                }
            }
            else
            if (code == "")
            {
                NoKeys = false;
                try
                {
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
        }



        /// <summary>
        /// Загрузка кода из файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_LoadFromFile_Click(object sender, EventArgs e)
        {
            textBox_Alg.Clear();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog.FileName;
                    string name = Path.GetFileName(openFileDialog.FileName);
                    using (StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            textBox_Alg.Text += line + "\r\n";

                            label_LoadFileName.Text = "Файл: " + name;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// сохранение кода в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_SaveAlg_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
                File.WriteAllText(sfd.FileName + ".pas", textBox_Pascal.Text);

        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox_Alg_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
