using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TranslatorTest1
{
    public partial class Form1 : Form
    {
        List<string> AlgCodeLines = new List<string>();
        List<string> SiPlusCodeLines = new List<string>();

        List<string> SiPlusCode_temp = new List<string>();
        bool NoKeys = true;
        bool SyntError = false;

        public Form1()
        {
            InitializeComponent();
            textBox_Alg.ReadOnly = true;
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
            string copyCode = textBox_Pascal.Text;
            textBox_Pascal.Text = "";
            textBox_Pascal.Text = copyCode;

            AlgCodeLines.Clear();
            SiPlusCodeLines.Clear();

            textBox_Alg.Text = "";
            

            int line = 0;
            string tempStr = "";
            foreach (string str in textBox_Pascal.Lines)
            {
                tempStr = str;
                ChangeKeys_PascalToAlg(tempStr, line);

                if (NoKeys == true) //не найдено ни одно совпадение из списка ключей
                {
                    MarkMistake(textBox_Pascal, tempStr);
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
                CheckSyntMistakes(); //Проверка на синтаксические ошибки

                if (SyntError == false) //если ошибок нет
                {
                    WriteAlgCodeToTextBox();
                    ChangeSynt_PascalToAlg(); //привести синтаксис к алгоритмическому языку
                    WriteAlgCodeToTextBox();
                }
                else
                {                    
                    MessageBox.Show("Проверка синтаксического анализа не пройдена"
                        , "Ошибка при синтаксическом анализе");
                }
            }          

                                 
            //for(int i = 0; i < textBox_Pascal.Lines.Count(); i++)
            //{
            //    for(int j = 0; )
            //}
            //Console.Write(arg[i].tostring)
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
            foreach (string line in textBox_Pascal.Lines)
            {
                SyntError = false;
                if (line.Contains(":=")) //проверка на правильность присваивания
                {
                    char[] words = line.ToCharArray();
                    int CharIndex = line.IndexOf(":=");
                    {
                        if (words[CharIndex + 2] == ';')
                        {
                            MarkMistake(textBox_Pascal, line);
                            MessageBox.Show("Похоже, что в строке " + line + " ' ошибка присваивания" + "\n" + "Проверьте, пожалуйста, код"
                            , "Ошибка при синтаксическом анализе");
                            SyntError = true;
                            break;
                        }
                        try
                        {
                            if (words[CharIndex - 1] == ' ')
                            {

                            }
                        }
                        catch
                        {
                            MarkMistake(textBox_Pascal, line);
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
            textBox_Alg.Text = "";
            for (int i = 0; i < AlgCodeLines.Count; i++)
            {
                textBox_Alg.Text += AlgCodeLines[i] + "\r\n";
            }
        }


        /// <summary>
        /// синтаксический анализ кода [Pascal -> Алг]
        /// </summary>
        public void ChangeSynt_PascalToAlg()
        {
            int line = 0;            

            foreach (string str in textBox_Alg.Lines)
            {
                ChangeCycle_PascalToAlg(str, line);
                line++;
            }
            line = 0;
            foreach (string str in textBox_Alg.Lines)
            {
                ChangeReadWrite_PascalToAlg(str, line);
                ChangeVarType_PascalToAlg(str, line);
                line++;
            }
        }

        /// <summary>
        /// поменять синтаксис объявления массивов
        /// </summary>
        public void ChangeArray_PascalToAlg() 
        {
            int line = 0;
            string code = "";
            foreach (string str in textBox_Pascal.Lines)
            {
                if(str.Contains("] of integer"))
                {
                    try
                    {
                        code = str.Replace("of integer", "");
                        code = code.Replace("array", "");
                        code = code.Replace(":", " ");
                        code = code.Replace("..", ":");
                        code = "ЦЕЛТАБ " + code;

                        AlgCodeLines[line] = code;
                    }
                    catch
                    {
                        AlgCodeLines.Add(code);
                    }
                }
                else
                if (str.Contains("] of float"))
                {
                    try
                    {
                        code = code.Replace("of float", "");
                        code = code.Replace("array", "");
                        code = code.Replace(":", " ");
                        code = code.Replace("..", ":");
                        code = "ВЕЩТАБ " + code;

                        AlgCodeLines[line] = code;
                    }
                    catch
                    {
                        AlgCodeLines.Add(code);
                    }
                }
                line++;
            }
        }


        /// <summary>
        /// поменять синтаксис ввода/вывода данных
        /// </summary>
        /// <param name="code"></param>
        /// <param name="line"></param>
        public void ChangeReadWrite_PascalToAlg(string code, int line)
        {
            if ((code.Contains("ВВОД"))||(code.Contains("ВЫВОД")) || (code.Contains("ВЫВОД_НС")) || (code.Contains("ВВОД_НС")))
            {
                code = code.Replace("(", " ");
                code = code.Replace(")", "");
                AlgCodeLines[line] = code;
            }
        }

        /// <summary>
        /// поменять синтаксис циклов
        /// </summary>
        /// <param name="code"></param>
        /// <param name="line"></param>
        public void ChangeCycle_PascalToAlg(string code, int line)
        {
            if ((code.Contains("НЦ ДЛЯ"))||(code.Contains("НЦ ПОКА")))
            {
                line++;

                for (int i = line; i < AlgCodeLines.Count; i++)
                {
                    if (!AlgCodeLines[i].Contains("КОН;"))
                    {
                        line++;
                    }
                    else
                    {
                        AlgCodeLines[i] = "КЦ КОН;";                        
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// поменять синтаксис объявления переменных
        /// </summary>
        /// <param name="code"></param>
        /// <param name="line"></param>
        public void ChangeVarType_PascalToAlg(string code, int line)
        {
            if (code.Contains("ДЛИНЦЕЛ"))
            {
                int index = code.IndexOf(":");
                AlgCodeLines[line] = VarType_PascalToAlg(index, code, "ДЛИНЦЕЛ");
            }
            else
            if ((code.Contains("ЦЕЛ")) && (!code.Contains("ЦЕЛТАБ")))
            {
                int index = code.IndexOf(":");                      
                AlgCodeLines[line] = VarType_PascalToAlg(index, code, "ЦЕЛ");
            }
            else
            if ((code.Contains("ВЕЩ")) && (!code.Contains("ВЕЩТАБ")))
            {
                int index = code.IndexOf(":");
                AlgCodeLines[line] = VarType_PascalToAlg(index, code, "ВЕЩ");
            }
        }
        public string VarType_PascalToAlg(int indexStart, string code, string type)
        {
            code = code.Remove(indexStart);
            code = type + " " + code + ";";

            return code;
        }




        /// <summary>
        /// поменять синтаксис массивов
        /// </summary>
        /// <param name="code"></param>
        /// <param name="line"></param>
        public void ChangeArray_Alg(string code, int line)
        {
            if((code.Contains("int"))&&(code.Contains("[")))
            {
                int indexSkobka = code.IndexOf("[");
                int indexDoubleDot = code.IndexOf(":");
                code = code.Remove(indexSkobka, indexDoubleDot-indexSkobka);
                code = code.Replace(":", "[");

                SiPlusCodeLines[line] = code;              
            }
        }

        /// <summary>
        /// поменять синтаксис циклов
        /// </summary>
        /// <param name="code"></param>
        /// <param name="line"></param>
        public void ChangeCycle_Alg(string code, int line)
        {
            if(code.Contains("for"))
            {
                code = code.Replace("НЦ", "");
                code = code.Replace("for", "");
                code = "int" + code;
                code = code.Replace("ОТ", "=");
                code = code.Replace("ДО", "; i < ");
                code = "for(" + code + ";i++)";

                SiPlusCodeLines[line] = code;
            }
            if (code.Contains("while"))
            {
                code = code.Replace("НЦ", "");
                code = code.Replace("while", "");
                code = "while( " + code + " )";

                SiPlusCodeLines[line] = code;
            }
            if(code.Contains("}."))
            {
                code = code.Replace(".", "");

                SiPlusCodeLines[line] = code;
            }
        }

        /// <summary>
        /// поменять синтаксис условного оператора
        /// </summary>
        /// <param name="code"></param>
        /// <param name="line"></param>
        public void ChangeIF_Alg(string code, int line)
        {
            if(code.Contains("if"))
            {
                code = code.Replace("if", "");
                code = "if( " + code + " )";

                SiPlusCodeLines[line] = code;
            }
        }


        /// <summary>
        /// лексический анализ кода Pascal
        /// </summary>
        /// <param name="code"></param>
        /// <param name="line"></param>
        public void ChangeKeys_PascalToAlg(string code, int line)
        {
            NoKeys = true;
            if (code.Contains("if"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ЕСЛИ", "if");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            if (code.Contains("then"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ТО", "then");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            if (code.Contains("else"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ИНАЧЕ", "else");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            if (code.Contains("and"))
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
            if (code.Contains("or")&& (!code.Contains("for")))
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
            if (code.Contains("not"))
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
            if (code.Contains("true"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ДА", "true");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            if (code.Contains("false"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ДА", "true");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }

            if (code.Contains("for"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("НЦ ДЛЯ", "for");
                    code = code.Replace("НАЧ", "begin");
                    code = code.Replace(" ОТ ", ":=");
                    code = code.Replace("ДО", "to");
                    code = code.Replace("", "do");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            if (code.Contains("while"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("НЦ ПОКА", "while");
                    code = code.Replace("НАЧ", "begin");
                    code = code.Replace("do", "");
                    code = code.Replace("И", "and");
                    code = code.Replace("ИЛИ  ", "or");                  
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }

            if (code.Contains("] of integer"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("", "of integer");
                    code = code.Replace("", "array");
                    code = code.Replace("", "array");
                    code = code.Replace(":", "..");
                    code = "ЦЕЛТАБ " + code;

                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            else
            if (code.Contains("] of float"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("", "of float");
                    code = code.Replace("", "array");
                    code = code.Replace(" ", ":");
                    code = code.Replace(" ", ":");
                    code = "ВЕЩТАБ " + code;

                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            else

            if (code.Contains("program"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("program", "АЛГ");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            else
            //типы переменных----------------------------
            if (code.Contains("var"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ПЕР", "var");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            else
            if ((code.Contains("longint"))&& (!code.Contains("array")))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ДЛИНЦЕЛ", "longint");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            else
            if (code.Contains("integer")&&(!code.Contains("array")))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ЦЕЛ", "integer");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            else
            if (code.Contains("float") && (!code.Contains("array")))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ВЕЩ", "float");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            else
            if (code.Contains("string") && (!code.Contains("array")))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("СТР", "string");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            else
            //----------------------------------

            //Начало/Конец-------------------------------------
            if (code.Contains("begin"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("НАЧ", "begin");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            else
            if (code.Contains("end"))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("КОН", "end");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            else
            //------------------------------------------


            //ввод/вывод 
            if ((code.Contains("write")) || (code.Contains("writeln")))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ВЫВОД_НС", "writeln");
                    code = code.Replace("ВЫВОД", "write");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            else
            if ((code.Contains("read")) || (code.Contains("readln")))
            {
                NoKeys = false;
                try
                {
                    code = code.Replace("ВВОД_НС", "readln");
                    code = code.Replace("ВВОД", "read");
                    AlgCodeLines[line] = code;
                }
                catch
                {
                    AlgCodeLines.Add(code);
                }
            }
            else
            //----------------------------------------------------------

            if (code.Contains(":="))
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
        /// Загрузка кода Pascal из файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_LoadFromFile_Click(object sender, EventArgs e)
        {
            textBox_Pascal.Clear();
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
                            textBox_Pascal.Text += line + "\r\n";

                            label_LoadFileName.Text = "Файл: " + name;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// сохранение Алг.кода в файл
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
                File.WriteAllText(sfd.FileName, textBox_Alg.Text);

        }

     

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
