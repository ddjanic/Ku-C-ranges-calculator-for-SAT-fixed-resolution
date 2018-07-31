

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GoogleMaps.LocationServices;

namespace calcSAT
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // максимизируем окно и не даем ему изменять размеры для сохранения удобства 
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // расставляем элементы по сетке на форме для удобочитаемости (1 список)
            //int Y_loc_1st_list = 12;
            //for (int Iterator_1st_list=1; Iterator_1st_list <= 30; Iterator_1st_list++)
            //{
            //(Controls["label" + Iterator_1st_list] as Label).Location = new Point(6, Y_loc_1st_list);
            //Y_loc_1st_list += 24;
            //}
            // расставляем элементы по сетке на форме для удобочитаемости (2 список)
            //int Y_loc_2nd_list = 276;
            //for (int Iterator_2nd_list = 31; Iterator_2nd_list <= 50; Iterator_2nd_list++)
            //{
            //(Controls["label" + Iterator_2nd_list] as Label).Location = new Point(600, Y_loc_2nd_list);
            //Y_loc_2nd_list += 24;
            //}
            // расставляем элементы по сетке на форме для удобочитаемости (широта и долгота) - текстовые значения
            //int Y_loc_3th_list = 12;
            // for (int Iterator_3th_list = 51; Iterator_3th_list <= 57; Iterator_3th_list++)
            //{
            //(Controls["label" + Iterator_3th_list] as Label).Location = new Point(600, Y_loc_3th_list);
            //Y_loc_3th_list += 36;
            //}
            // расставляем элементы по сетке на форме для удобочитаемости (широта и долгота) - поля для значений editbox
            //Y_loc_3th_list = 12;
            //for (int Iterator_3th_list = 1; Iterator_3th_list <= 2; Iterator_3th_list++)
            //{
            //(Controls["textBox" + Iterator_3th_list] as TextBox).Location = new Point(660, Y_loc_3th_list);
            //Y_loc_3th_list += 30;
            //}
            //Y_loc_3th_list = 120;
            //for (int Iterator_3th_list = 3; Iterator_3th_list <= 4; Iterator_3th_list++)
            //{
            //(Controls["textBox" + Iterator_3th_list] as TextBox).Location = new Point(660, Y_loc_3th_list);
            //Y_loc_3th_list += 30;
            //}

            // расставляем элементы по сетке на форме для удобочитаемости (широта и долгота) - кнопки гео локации
            //buttonGeoLocate.Location = new Point(825, 12);
            //buttonGeoClear.Location = new Point(825, 42);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // расставляем элементы по сетке на форме для удобочитаемости
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // цветовое выделение полей при инициализации формы для ведения пользователя по необходимым полям для заполнения

            // поля широты и долготы при инициализации
            this.textBox1.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox2.BackColor = Color.FromArgb(192, 255, 192);

            // фокус на поле страна
            this.ActiveControl = textBox1;

        }

        private void buttonGeoClear_Click(object sender, EventArgs e)
        {

            // очистка текстовых полей для страны и города
            for (int iterator = 1; iterator <= 8; iterator++)
            {
                if (iterator == 5) iterator++;
                (tableLayoutPanel1.Controls["textBox" + iterator.ToString()] as TextBox).Text = "";
            }

            // очистка цветового выделения полей гео-позиционирования
            for (int iterator = 1; iterator <= 8; iterator++)
            {
                if (iterator == 5) iterator++;
                (tableLayoutPanel1.Controls["textBox" + iterator.ToString()] as TextBox).BackColor = Color.White;
            }


            // фокус на поле страна

            this.ActiveControl = textBox1;

            ///////////////////////////////////////////////////////////
            // ставим индикатор на поле Страна
            this.textBox1.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле Город
            this.textBox2.BackColor = Color.FromArgb(192, 255, 192);
            ///////////////////////////////////////////////////////////
            // очистка всех зн-ий расчётов (Данные о спутнике (позиция спутника))
            this.comboBox1.Text = "";
            this.comboBox1.BackColor = Color.White;
            
            ///////////////////////////////////////////////////////////
        }



        private void buttonGeoLocate_Click(object sender, EventArgs e)
        {
            // принимаем страну и город как значения для сервиса geoLocation
            var address = textBox2.Text + ", " + textBox1.Text;
            // подключаем сервис geoLocation
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);
            // принимаем широту и долготу
            var latitude = point.Latitude;
            var longitude = point.Longitude;
            // отдаем значения широты и долготы - editbox
            textBox3.Text = latitude.ToString();
            textBox4.Text = longitude.ToString();
            // цветовое выделение полей при расчете гео-позиционирования страны и города 
            this.textBox1.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox2.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox3.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox4.BackColor = Color.FromArgb(192, 255, 192);

            // основной расчет lan / long - цвето-позиционирование
            this.textBox6.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox7.BackColor = Color.FromArgb(192, 255, 192);
            //
            this.textBox6.Text = Convert.ToString(Math.Truncate(Convert.ToDouble(this.textBox3.Text)));
            this.textBox7.Text = Convert.ToString(Math.Truncate(Convert.ToDouble(this.textBox4.Text)));
        }

        // сделаем коллбэк для данных пинга 8.8.8.8
        public static void Callback8888(object state)
        {
            var ping = new Ping();
            var ipAddress = IPAddress.Parse((String)state);

            var pingReply = ping.Send(ipAddress, 1000);

            (Application.OpenForms[0] as FormMain).Invoke((MethodInvoker)(delegate ()
            {
                (Application.OpenForms[0] as FormMain).toolStripStatusLabel2.Text = "[ " + DateTime.UtcNow.ToString() + " ] [ " + ipAddress + " ] [ " + pingReply.RoundtripTime + " ms ] [ " + pingReply.Status + " ]";
            }));
        }

        // сделаем коллбэк для данных пинга 8.8.4.4
        public static void Callback8844(object state)
        {
            var ping = new Ping();
            var ipAddress = IPAddress.Parse((String)state);

            var pingReply = ping.Send(ipAddress, 1000);

            (Application.OpenForms[0] as FormMain).Invoke((MethodInvoker)(delegate ()
            {
                (Application.OpenForms[0] as FormMain).toolStripStatusLabel4.Text = "[ " + DateTime.UtcNow.ToString() + " ] [ " + ipAddress + " ] [ " + pingReply.RoundtripTime + " ms ] [ " + pingReply.Status + " ]";
            }));
        }

        public void FormMain_Shown(object sender, EventArgs e)
        {

            var period = 10 * 1000;
            // активизируем таймер пинга публичных гугл dns 8.8.8.8
            string ipAddress8888 = "8.8.8.8";
            System.Threading.Timer t = new System.Threading.Timer(Callback8888, ipAddress8888, 0, period);
            // активизируем таймер пинга публичных гугл dns 8.8.4.4
            string ipAddress8844 = "8.8.4.4";
            System.Threading.Timer t2 = new System.Threading.Timer(Callback8844, ipAddress8844, 0, period);

            // делаем по дефолту изначально работу с Ku - диапазоном
            this.radioButton1.PerformClick();

            ///////////////////////////////////////////////////////////
            // ставим индикатор на поле Страна
            this.textBox1.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле Город
            this.textBox2.BackColor = Color.FromArgb(192, 255, 192);
            ///////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////
            // ставим индикатор на поле Потери из-за неточного наведения антенны
            this.textBox80.Text = "0,6";
            // ставим индикатор на поле Потери из-за неточного наведения антенны
            this.textBox80.BackColor = Color.FromArgb(192, 255, 192);
            ///////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////
            // ставим индикатор на поле - Диаметр антенны(d) - поле 1
            this.textBox28.Text = "0,5";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 1
            this.textBox28.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 2
            this.textBox41.Text = "0,6";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 2
            this.textBox41.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 3
            this.textBox43.Text = "0,8";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 3
            this.textBox43.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 4
            this.textBox45.Text = "1,0";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 4
            this.textBox45.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 5
            this.textBox47.Text = "1,2";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 5
            this.textBox47.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 6
            this.textBox49.Text = "1,4";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 6
            this.textBox49.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 7
            this.textBox51.Text = "1,8";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 7
            this.textBox51.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 8
            this.textBox53.Text = "2,0";
            // ставим индикатор на поле Диаметр антенны(d) - поле 8
            this.textBox53.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 9
            this.textBox55.Text = "2,5";
            // ставим индикатор на поле Диаметр антенны(d) - поле 9
            this.textBox55.BackColor = Color.FromArgb(192, 255, 192);
            ///////////////////////////////////////////////////////////
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            // цветовое выделение поля при расчете гео-позиционирования спутника 

            this.comboBox1.BackColor = Color.FromArgb(192, 255, 192);

        }

        private void button2_Click(object sender, EventArgs e)
        {


            //this.comboBox1.BackColor = Color.White;
            //this.comboBox1.Text = "";

            // очистка всех зн-ий расчётов - поля для значений textbox
            for (int iterator = 1; iterator <= 160; iterator++)
            {
                if (iterator == 5) iterator++;
                if (iterator == 26) iterator++;
                if (iterator == 82) iterator++;
                (tableLayoutPanel1.Controls["textBox" + iterator.ToString()] as TextBox).Text = "";
            }

            // обнуление цветовых индикаторов полей textBox
            for (int iterator = 1; iterator <= 160; iterator++)
            {
                if (iterator == 5) iterator++;
                if (iterator == 26) iterator++;
                if (iterator == 82) iterator++;
                (tableLayoutPanel1.Controls["textBox" + iterator.ToString()] as TextBox).BackColor = Color.White;
            }
            ///////////////////////////////////////////////////////////
            // ставим индикатор на поле Страна
            this.textBox1.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле Город
            this.textBox2.BackColor = Color.FromArgb(192, 255, 192);
            ///////////////////////////////////////////////////////////
            // очистка всех зн-ий расчётов (Данные о спутнике (позиция спутника))
            this.comboBox1.Text = "";
            this.comboBox1.BackColor = Color.White;
            
            ///////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////
            // ставим индикатор на поле Потери из-за неточного наведения антенны
            this.textBox80.Text = "0,6";
            // ставим индикатор на поле Потери из-за неточного наведения антенны
            this.textBox80.BackColor = Color.FromArgb(192, 255, 192);
            ///////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////
            // ставим индикатор на поле - Диаметр антенны(d) - поле 1
            this.textBox28.Text = "0,5";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 1
            this.textBox28.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 2
            this.textBox41.Text = "0,6";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 2
            this.textBox41.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 3
            this.textBox43.Text = "0,8";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 3
            this.textBox43.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 4
            this.textBox45.Text = "1,0";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 4
            this.textBox45.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 5
            this.textBox47.Text = "1,2";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 5
            this.textBox47.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 6
            this.textBox49.Text = "1,4";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 6
            this.textBox49.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 7
            this.textBox51.Text = "1,8";
            // ставим индикатор на поле - Диаметр антенны(d) - поле 7
            this.textBox51.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 8
            this.textBox53.Text = "2,0";
            // ставим индикатор на поле Диаметр антенны(d) - поле 8
            this.textBox53.BackColor = Color.FromArgb(192, 255, 192);

            // ставим индикатор на поле - Диаметр антенны(d) - поле 9
            this.textBox55.Text = "2,5";
            // ставим индикатор на поле Диаметр антенны(d) - поле 9
            this.textBox55.BackColor = Color.FromArgb(192, 255, 192);
            ///////////////////////////////////////////////////////////

            // фокус на поле Страна

            this.ActiveControl = textBox1;

            // очистка всех зн-ий расчётов (Полоса пропускания приемника (ширина радиоканала)(B))
            this.comboBox3.Text = "";
            this.comboBox3.BackColor = Color.White;
            

            ///////////////////////////////////////////////////////////
        }
 


        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

            // ставим индикатор на поле Отношение радиуса  орбиты к радиусу экватора Земли
            this.textBox10.BackColor = Color.FromArgb(192, 255, 192);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            // цветовое выделение поля при расчете гео-позиционирования спутника 

            this.textBox8.Text = this.comboBox1.Text;

            this.textBox8.BackColor = Color.FromArgb(192, 255, 192);

            // расчет - восточная долгота земной станции минус долгота спутника

            this.textBox9.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox9.Text = Convert.ToString(Convert.ToDouble(this.textBox7.Text) - Convert.ToDouble(this.textBox8.Text));

            // фокус на поле Отношение радиуса  орбиты к радиусу экватора Земли

            this.ActiveControl = textBox10;
        }

        // Конвертация градусов в радианы (обычная функция)
        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        // Конвертация радиан в градусы (обычная функция)
        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // готовим поля для работы - Косинус А
            double rad = Convert.ToDouble(this.textBox6.Text) * (Math.PI / 180.0);
            double val = Math.Cos(rad);
            this.textBox11.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле Косинус А
            this.textBox11.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Косинус B
            rad = Convert.ToDouble(this.textBox9.Text) * (Math.PI / 180.0);
            val = Math.Cos(rad);
            this.textBox12.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле Косинус B
            this.textBox12.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Квадрат косинуса А
            rad = (Convert.ToDouble(this.textBox6.Text) * 2) * (Math.PI / 180.0);
            val = Math.Cos(rad);
            this.textBox13.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле Квадрат косинуса А
            this.textBox13.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Квадрат косинуса В
            rad = (Convert.ToDouble(this.textBox9.Text) * 2) * (Math.PI / 180.0);
            val = Math.Cos(rad);
            this.textBox14.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле Квадрат косинуса В
            this.textBox14.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Корень
            val = Math.Sqrt(1 - Convert.ToDouble(this.textBox13.Text) * Convert.ToDouble(this.textBox14.Text));
            this.textBox15.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле Корень
            this.textBox15.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы -  Числитель
            val = Convert.ToDouble(this.textBox10.Text) * Convert.ToDouble(this.textBox11.Text) * Convert.ToDouble(this.textBox12.Text) - 1;
            this.textBox16.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле Числитель
            this.textBox16.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы -  Знаменатель
            val = Convert.ToDouble(this.textBox10.Text) * Math.Sqrt(1 - Convert.ToDouble(this.textBox13.Text) * Convert.ToDouble(this.textBox14.Text));
            this.textBox17.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле Знаменатель
            this.textBox17.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы -  Арктангенс
            val = Convert.ToDouble(Math.Atan(Convert.ToDouble(this.textBox16.Text) / Convert.ToDouble(this.textBox17.Text)));
            this.textBox18.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле Арктангенс
            this.textBox18.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы -  Угол места (угол возвышения)
            val = Convert.ToDouble(this.textBox18.Text);
            double degrees = val * (180 / Math.PI);
            this.textBox19.Text = Convert.ToString(degrees.ToString("F2"));
            // ставим индикатор на поле Угол места (угол возвышения)
            this.textBox19.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы -  Для низких углов EL
            val = Convert.ToDouble(Convert.ToDouble(this.textBox19.Text) + Math.Sqrt(Math.Pow(Convert.ToDouble(this.textBox19.Text), 2)) + 4.132) / 2;
            this.textBox20.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле Для низких углов EL
            this.textBox20.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы -  Азимут
            val = 180 + RadianToDegree(Math.Atan(Math.Tan(DegreeToRadian(Convert.ToDouble(this.textBox9.Text)))) / Math.Sin(DegreeToRadian(Convert.ToDouble(this.textBox6.Text))));
            this.textBox21.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Азимут
            this.textBox21.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            // ставим индикатор на поле - Магнитное склонение
            this.textBox22.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // готовим поля для работы - Магнитное склонение & Магнитный азимут
            double val = Convert.ToDouble(this.textBox21.Text) + Convert.ToDouble(this.textBox22.Text);
            this.textBox40.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Магнитное склонение & Магнитный азимут
            this.textBox40.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Наклонная дальность (D)
            val = 6378.16 * Math.Sqrt(Convert.ToDouble(this.textBox10.Text) * Convert.ToDouble(this.textBox10.Text) + 1 - 2 * Math.Cos(DegreeToRadian(Convert.ToDouble(this.textBox6.Text))) * Math.Cos(DegreeToRadian(Convert.ToDouble(this.textBox9.Text))));
            this.textBox23.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Наклонная дальность (D)
            this.textBox23.BackColor = Color.FromArgb(192, 255, 192);
        }

        // оповещаем пользователя пользователя, что будем работать с Ku диапазоном
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.label83.Text = "Ku";
            this.comboBox2.Text = "12000000000";
            this.comboBox2.BackColor = Color.FromArgb(192, 255, 192);

            // в Ku диапазоне не исп. 
        }

        // оповещаем пользователя пользователя, что будем работать с Ku диапазоном
        private void radioButton1_Click(object sender, EventArgs e)
        {
            this.label83.Text = "Ku";
            this.comboBox2.Text = "12000000000";
            this.comboBox2.BackColor = Color.FromArgb(192, 255, 192);

            // в Ku диапазоне не исп. 

        }

        // оповещаем пользователя пользователя, что будем работать с C диапазоном
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.label83.Text = "C";
            this.comboBox2.Text = "4000000000";
            this.comboBox2.BackColor = Color.FromArgb(192, 255, 192);

            ///////////////////////////////////////////////////////////
            // в C диапазоне не исп. некоторые значения
            ///////////////////////////////////////////////////////////

            // Температура среды для небольшой облачности
            this.label32.Enabled = false;
            this.textBox37.Enabled = false;
            this.label71.Enabled = false;
            // Ослабление сигнала из-за поглощения газами (Aatm)
            this.label33.Enabled = false;
            this.textBox38.Enabled = false;
            this.label70.Enabled = false;
            // Шумовая темп. поглощения в чистой атм.(Tclear sky)
            this.label34.Enabled = false;
            this.textBox39.Enabled = false;
            this.label69.Enabled = false;

            this.button10.Enabled = false;
            this.label88.Enabled = false;
            this.label105.Enabled = false;

            // Ослабление сигнала в дожде
            this.label35.Enabled = false;
            this.textBox73.Enabled = false;
            this.label77.Enabled = false;
            // Температура среды в условиях дождя
            this.label36.Enabled = false;
            this.textBox74.Enabled = false;
            this.label78.Enabled = false;
            // Шумовая температура поглощения в дожде(Train)
            this.label37.Enabled = false;
            this.textBox75.Enabled = false;
            this.label79.Enabled = false;

            // Общ. шумовая температура системы при дожде(Tsys_rain)
            this.label39.Enabled = false;
            this.textBox77.Enabled = false;
            this.textBox84.Enabled = false;
            this.textBox97.Enabled = false;
            this.textBox98.Enabled = false;
            this.textBox100.Enabled = false;
            this.textBox101.Enabled = false;
            this.textBox102.Enabled = false;
            this.textBox103.Enabled = false;
            this.textBox104.Enabled = false;
            // Cнижение эффективности линии связи вниз (DND)
            this.label40.Enabled = false;
            this.textBox78.Enabled = false;
            this.textBox105.Enabled = false;
            this.textBox106.Enabled = false;
            this.textBox107.Enabled = false;
            this.textBox108.Enabled = false;
            this.textBox109.Enabled = false;
            this.textBox110.Enabled = false;
            this.textBox111.Enabled = false;
            this.textBox112.Enabled = false;

            // Минимальный коэффициент добротности (G/Tusable) 
            this.label43.Enabled = false;
            this.textBox81.Enabled = false;
            this.textBox121.Enabled = false;
            this.textBox122.Enabled = false;
            this.textBox123.Enabled = false;
            this.textBox124.Enabled = false;
            this.textBox125.Enabled = false;
            this.textBox126.Enabled = false;
            this.textBox127.Enabled = false;
            this.textBox128.Enabled = false;

            // Отношение несущая/шум в плохих условиях (C/N_rain)
            this.label46.Enabled = false;
            this.textBox99.Enabled = false;
            this.textBox129.Enabled = false;
            this.textBox130.Enabled = false;
            this.textBox131.Enabled = false;
            this.textBox132.Enabled = false;
            this.textBox133.Enabled = false;
            this.textBox134.Enabled = false;
            this.textBox135.Enabled = false;
            this.textBox136.Enabled = false;

            // Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0)
            this.label49.Enabled = false;
            this.textBox87.Enabled = false;
            this.textBox145.Enabled = false;
            this.textBox146.Enabled = false;
            this.textBox147.Enabled = false;
            this.textBox148.Enabled = false;
            this.textBox149.Enabled = false;
            this.textBox150.Enabled = false;
            this.textBox151.Enabled = false;
            this.textBox152.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // готовим поля для работы - Длина волны (лямбда)
            double val = 300000000 / Convert.ToDouble(this.comboBox2.Text);
            this.textBox24.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Длина волны (лямбда)
            this.textBox24.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Потери в свободном пространстве (Lfs)
            val = 20 * Math.Log((4000 * Math.PI * Convert.ToDouble(this.textBox23.Text)) / Convert.ToDouble(this.textBox24.Text));
            this.textBox25.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Потери в свободном пространстве (Lfs)
            this.textBox25.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {
            // ставим индикатор на поле - Процент эффективности антенны
            this.textBox27.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // готовим поля для работы - Коэффициент усиления антенны(Ga) - 0,5
            double val = 10 * Math.Log10(Math.Pow(Convert.ToDouble(Math.PI * Convert.ToDouble(this.textBox28.Text)), 2) * Convert.ToDouble(this.textBox27.Text) / (100 * Math.Pow(Convert.ToDouble(this.textBox24.Text), 2)));
            this.textBox29.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Коэффициент усиления антенны(Ga) - 0,5
            this.textBox29.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox28.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Коэффициент усиления антенны(Ga) - 0,6
            val = 10 * Math.Log10(Math.Pow(Convert.ToDouble(Math.PI * Convert.ToDouble(this.textBox41.Text)), 2) * Convert.ToDouble(this.textBox27.Text) / (100 * Math.Pow(Convert.ToDouble(this.textBox24.Text), 2)));
            this.textBox42.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Коэффициент усиления антенны(Ga) - 0,6
            this.textBox42.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox41.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Коэффициент усиления антенны(Ga) - 0,8
            val = 10 * Math.Log10(Math.Pow(Convert.ToDouble(Math.PI * Convert.ToDouble(this.textBox43.Text)), 2) * Convert.ToDouble(this.textBox27.Text) / (100 * Math.Pow(Convert.ToDouble(this.textBox24.Text), 2)));
            this.textBox44.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Коэффициент усиления антенны(Ga) - 0,8
            this.textBox44.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox43.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Коэффициент усиления антенны(Ga) - 1,0
            val = 10 * Math.Log10(Math.Pow(Convert.ToDouble(Math.PI * Convert.ToDouble(this.textBox45.Text)), 2) * Convert.ToDouble(this.textBox27.Text) / (100 * Math.Pow(Convert.ToDouble(this.textBox24.Text), 2)));
            this.textBox46.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Коэффициент усиления антенны(Ga) - 1,0
            this.textBox46.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox45.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Коэффициент усиления антенны(Ga) - 1,2
            val = 10 * Math.Log10(Math.Pow(Convert.ToDouble(Math.PI * Convert.ToDouble(this.textBox47.Text)), 2) * Convert.ToDouble(this.textBox27.Text) / (100 * Math.Pow(Convert.ToDouble(this.textBox24.Text), 2)));
            this.textBox48.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Коэффициент усиления антенны(Ga) - 1,2
            this.textBox48.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox47.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Коэффициент усиления антенны(Ga) - 1,4
            val = 10 * Math.Log10(Math.Pow(Convert.ToDouble(Math.PI * Convert.ToDouble(this.textBox49.Text)), 2) * Convert.ToDouble(this.textBox27.Text) / (100 * Math.Pow(Convert.ToDouble(this.textBox24.Text), 2)));
            this.textBox50.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Коэффициент усиления антенны(Ga) - 1,4
            this.textBox50.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox49.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Коэффициент усиления антенны(Ga) - 1,8
            val = 10 * Math.Log10(Math.Pow(Convert.ToDouble(Math.PI * Convert.ToDouble(this.textBox51.Text)), 2) * Convert.ToDouble(this.textBox27.Text) / (100 * Math.Pow(Convert.ToDouble(this.textBox24.Text), 2)));
            this.textBox52.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Коэффициент усиления антенны(Ga) - 1,8
            this.textBox52.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox51.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Коэффициент усиления антенны(Ga) - 2,0
            val = 10 * Math.Log10(Math.Pow(Convert.ToDouble(Math.PI * Convert.ToDouble(this.textBox53.Text)), 2) * Convert.ToDouble(this.textBox27.Text) / (100 * Math.Pow(Convert.ToDouble(this.textBox24.Text), 2)));
            this.textBox54.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Коэффициент усиления антенны(Ga) - 2,0
            this.textBox54.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox53.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Коэффициент усиления антенны(Ga) - 2,5
            val = 10 * Math.Log10(Math.Pow(Convert.ToDouble(Math.PI * Convert.ToDouble(this.textBox55.Text)), 2) * Convert.ToDouble(this.textBox27.Text) / (100 * Math.Pow(Convert.ToDouble(this.textBox24.Text), 2)));
            this.textBox56.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Коэффициент усиления антенны(Ga) - 2,5
            this.textBox56.BackColor = Color.FromArgb(192, 255, 192);
            this.textBox55.BackColor = Color.FromArgb(192, 255, 192);

            //////////////////////////////////////////////////////////////////////////////
            // Шумовая температура антенны(TANT)
            //////////////////////////////////////////////////////////////////////////////

            // готовим поля для работы - Шумовая температура антенны(TANT) - 0,5
            val = 15 + 30 / Convert.ToDouble(this.textBox28.Text) + 190 / Convert.ToDouble(this.textBox19.Text);
            this.textBox34.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Шумовая температура антенны(TANT) - 0,5
            this.textBox34.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Шумовая температура антенны(TANT) - 0,6
            val = 15 + 30 / Convert.ToDouble(this.textBox41.Text) + 190 / Convert.ToDouble(this.textBox19.Text);
            this.textBox57.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Шумовая температура антенны(TANT) - 0,6
            this.textBox57.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Шумовая температура антенны(TANT) - 0,8
            val = 15 + 30 / Convert.ToDouble(this.textBox43.Text) + 190 / Convert.ToDouble(this.textBox19.Text);
            this.textBox59.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Шумовая температура антенны(TANT) - 0,8
            this.textBox59.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Шумовая температура антенны(TANT) - 1,0
            val = 15 + 30 / Convert.ToDouble(this.textBox45.Text) + 190 / Convert.ToDouble(this.textBox19.Text);
            this.textBox61.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Шумовая температура антенны(TANT) - 1,0
            this.textBox61.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Шумовая температура антенны(TANT) - 1,2
            val = 15 + 30 / Convert.ToDouble(this.textBox47.Text) + 190 / Convert.ToDouble(this.textBox19.Text);
            this.textBox63.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Шумовая температура антенны(TANT) - 1,2
            this.textBox63.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Шумовая температура антенны(TANT) - 1,4
            val = 15 + 30 / Convert.ToDouble(this.textBox49.Text) + 190 / Convert.ToDouble(this.textBox19.Text);
            this.textBox65.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Шумовая температура антенны(TANT) - 1,4
            this.textBox65.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Шумовая температура антенны(TANT) - 1,8
            val = 15 + 30 / Convert.ToDouble(this.textBox51.Text) + 190 / Convert.ToDouble(this.textBox19.Text);
            this.textBox67.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Шумовая температура антенны(TANT) - 1,8
            this.textBox67.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Шумовая температура антенны(TANT) - 2,0
            val = 15 + 30 / Convert.ToDouble(this.textBox53.Text) + 190 / Convert.ToDouble(this.textBox19.Text);
            this.textBox69.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Шумовая температура антенны(TANT) - 2,0
            this.textBox69.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Шумовая температура антенны(TANT) - 2,5
            val = 15 + 30 / Convert.ToDouble(this.textBox55.Text) + 190 / Convert.ToDouble(this.textBox19.Text);
            this.textBox71.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Шумовая температура антенны(TANT) - 2,5
            this.textBox71.BackColor = Color.FromArgb(192, 255, 192);

        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {
            // ставим индикатор на поле - Шум конвертера(LNB)
            this.textBox30.BackColor = Color.FromArgb(192, 255, 192);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            // готовим поля для работы - Шумовая температура конвертера(TLNB)
            double val = 290 * (Math.Pow(10, (Convert.ToDouble(this.textBox30.Text) / 10)) - 1);
            this.textBox31.Text = Convert.ToString(val.ToString("F7"));
            // ставим индикатор на поле - Шумовая температура конвертера(TLNB)
            this.textBox31.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {
            // ставим индикатор на поле - Общая величина вносимого затухания компонентов
            this.textBox32.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // готовим поля для работы - Тепловая температура переходных шумов
            double val = 290 * (1 - Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))));
            this.textBox33.Text = Convert.ToString(val.ToString("F8"));
            // ставим индикатор на поле - Тепловая температура переходных шумов
            this.textBox33.BackColor = Color.FromArgb(192, 255, 192);

            //////////////////////////////////////////////////////////////////////////////
            // Эквивалентная шумовая температура(сигма TANT)
            //////////////////////////////////////////////////////////////////////////////

            // готовим поля для работы - Эквивалентная шумовая температура(сигма TANT) - 0,5
            val = Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * Convert.ToDouble(this.textBox34.Text);
            this.textBox35.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Эквивалентная шумовая температура(сигма TANT) - 0,5
            this.textBox35.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Эквивалентная шумовая температура(сигма TANT) - 0,6
            val = Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * Convert.ToDouble(this.textBox57.Text);
            this.textBox58.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Эквивалентная шумовая температура(сигма TANT) - 0,6
            this.textBox58.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Эквивалентная шумовая температура(сигма TANT) - 0,8
            val = Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * Convert.ToDouble(this.textBox59.Text);
            this.textBox60.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Эквивалентная шумовая температура(сигма TANT) - 0,8
            this.textBox60.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Эквивалентная шумовая температура(сигма TANT) - 1,0
            val = Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * Convert.ToDouble(this.textBox61.Text);
            this.textBox62.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Эквивалентная шумовая температура(сигма TANT) - 1,0
            this.textBox62.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Эквивалентная шумовая температура(сигма TANT) - 1,2
            val = Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * Convert.ToDouble(this.textBox63.Text);
            this.textBox64.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Эквивалентная шумовая температура(сигма TANT) - 1,2
            this.textBox64.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Эквивалентная шумовая температура(сигма TANT) - 1,4
            val = Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * Convert.ToDouble(this.textBox65.Text);
            this.textBox66.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Эквивалентная шумовая температура(сигма TANT) - 1,4
            this.textBox66.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Эквивалентная шумовая температура(сигма TANT) - 1,8
            val = Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * Convert.ToDouble(this.textBox67.Text);
            this.textBox68.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Эквивалентная шумовая температура(сигма TANT) - 1,8
            this.textBox68.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Эквивалентная шумовая температура(сигма TANT) - 2,0
            val = Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * Convert.ToDouble(this.textBox69.Text);
            this.textBox70.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Эквивалентная шумовая температура(сигма TANT) - 2,0
            this.textBox70.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Эквивалентная шумовая температура(сигма TANT) - 2,5
            val = Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * Convert.ToDouble(this.textBox71.Text);
            this.textBox72.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Эквивалентная шумовая температура(сигма TANT) - 2,5
            this.textBox72.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox36_TextChanged(object sender, EventArgs e)
        {
            // ставим индикатор на поле и показываем ято данные введены в поле - Шумовая температура галактики
            this.textBox36.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox37_TextChanged(object sender, EventArgs e)
        {
            // ставим индикатор на поле и показываем ято данные введены в поле - Температура среды для небольшой облачности
            this.textBox37.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox38_TextChanged(object sender, EventArgs e)
        {
            // ставим индикатор на поле и показываем ято данные введены в поле - Ослабление сигнала из-за поглощения газами (Aatm)
            this.textBox38.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // готовим поля для работы - Шумовая темп. поглощения в чистой атм.(Tclear sky)
            double val = (1 - Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox38.Text)))) * Convert.ToDouble(this.textBox37.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox38.Text))) * Convert.ToDouble(this.textBox36.Text);
            this.textBox39.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Шумовая темп. поглощения в чистой атм.(Tclear sky)
            this.textBox39.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox73_TextChanged(object sender, EventArgs e)
        {
            // ставим индикатор на поле и показываем что данные введены в поле - Ослабление сигнала в дожде
            this.textBox73.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox74_TextChanged(object sender, EventArgs e)
        {
            // ставим индикатор на поле и показываем что данные введены в поле - Температура среды в условиях дождя
            this.textBox74.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Шумовая температура поглощения в дожде(Train)
                double vl = (1 - Math.Pow(10, (-0.1 * (Convert.ToDouble(this.textBox38.Text) + Convert.ToDouble(this.textBox73.Text))))) * Convert.ToDouble(this.textBox74.Text) + Math.Pow(10, (-0.1 * (Convert.ToDouble(this.textBox38.Text) + Convert.ToDouble(this.textBox73.Text)))) * Convert.ToDouble(this.textBox36.Text);
                this.textBox75.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Шумовая температура поглощения в дожде(Train)
                this.textBox75.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Шумовая температура поглощения в дожде(Train)
                this.textBox75.Text = " - ";
                // ставим индикатор на поле - Шумовая температура поглощения в дожде(Train)
                this.textBox75.BackColor = Color.FromArgb(192, 255, 192);
            }

            //////////////////////////////////////////////////////////////////////////////
            // Общ. шумовая температура системы при чистом небе(Tsys_clear sky)
            //////////////////////////////////////////////////////////////////////////////
            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 1
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox35.Text) + Convert.ToDouble(this.textBox39.Text));
                this.textBox76.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 1
                this.textBox76.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 1
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox35.Text));
                this.textBox76.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 1
                this.textBox76.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 2
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox58.Text) + Convert.ToDouble(this.textBox39.Text));
                this.textBox89.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 2
                this.textBox89.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 2
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox58.Text));
                this.textBox89.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 2
                this.textBox89.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 3
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox60.Text) + Convert.ToDouble(this.textBox39.Text));
                this.textBox90.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 3
                this.textBox90.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 3
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox60.Text));
                this.textBox90.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 3
                this.textBox90.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 4
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox62.Text) + Convert.ToDouble(this.textBox39.Text));
                this.textBox91.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 4
                this.textBox91.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 4
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox62.Text));
                this.textBox91.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 4
                this.textBox91.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 5
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox64.Text) + Convert.ToDouble(this.textBox39.Text));
                this.textBox92.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 5
                this.textBox92.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 5
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox64.Text));
                this.textBox92.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 5
                this.textBox92.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 6
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox66.Text) + Convert.ToDouble(this.textBox39.Text));
                this.textBox93.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 6
                this.textBox93.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 6
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox66.Text));
                this.textBox93.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 6
                this.textBox93.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 7
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox68.Text) + Convert.ToDouble(this.textBox39.Text));
                this.textBox94.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 7
                this.textBox94.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 7
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox68.Text));
                this.textBox94.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 7
                this.textBox94.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 8
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox70.Text) + Convert.ToDouble(this.textBox39.Text));
                this.textBox95.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 8
                this.textBox95.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 8
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox70.Text));
                this.textBox95.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 8
                this.textBox95.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 9
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox72.Text) + Convert.ToDouble(this.textBox39.Text));
                this.textBox96.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 9
                this.textBox96.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 9
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox72.Text));
                this.textBox96.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при чистом небе(Tsys_clear sky) - поле номер 9
                this.textBox96.BackColor = Color.FromArgb(192, 255, 192);
            }


            //////////////////////////////////////////////////////////////////////////////
            // Общ. шумовая температура системы при дожде(Tsys_rain)
            //////////////////////////////////////////////////////////////////////////////

            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 1
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox35.Text) + Convert.ToDouble(this.textBox75.Text));
                this.textBox77.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 1
                this.textBox77.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain)
                this.textBox77.Text = " - ";
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain)
                this.textBox77.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 2
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox58.Text) + Convert.ToDouble(this.textBox75.Text));
                this.textBox84.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 2
                this.textBox84.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 2
                this.textBox84.Text = " - ";
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 2
                this.textBox84.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 3
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox60.Text) + Convert.ToDouble(this.textBox75.Text));
                this.textBox97.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 3
                this.textBox97.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 3
                this.textBox97.Text = " - ";
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 3
                this.textBox97.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 4
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox62.Text) + Convert.ToDouble(this.textBox75.Text));
                this.textBox98.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 4
                this.textBox98.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 4
                this.textBox98.Text = " - ";
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 4
                this.textBox98.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 5
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox64.Text) + Convert.ToDouble(this.textBox75.Text));
                this.textBox100.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 5
                this.textBox100.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 5
                this.textBox100.Text = " - ";
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 5
                this.textBox100.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 6
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox66.Text) + Convert.ToDouble(this.textBox75.Text));
                this.textBox101.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 6
                this.textBox101.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 6
                this.textBox101.Text = " - ";
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 6
                this.textBox101.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 7
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox68.Text) + Convert.ToDouble(this.textBox75.Text));
                this.textBox102.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 7
                this.textBox102.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 7
                this.textBox102.Text = " - ";
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 7
                this.textBox102.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 8
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox70.Text) + Convert.ToDouble(this.textBox75.Text));
                this.textBox103.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 8
                this.textBox103.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 8
                this.textBox103.Text = " - ";
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 8
                this.textBox103.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 9
                double vl = Convert.ToDouble(this.textBox31.Text) + Convert.ToDouble(this.textBox33.Text) + Math.Pow(10, (-0.1 * Convert.ToDouble(this.textBox32.Text))) * (Convert.ToDouble(this.textBox72.Text) + Convert.ToDouble(this.textBox75.Text));
                this.textBox104.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 9
                this.textBox104.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 9
                this.textBox104.Text = " - ";
                // ставим индикатор на поле - Общ. шумовая температура системы при дожде(Tsys_rain) - поле номер 9
                this.textBox104.BackColor = Color.FromArgb(192, 255, 192);
            }


            //////////////////////////////////////////////////////////////////////////////
            // Cнижение эффективности линии связи вниз (DND)
            //////////////////////////////////////////////////////////////////////////////


            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 1
                double vl = Convert.ToDouble(this.textBox73.Text) + 10 * Math.Log(Convert.ToDouble(this.textBox77.Text) / Convert.ToDouble(this.textBox76.Text));
                this.textBox78.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 1
                this.textBox78.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 1
                this.textBox78.Text = " - ";
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 1
                this.textBox78.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 2
                double vl = Convert.ToDouble(this.textBox73.Text) + 10 * Math.Log(Convert.ToDouble(this.textBox84.Text) / Convert.ToDouble(this.textBox89.Text));
                this.textBox105.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 2
                this.textBox105.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 2
                this.textBox105.Text = " - ";
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 2
                this.textBox105.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 3
                double vl = Convert.ToDouble(this.textBox73.Text) + 10 * Math.Log(Convert.ToDouble(this.textBox97.Text) / Convert.ToDouble(this.textBox90.Text));
                this.textBox106.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле c
                this.textBox106.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 4
                this.textBox106.Text = " - ";
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 4
                this.textBox106.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 4
                double vl = Convert.ToDouble(this.textBox73.Text) + 10 * Math.Log(Convert.ToDouble(this.textBox98.Text) / Convert.ToDouble(this.textBox91.Text));
                this.textBox107.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 4
                this.textBox107.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 4
                this.textBox107.Text = " - ";
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 4
                this.textBox107.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 5
                double vl = Convert.ToDouble(this.textBox73.Text) + 10 * Math.Log(Convert.ToDouble(this.textBox100.Text) / Convert.ToDouble(this.textBox92.Text));
                this.textBox108.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 5
                this.textBox108.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 5
                this.textBox108.Text = " - ";
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 5
                this.textBox108.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 6
                double vl = Convert.ToDouble(this.textBox73.Text) + 10 * Math.Log(Convert.ToDouble(this.textBox101.Text) / Convert.ToDouble(this.textBox93.Text));
                this.textBox109.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 6
                this.textBox109.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 6
                this.textBox109.Text = " - ";
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 6
                this.textBox109.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 7
                double vl = Convert.ToDouble(this.textBox73.Text) + 10 * Math.Log(Convert.ToDouble(this.textBox102.Text) / Convert.ToDouble(this.textBox94.Text));
                this.textBox110.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 7
                this.textBox110.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 7
                this.textBox110.Text = " - ";
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 7
                this.textBox110.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 8
                double vl = Convert.ToDouble(this.textBox73.Text) + 10 * Math.Log(Convert.ToDouble(this.textBox103.Text) / Convert.ToDouble(this.textBox95.Text));
                this.textBox111.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 8
                this.textBox111.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 8
                this.textBox111.Text = " - ";
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 8
                this.textBox111.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 9
                double vl = Convert.ToDouble(this.textBox73.Text) + 10 * Math.Log(Convert.ToDouble(this.textBox104.Text) / Convert.ToDouble(this.textBox96.Text));
                this.textBox112.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле 
                this.textBox112.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Cнижение эффективности линии связи вниз (DND) - поле номер 9
                this.textBox112.Text = " - ";
                // ставим индикатор на поле - Cнижение эффективности линии связи вниз (DND) - поле номер 9
                this.textBox112.BackColor = Color.FromArgb(192, 255, 192);
            }


            //////////////////////////////////////////////////////////////////////////////
            // Коэффициент добротности (G/Tnom) 
            //////////////////////////////////////////////////////////////////////////////


            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 1
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox29.Text) - Convert.ToDouble(this.textBox32.Text)))) / Convert.ToDouble(this.textBox76.Text));
                this.textBox79.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 1
                this.textBox79.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 1
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox29.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox76.Text));
                this.textBox79.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 1
                this.textBox79.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 2
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox42.Text) - Convert.ToDouble(this.textBox32.Text)))) / Convert.ToDouble(this.textBox89.Text));
                this.textBox113.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 2
                this.textBox113.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 2
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox42.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox89.Text));
                this.textBox113.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 2
                this.textBox113.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 3
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox44.Text) - Convert.ToDouble(this.textBox32.Text)))) / Convert.ToDouble(this.textBox90.Text));
                this.textBox114.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 3
                this.textBox114.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 3
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox44.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox90.Text));
                this.textBox114.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 3
                this.textBox114.BackColor = Color.FromArgb(192, 255, 192);
            }




            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 4
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox46.Text) - Convert.ToDouble(this.textBox32.Text)))) / Convert.ToDouble(this.textBox91.Text));
                this.textBox115.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 4
                this.textBox115.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 4
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox46.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox91.Text));
                this.textBox115.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 4
                this.textBox115.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 5
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox48.Text) - Convert.ToDouble(this.textBox32.Text)))) / Convert.ToDouble(this.textBox92.Text));
                this.textBox116.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 5
                this.textBox116.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 5
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox48.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox92.Text));
                this.textBox116.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 5
                this.textBox116.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 6
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox50.Text) - Convert.ToDouble(this.textBox32.Text)))) / Convert.ToDouble(this.textBox93.Text));
                this.textBox117.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 6
                this.textBox117.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 6
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox50.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox93.Text));
                this.textBox117.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 6
                this.textBox117.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 7
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox52.Text) - Convert.ToDouble(this.textBox32.Text)))) / Convert.ToDouble(this.textBox94.Text));
                this.textBox118.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 7
                this.textBox118.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 7
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox52.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox94.Text));
                this.textBox118.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 7
                this.textBox118.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 8
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox54.Text) - Convert.ToDouble(this.textBox32.Text)))) / Convert.ToDouble(this.textBox95.Text));
                this.textBox119.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 8
                this.textBox119.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 8
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox54.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox95.Text));
                this.textBox119.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 8
                this.textBox119.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 9
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox56.Text) - Convert.ToDouble(this.textBox32.Text)))) / Convert.ToDouble(this.textBox96.Text));
                this.textBox120.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 9
                this.textBox120.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Коэффициент добротности (G/Tnom)  - поле номер 9
                double vl = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox56.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox96.Text));
                this.textBox120.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Коэффициент добротности (G/Tnom)  - поле номер 9
                this.textBox120.BackColor = Color.FromArgb(192, 255, 192);
            }

        }

        private void textBox80_TextChanged(object sender, EventArgs e)
        {
            // ставим индикатор на поле и показываем что данные введены в поле - Потери из-за неточного наведения антенны
            this.textBox80.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////////////////
            // Минимальный коэффициент добротности (G/Tusable) 
            //////////////////////////////////////////////////////////////////////////////


            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 1
                double val = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox29.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox77.Text));
                this.textBox81.Text = Convert.ToString(val.ToString("F2"));
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 1
                this.textBox81.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 1
                this.textBox81.Text = " - ";
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 1
                this.textBox81.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 2
                double val = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox42.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox84.Text));
                this.textBox121.Text = Convert.ToString(val.ToString("F2"));
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 2
                this.textBox121.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 2
                this.textBox121.Text = " - ";
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 2
                this.textBox121.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 3
                double val = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox44.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox97.Text));
                this.textBox122.Text = Convert.ToString(val.ToString("F2"));
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 3
                this.textBox122.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 3
                this.textBox122.Text = " - ";
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 3
                this.textBox122.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 4
                double val = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox46.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox98.Text));
                this.textBox123.Text = Convert.ToString(val.ToString("F2"));
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 4
                this.textBox123.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 4
                this.textBox123.Text = " - ";
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 4
                this.textBox123.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 5
                double val = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox48.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox100.Text));
                this.textBox124.Text = Convert.ToString(val.ToString("F2"));
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 5
                this.textBox124.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 5
                this.textBox124.Text = " - ";
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 5
                this.textBox124.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 6
                double val = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox50.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox101.Text));
                this.textBox125.Text = Convert.ToString(val.ToString("F2"));
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 6
                this.textBox125.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 6
                this.textBox125.Text = " - ";
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 6
                this.textBox125.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 7
                double val = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox52.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox102.Text));
                this.textBox126.Text = Convert.ToString(val.ToString("F2"));
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 7
                this.textBox126.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 7
                this.textBox126.Text = " - ";
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 7
                this.textBox126.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 8
                double val = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox54.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox103.Text));
                this.textBox127.Text = Convert.ToString(val.ToString("F2"));
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 8
                this.textBox127.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 8
                this.textBox127.Text = " - ";
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 8
                this.textBox127.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 9
                double val = 10 * Math.Log(Math.Pow(10, (0.1 * (Convert.ToDouble(this.textBox56.Text) - Convert.ToDouble(this.textBox32.Text) + Convert.ToDouble(this.textBox80.Text)))) / Convert.ToDouble(this.textBox104.Text));
                this.textBox128.Text = Convert.ToString(val.ToString("F2"));
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 9
                this.textBox128.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 9
                this.textBox128.Text = " - ";
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 9
                this.textBox128.BackColor = Color.FromArgb(192, 255, 192);
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // готовим поля предвыборки для работы - Рабочая частота (f)
            this.comboBox2.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            // готовим поля предвыборки для работы - Рабочая частота (f)
            this.comboBox2.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // готовим поля предвыборки для работы - Полоса пропускания приемника (ширина радиоканала)(B)
            this.comboBox3.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            // готовим поля предвыборки для работы - Полоса пропускания приемника (ширина радиоканала)(B)
            this.comboBox3.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox83_TextChanged(object sender, EventArgs e)
        {
            // ставим индикатор на поле - EIRP(эквивалентная изотропная излучаемая мощность)
            this.textBox83.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////////////////
            // Отношение несущая/шум в плохих условиях (C/N_rain)
            //////////////////////////////////////////////////////////////////////////////


            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 1
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox81.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox73.Text) - Convert.ToDouble(this.textBox38.Text);
                this.textBox99.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 1
                this.textBox99.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 1
                this.textBox99.Text = " - ";
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 1
                this.textBox99.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 2
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox121.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox73.Text) - Convert.ToDouble(this.textBox38.Text);
                this.textBox129.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 2
                this.textBox129.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 2
                this.textBox129.Text = " - ";
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 2
                this.textBox129.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 3
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox122.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox73.Text) - Convert.ToDouble(this.textBox38.Text);
                this.textBox130.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 3
                this.textBox130.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 3
                this.textBox130.Text = " - ";
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 3
                this.textBox130.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 4
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox123.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox73.Text) - Convert.ToDouble(this.textBox38.Text);
                this.textBox131.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 4
                this.textBox131.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 4
                this.textBox131.Text = " - ";
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 4
                this.textBox131.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 5
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox124.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox73.Text) - Convert.ToDouble(this.textBox38.Text);
                this.textBox132.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 5
                this.textBox132.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 5
                this.textBox132.Text = " - ";
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 5
                this.textBox132.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 6
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox125.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox73.Text) - Convert.ToDouble(this.textBox38.Text);
                this.textBox133.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 6
                this.textBox133.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 6
                this.textBox133.Text = " - ";
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 6
                this.textBox133.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 7
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox126.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox73.Text) - Convert.ToDouble(this.textBox38.Text);
                this.textBox134.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 7
                this.textBox134.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 7
                this.textBox134.Text = " - ";
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 7
                this.textBox134.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 8
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox127.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox73.Text) - Convert.ToDouble(this.textBox38.Text);
                this.textBox135.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 8
                this.textBox135.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 8
                this.textBox135.Text = " - ";
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 8
                this.textBox135.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 9
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox128.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox73.Text) - Convert.ToDouble(this.textBox38.Text);
                this.textBox136.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в плохих условиях (C/N_rain) - поле номер 9
                this.textBox136.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Минимальный коэффициент добротности (G/Tusable) - поле номер 9
                this.textBox136.Text = " - ";
                // ставим индикатор на поле - Минимальный коэффициент добротности (G/Tusable) - поле номер 9
                this.textBox136.BackColor = Color.FromArgb(192, 255, 192);
            }


            //////////////////////////////////////////////////////////////////////////////
            // Отношение несущая/шум в хороших условиях (C/N_clear_sky)
            //////////////////////////////////////////////////////////////////////////////


            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 1
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox79.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox38.Text);
                this.textBox85.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 1
                this.textBox85.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 1
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox79.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text));
                this.textBox85.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 1
                this.textBox85.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 2
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox113.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox38.Text);
                this.textBox137.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 2
                this.textBox137.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 2
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox113.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text));
                this.textBox137.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 2
                this.textBox137.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 3
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox114.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox38.Text);
                this.textBox138.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 3
                this.textBox138.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 3
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox114.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text));
                this.textBox138.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 3
                this.textBox138.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 4
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox115.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox38.Text);
                this.textBox139.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 4
                this.textBox139.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 4
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox115.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text));
                this.textBox139.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 4
                this.textBox139.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 5
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox116.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox38.Text);
                this.textBox140.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 5
                this.textBox140.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 5
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox116.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text));
                this.textBox140.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 5
                this.textBox140.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 6
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox117.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox38.Text);
                this.textBox141.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 6
                this.textBox141.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 6
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox117.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text));
                this.textBox141.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 6
                this.textBox141.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 7
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox118.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox38.Text);
                this.textBox142.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 7
                this.textBox142.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 7
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox118.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text));
                this.textBox142.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 7
                this.textBox142.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 8
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox119.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox38.Text);
                this.textBox143.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 8
                this.textBox143.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 8
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox119.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text));
                this.textBox143.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 8
                this.textBox143.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 9
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox120.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text)) - Convert.ToDouble(this.textBox38.Text);
                this.textBox144.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 9
                this.textBox144.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 9
                double vl = Convert.ToDouble(this.textBox83.Text) - Convert.ToDouble(this.textBox25.Text) + Convert.ToDouble(this.textBox120.Text) - 10 * Math.Log(1.38 * (Math.Pow(10, -23)) * Convert.ToDouble(this.comboBox3.Text));
                this.textBox144.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отношение несущая/шум в хороших условиях (C/N_clear_sky) - поле номер 9
                this.textBox144.BackColor = Color.FromArgb(192, 255, 192);
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////////////////////////////
            // Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0)
            //////////////////////////////////////////////////////////////////////////////


            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 1
                double vl = Convert.ToDouble(this.textBox99.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
                this.textBox87.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 1
                this.textBox87.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 1
                this.textBox87.Text = " - ";
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 1
                this.textBox87.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 2
                double vl = Convert.ToDouble(this.textBox129.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
                this.textBox145.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 2
                this.textBox145.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 2
                this.textBox145.Text = " - ";
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 2
                this.textBox145.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 3
                double vl = Convert.ToDouble(this.textBox130.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
                this.textBox146.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 3
                this.textBox146.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 3
                this.textBox146.Text = " - ";
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 3
                this.textBox146.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 4
                double vl = Convert.ToDouble(this.textBox131.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
                this.textBox147.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 4
                this.textBox147.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 4
                this.textBox147.Text = " - ";
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 4
                this.textBox147.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 5
                double vl = Convert.ToDouble(this.textBox132.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
                this.textBox148.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 5
                this.textBox148.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 5
                this.textBox148.Text = " - ";
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 5
                this.textBox148.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 6
                double vl = Convert.ToDouble(this.textBox133.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
                this.textBox149.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 6
                this.textBox149.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 6
                this.textBox149.Text = " - ";
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 6
                this.textBox149.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 7
                double vl = Convert.ToDouble(this.textBox134.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
                this.textBox150.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 7
                this.textBox150.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 7
                this.textBox150.Text = " - ";
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 7
                this.textBox150.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 8
                double vl = Convert.ToDouble(this.textBox135.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
                this.textBox151.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 8
                this.textBox151.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 8
                this.textBox151.Text = " - ";
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 8
                this.textBox151.BackColor = Color.FromArgb(192, 255, 192);
            }



            if (radioButton1.Checked == true) // если Ку диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 9
                double vl = Convert.ToDouble(this.textBox136.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
                this.textBox152.Text = Convert.ToString(vl.ToString("F2"));
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 9
                this.textBox152.BackColor = Color.FromArgb(192, 255, 192);
            }
            else if (radioButton2.Checked == true) // если С диапазон
            {
                // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 9
                this.textBox152.Text = " - ";
                // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0) - поле номер 9
                this.textBox152.BackColor = Color.FromArgb(192, 255, 192);
            }


            //////////////////////////////////////////////////////////////////////////////
            // Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0)
            //////////////////////////////////////////////////////////////////////////////


            // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 1
            double val = Convert.ToDouble(this.textBox85.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
            this.textBox88.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 1
            this.textBox88.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 2
            val = Convert.ToDouble(this.textBox137.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
            this.textBox153.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 2
            this.textBox153.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 3
            val = Convert.ToDouble(this.textBox138.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
            this.textBox154.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 3
            this.textBox154.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 4
            val = Convert.ToDouble(this.textBox139.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
            this.textBox155.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 4
            this.textBox155.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 5
            val = Convert.ToDouble(this.textBox140.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
            this.textBox156.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 5
            this.textBox156.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 6
            val = Convert.ToDouble(this.textBox141.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
            this.textBox157.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 6
            this.textBox157.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 7
            val = Convert.ToDouble(this.textBox142.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
            this.textBox158.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 7
            this.textBox158.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 8
            val = Convert.ToDouble(this.textBox143.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
            this.textBox159.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 8
            this.textBox159.BackColor = Color.FromArgb(192, 255, 192);

            // готовим поля для работы - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 9
            val = Convert.ToDouble(this.textBox144.Text) + 10 * Math.Log(1 / Convert.ToDouble(this.textBox86.Text)) + 10 * Math.Log(Convert.ToDouble(this.comboBox3.Text));
            this.textBox160.Text = Convert.ToString(val.ToString("F2"));
            // ставим индикатор на поле - Отн. кол-ва энергии бита к мощности шумов для хороших (Eb/N0) - поле номер 9
            this.textBox160.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox86_TextChanged(object sender, EventArgs e)
        {
            // ставим индикатор на поле - Максимальная скорость потока (бит/с)
            this.textBox86.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            // оповещаем пользователя пользователя, что будем работать с C диапазоном
            this.label83.Text = "C";
            this.comboBox2.Text = "4000000000";
            this.comboBox2.BackColor = Color.FromArgb(192, 255, 192);

            ///////////////////////////////////////////////////////////
            // в C диапазоне не исп. некоторые значения
            ///////////////////////////////////////////////////////////

            // Температура среды для небольшой облачности
            this.label32.Enabled = false;
            this.textBox37.Enabled = false;
            this.label71.Enabled = false;
            // Ослабление сигнала из-за поглощения газами (Aatm)
            this.label33.Enabled = false;
            this.textBox38.Enabled = false;
            this.label70.Enabled = false;
            // Шумовая темп. поглощения в чистой атм.(Tclear sky)
            this.label34.Enabled = false;
            this.textBox39.Enabled = false;
            this.label69.Enabled = false;

            this.button10.Enabled = false;
            this.label88.Enabled = false;
            this.label105.Enabled = false;

            // Ослабление сигнала в дожде
            this.label35.Enabled = false;
            this.textBox73.Enabled = false;
            this.label77.Enabled = false;
            // Температура среды в условиях дождя
            this.label36.Enabled = false;
            this.textBox74.Enabled = false;
            this.label78.Enabled = false;
            // Шумовая температура поглощения в дожде(Train)
            this.label37.Enabled = false;
            this.textBox75.Enabled = false;
            this.label79.Enabled = false;

            // Общ. шумовая температура системы при дожде(Tsys_rain)
            this.label39.Enabled = false;
            this.textBox77.Enabled = false;
            this.textBox84.Enabled = false;
            this.textBox97.Enabled = false;
            this.textBox98.Enabled = false;
            this.textBox100.Enabled = false;
            this.textBox101.Enabled = false;
            this.textBox102.Enabled = false;
            this.textBox103.Enabled = false;
            this.textBox104.Enabled = false;
            // Cнижение эффективности линии связи вниз (DND)
            this.label40.Enabled = false;
            this.textBox78.Enabled = false;
            this.textBox105.Enabled = false;
            this.textBox106.Enabled = false;
            this.textBox107.Enabled = false;
            this.textBox108.Enabled = false;
            this.textBox109.Enabled = false;
            this.textBox110.Enabled = false;
            this.textBox111.Enabled = false;
            this.textBox112.Enabled = false;

            // Минимальный коэффициент добротности (G/Tusable) 
            this.label43.Enabled = false;
            this.textBox81.Enabled = false;
            this.textBox121.Enabled = false;
            this.textBox122.Enabled = false;
            this.textBox123.Enabled = false;
            this.textBox124.Enabled = false;
            this.textBox125.Enabled = false;
            this.textBox126.Enabled = false;
            this.textBox127.Enabled = false;
            this.textBox128.Enabled = false;

            // Отношение несущая/шум в плохих условиях (C/N_rain)
            this.label46.Enabled = false;
            this.textBox99.Enabled = false;
            this.textBox129.Enabled = false;
            this.textBox130.Enabled = false;
            this.textBox131.Enabled = false;
            this.textBox132.Enabled = false;
            this.textBox133.Enabled = false;
            this.textBox134.Enabled = false;
            this.textBox135.Enabled = false;
            this.textBox136.Enabled = false;

            // Отн. кол-ва энергии бита к мощности шумов для плохих(Eb/N0)
            this.label49.Enabled = false;
            this.textBox87.Enabled = false;
            this.textBox145.Enabled = false;
            this.textBox146.Enabled = false;
            this.textBox147.Enabled = false;
            this.textBox148.Enabled = false;
            this.textBox149.Enabled = false;
            this.textBox150.Enabled = false;
            this.textBox151.Enabled = false;
            this.textBox152.Enabled = false;
         }
     }
}
